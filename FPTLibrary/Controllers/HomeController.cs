using DataAccess.DTO;
using System.Web.Mvc;

namespace FPTLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];

            try
            {
                if (userSession == null)
                {

                    return RedirectToAction("DoNotHavePermission", "Home");
                }
                else
                {
                    userSession.IsBanned = new DataAccess.DAOImpl.UserDAOImpl().User_CheckBan(userSession.UserID);

                    if (userSession.IsBanned == true)
                    {         
                        return RedirectToAction("DoNotHavePermission", "Home");
                    }

                    switch (userSession.RoleID)
                    {
                        case 1:

                            return RedirectToAction("Index", "User");
                        case 2:
                            return RedirectToAction("Index", "Book");
                        case 3:
                            return RedirectToAction("Index", "Store");
                        default:
                            return RedirectToAction("Login", "Unauthenticate");

                    }


                }

            }
            catch (System.Exception)
            {

                throw;
            }



        }

        public ActionResult DoNotHavePermission()
        {
            try
            {
                var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];

                userSession.IsBanned = new DataAccess.DAOImpl.UserDAOImpl().User_CheckBan(userSession.UserID);
                if (userSession.IsBanned == true)
                {
                    ViewBag.UserBanned = true;

                    Session.RemoveAll();
                    Session.Abandon();




                }
                return View();


            }
            catch (System.Exception)
            {

                return RedirectToAction("Login", "Unauthenticate");
            }

        }
    }
}