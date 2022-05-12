using FPTLibrary.Models;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;

namespace FPTLibrary.Controllers
{
    public class UnauthenticateController : Controller
    {
        // GET: Unauthenticate
        public ActionResult Index()

        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "Unauthenticate");
        }

        public JsonResult LoginCheck(string UserAccount, string UserPassword)
        {
            var returnData = new ReturnData();

            var result = new DataAccess.DAOImpl.UserDAOImpl().User_Login(UserAccount, UserPassword);

            try
            {
                if (result > 0)
                {

                    returnData.ResponseCode = 1;
                    returnData.Description = "Login successfully !!!";

                    Session[DataAccess.Libs.Config.SessionAccount] = new DataAccess.DAOImpl
                        .UserDAOImpl()
                        .Users_GetList()
                        .FirstOrDefault(u => u.UserAccount == UserAccount);

                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else if (result == -1)
                {
                    returnData.ResponseCode = -998;
                    returnData.Description = "User Account not exist. Login Fail !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    returnData.ResponseCode = -997;
                    returnData.Description = "User Account or Password was wrong. Login Fail !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
            }


            catch (System.Exception)
            {

                throw;
                //returnData.ResponseCode = -999;
                //returnData.Description = "System Bussy. please  f5";
                //return Json(returnData, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult Register()
        {
            var result = new DataAccess.DAOImpl.RoleDAOImpl().Roles_GetList();
            return View(result);
        }
      
        public JsonResult RegisterCheck(string UserAccount, string UserPassword, string UserFullName, string UserAddress, string UserPhoneNumber, int RoleID)
        {
            var returnData = new ReturnData();


            try
            {
                var checkExist = new DataAccess.DAOImpl.UserDAOImpl().User_CheckExist(UserAccount);
                if (checkExist < 0)
                {
                    returnData.ResponseCode = -999;
                    returnData.Description = "UserAccount already exist !! ";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                  

                    var body = $"<a href='https://localhost:44355/Unauthenticate/ConfirmMail'>Click to confirm your email</a>";
                    var email = UserAccount;
                    int sendMailToAccount = DataAccess.Libs.SendMail.SendMailToAccount(body, email);
                    if(sendMailToAccount == 1)
                    {
                        var result = new DataAccess.DAOImpl.UserDAOImpl()
                        .User_Register(UserAccount, UserPassword, UserFullName,
                        UserAddress, UserPhoneNumber, RoleID);
                        if (result > 0)
                        {

                            returnData.ResponseCode = 1;
                            returnData.Description = "please check mail !!!";
                            if (RoleID == 3)
                            {
                                var userID = new DataAccess.DAOImpl.UserDAOImpl().Users_GetList()
                                    .FirstOrDefault(u => u.UserAccount == UserAccount).UserID;
                                var createNewStore = new DataAccess.DAOImpl.StoreDAOImpl().Store_Create(userID, UserFullName);
                            }


                            return Json(returnData, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            returnData.ResponseCode = -98;
                            returnData.Description = "System bussy please F5!!!";
                            return Json(returnData, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        returnData.ResponseCode = -99;
                        returnData.Description = "System bussy please F5!!!";
                        return Json(returnData, JsonRequestBehavior.AllowGet);
                    }            
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public ActionResult ConfirmMail()
        {
            return View();
        }

    }
}