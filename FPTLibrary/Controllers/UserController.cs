using DataAccess.DTO;
using FPTLibrary.Models;
using System.Linq;
using System.Web.Mvc;

namespace FPTLibrary.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            try
            {
                var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }
                else
                {
                    if (userSession.RoleID != 1)
                    {
                        return RedirectToAction("DoNotHavePermission", "Home");
                    }
                    else
                    {

                        return View();
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        public ActionResult UserManagementParialView()
        {
            try
            {
                var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }
                else
                {

                    if (userSession.RoleID != 1)
                    {
                        return RedirectToAction("DoNotHavePermission", "Home");
                    }
                    else
                    {
                        var result = new DataAccess.DAOImpl.UserDAOImpl().Users_GetList().OrderBy(u => u.RoleID).ToList();
                        foreach (var item in result)
                        {
                            item.RoleName = new DataAccess.DAOImpl.RoleDAOImpl()
                                .Roles_GetList()
                                .FirstOrDefault(r => r.RoleID == item.RoleID)
                                .RoleName;
                            item.UserPassword = DataAccess.Libs
                           .MD5
                           .CreateMD5(item.UserPassword);
                            
                        }
                        return PartialView(result);
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        public ActionResult UserDetail(int UserID)
        {
            try
            {
                var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }
                else
                {
                    if (userSession.RoleID != 1)
                    {
                        return RedirectToAction("DoNotHavePermission", "Home");
                    }
                    else
                    {
                        var result = new DataAccess.DAOImpl.UserDAOImpl()
                            .Users_GetList()
                            .FirstOrDefault(u => u.UserID == UserID);
                        result.RoleName = new DataAccess.DAOImpl.RoleDAOImpl()
                            .Roles_GetList()
                            .FirstOrDefault(r => r.RoleID == result.RoleID).RoleName;
                        result.UserPassword = DataAccess.Libs
                            .MD5
                            .CreateMD5(result.UserPassword);
                        return View(result);
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        public ActionResult UserEdit(int UserID)
        {
            var result = new DataAccess.DAOImpl.UserDAOImpl()
                .Users_GetList().FirstOrDefault(u => u.UserID == UserID);
            return View(result);
        }
        public JsonResult UserUpdate(string UserAccount,string UserPassword, string UserFullName,string  UserAddress,string UserPhoneNumber)
        {
            var returnData = new ReturnData();
            var result = new DataAccess.DAOImpl.UserDAOImpl()
                .User_Update(UserAccount, UserPassword, UserFullName, UserAddress, UserPhoneNumber);
            try
            {
                if (result > 0)
                {
                    returnData.Description = "Update Successfully !!!";
                    returnData.ResponseCode = 999;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.Description = "Update Fail !!!";
                    returnData.ResponseCode = -998;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
            }
            catch (System.Exception)
            {
                returnData.Description = "Some thing went wrong !!! please try again";
                returnData.ResponseCode = -999;
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            
        }
        public JsonResult UserBan(int UserID)
        {
            var returnData = new ReturnData();

            try
            {
                var result = new DataAccess.DAOImpl.UserDAOImpl().User_Ban(UserID);
                if (result > 0)
                {
                    returnData.Description = "Banned User !!!";
                    returnData.ResponseCode = 999;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.Description = "Banned User Fail !!!";
                    returnData.ResponseCode = 999;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
            }

            catch (System.Exception)
            {

                returnData.Description = "Some thing went swrong!!! please try aain ";
                returnData.ResponseCode = 999;
                return Json(returnData, JsonRequestBehavior.AllowGet); ;
            }
         
            
        }
        public JsonResult UserUnBan(int UserID)
        {
            var returnData = new ReturnData();

            try
            {
                var result = new DataAccess.DAOImpl.UserDAOImpl().User_UnBan(UserID);
                if (result > 0)
                {
                    returnData.Description = "Un Banned User !!!";
                    returnData.ResponseCode = 999;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.Description = "Un Banned User Fail !!!";
                    returnData.ResponseCode = 999;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
            }

            catch (System.Exception)
            {

                returnData.Description = "Some thing went swrong!!! please try aain ";
                returnData.ResponseCode = 999;
                return Json(returnData, JsonRequestBehavior.AllowGet); ;
            }

        }


    }
}