using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FPTLibrary.Controllers
{
    public class BookController : Controller
    {
        public ActionResult Index()
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
            try
            {
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }
                else
                {
                    if (userSession.RoleID != 2)
                    {
                        return RedirectToAction("DoNotHavePermission", "Home");
                    }
                    else
                    {

                        return View();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult BookLibraryPartialView(int? PageNumber, int? NumberPerPage, string Keyword)
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
            var result = new List<BookDTO>();

            try
            {
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }
                else
                {
                    if (userSession.RoleID != 2)
                    {
                        return RedirectToAction("DoNotHavePermission", "Home");
                    }
                    else
                    {


                        if (PageNumber == null && NumberPerPage == null)
                        {
                            PageNumber = 1;
                            NumberPerPage = 6;
                        }

                        result = new DataAccess.DAOImpl.BookDAOImpl().Books_GetListByPage(PageNumber, NumberPerPage);
                        ViewBag.CurrentPage = PageNumber;
                        ViewBag.NumberPerPage = NumberPerPage;
                        ViewBag.EndPage = (new DataAccess.DAOImpl.BookDAOImpl().Books_GetList().Count) / NumberPerPage + 1;
                        ViewBag.Keyword = Keyword;
                        if (PageNumber > ViewBag.EndPage)
                        {
                            return HttpNotFound();
                        }
                        foreach (var item in result)
                        {
                            item.CategoryName = new DataAccess.DAOImpl.CategoryDAOImpl()
                                .Category_GetDetailByID(item.CategoryID).CategoryName;
                            item.StoreName = new DataAccess.DAOImpl.StoreDAOImpl().Stores_GetList()
                                .FirstOrDefault(s => s.StoreID == item.StoreID).StoreName;
                        }

                        return PartialView(result);


                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult BookDetail(long BookISBN)
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
            if (userSession == null)
            {
                return RedirectToAction("Login", "Unauthenticate");
            }
            else
            {
                if (userSession.RoleID != 2 && userSession.RoleID != 3)
                {
                    return RedirectToAction("DoNotHavePermission", "Home");
                }
                else
                {
                    ViewBag.user = userSession.RoleID;
                    var result = new DataAccess.DAOImpl.BookDAOImpl().Book_GetDetail(BookISBN);
                    return View(result);

                }
            }
        }


        public ActionResult InsertBook()
        {
            return View();
        }

        public ActionResult BookSearch(int? PageNumber, int? NumberPerPage, string Keyword)
        {

            var result = new List<BookDTO>();
          
            if (PageNumber == null && NumberPerPage == null)
            {
                PageNumber = 1;
                NumberPerPage = 6;
            }

            result = new DataAccess.DAOImpl.BookDAOImpl()
                .Books_SearchAndGetListByPage(PageNumber, NumberPerPage, Keyword.Trim());
            ViewBag.CurrentPage = PageNumber;
            ViewBag.NumberPerPage = NumberPerPage;
            ViewBag.EndPage = (new DataAccess.DAOImpl.BookDAOImpl().Book_Search(Keyword).Count) / NumberPerPage + 1;
            ViewBag.Keyword = Keyword;


            if (PageNumber > ViewBag.EndPage)
            {
                return HttpNotFound();
            }

            foreach (var item in result)
            {
                item.CategoryName = new DataAccess.DAOImpl.CategoryDAOImpl()
                    .Category_GetDetailByID(item.CategoryID).CategoryName;
            }

            return View(result);



        }
    }
}
