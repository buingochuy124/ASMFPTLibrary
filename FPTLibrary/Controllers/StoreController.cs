using DataAccess.DTO;
using FPTLibrary.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTLibrary.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
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
                    if (userSession.RoleID != 3)
                    {
                        return RedirectToAction("DoNotHavePermission", "Home");
                    }
                    else
                    {
                        ViewBag.userName = userSession.UserFullName;
                        return View();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult BookStorePartialView(int? PageNumber, int? NumberPerPage)
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
                    if (userSession.RoleID != 3)
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

                        var result = new DataAccess.DAOImpl.BookDAOImpl().Books_GetListByPage(PageNumber, NumberPerPage);
                        ViewBag.CurrentPage = PageNumber;
                        ViewBag.NumberPerPage = NumberPerPage;
                        ViewBag.EndPage = (new DataAccess.DAOImpl.BookDAOImpl().Books_GetList().Count) / NumberPerPage + 1;
                        if (PageNumber > ViewBag.EndPage)
                        {
                            return HttpNotFound();
                        }
                        foreach (var item in result)
                        {
                            item.CategoryName = new DataAccess.DAOImpl.CategoryDAOImpl()
                                .Category_GetDetailByID(item.CategoryID).CategoryName;
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

        public ActionResult StoreOrder()
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
            var result = new List<FPTLibrary.ViewModels.SellerOrderVM>();
            try
            {
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }
                else
                {
                    if (userSession.RoleID != 3)
                    {
                        return RedirectToAction("DoNotHavePermission", "Home");
                    }
                    else
                    {

                        var storeID = new DataAccess.DAOImpl.StoreDAOImpl().Store_GetStoreByUser(userSession.UserID).StoreID;
                        var listBookOfStore = new DataAccess.DAOImpl.BookDAOImpl().Books_GetList().Where(b => b.StoreID == storeID).ToList();

                        var ListOrder = new DataAccess.DAOImpl.OrderDetaiDAOlImpl().OrderDetail_GetList();

                        foreach (var item in ListOrder)
                        {
                            var book = new DataAccess.DAOImpl.BookDAOImpl().Book_GetDetail(item.BookISBN);
                            if (book.StoreID == storeID)
                            {
                                result.Add(new FPTLibrary.ViewModels.SellerOrderVM
                                {
                                    Book = book,
                                    Quantity = item.Quantity,
                                    UserID = new DataAccess.DAOImpl.OrderDAOImpl().Order_GetOrderByID(item.OrderID).UserID
                                });
                            }

                        }

                        result.ForEach(r => r.UserFullName = new DataAccess.DAOImpl.UserDAOImpl().Users_GetList()
                                 .FirstOrDefault(u => u.UserID == r.UserID)
                                 .UserFullName);

                        result.ForEach(r => r.UserAddress = new DataAccess.DAOImpl.UserDAOImpl().Users_GetList()
                                 .FirstOrDefault(u => u.UserID == r.UserID)
                                 .UserAddress);

                        return View(result.OrderBy(u => u.UserID).ToList());

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult BookDelete(long BookISBN)
        {
            try
            {
                var bookDelete = new DataAccess.DAOImpl.BookDAOImpl().Book_Delete(BookISBN);

                return RedirectToAction("Index", "Store");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Store");
            }

        }
        public JsonResult ImportBookByExel()
        {
            var index_fail = "";
            var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];

            var returnData = new ReturnData();
            try
            {
                var storeID = new DataAccess.DAOImpl.StoreDAOImpl().Store_GetStoreByUser(userSession.UserID).StoreID;
                HttpPostedFileBase excelFile = Request.Files["ExcelFile"];
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                if (excelFile == null)
                {
                    returnData.ResponseCode = -3;
                    returnData.Description = "File Empty";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                var package = new ExcelPackage(excelFile.InputStream);

                ExcelWorksheet ws = package.Workbook.Worksheets[0];

                for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                {

                    if (ws.Cells[rw, 1].Value != null
                        && ws.Cells[rw, 2].Value != null
                        && ws.Cells[rw, 3].Value != null
                        && ws.Cells[rw, 4].Value != null
                        && ws.Cells[rw, 5].Value != null
                        && ws.Cells[rw, 6].Value != null
                        && ws.Cells[rw, 7].Value != null
                        && ws.Cells[rw, 8].Value != null
                       )

                    {
                        //1
                        var bookISBN = ws.Cells[rw, 1].Value != null ? ws.Cells[rw, 1].Value.ToString() : string.Empty;
                        //2
                        var title = ws.Cells[rw, 2].Value != null ? ws.Cells[rw, 2].Value.ToString() : string.Empty;
                        //3
                        var author = ws.Cells[rw, 3].Value != null ? ws.Cells[rw, 3].Value.ToString() : string.Empty;
                        //4
                        var categoryName = ws.Cells[rw, 4].Value != null ? ws.Cells[rw, 4].Value.ToString() : string.Empty;
                        //5
                        var bookPages = ws.Cells[rw, 5].Value != null ? ws.Cells[rw, 5].Value.ToString() : string.Empty;
                        //6
                        var bookCost = ws.Cells[rw, 6].Value != null ? ws.Cells[rw, 6].Value.ToString() : string.Empty;
                        //7
                        var bookDescription = ws.Cells[rw, 7].Value != null ? ws.Cells[rw, 7].Value.ToString() : string.Empty;
                        //8
                        var bookImageURL = ws.Cells[rw, 8].Value != null ? ws.Cells[rw, 8].Value.ToString() : string.Empty;

                        var createCategory = new DataAccess.DAOImpl.CategoryDAOImpl().Category_Create(categoryName);
                        var categoryID = new DataAccess.DAOImpl.CategoryDAOImpl().Category_GetDetailByName(categoryName).CategoryID;
                        var result = new DataAccess.DAOImpl.BookDAOImpl()
                            .Book_SellBook(long.Parse(bookISBN), title, author, double.Parse(bookCost),
                            int.Parse(bookPages), categoryID, bookDescription, bookImageURL, storeID);

                        var err_des = "";
                        try
                        {
                            if (result == 0)
                            {
                                err_des = "Book Already exist";

                            }
                            else if (result < 0)
                            {
                                err_des = "Add book fail";
                            }

                            else
                            {
                                index_fail = string.Empty;
                            }

                            index_fail += err_des;


                        }
                        catch (System.Exception)
                        {

                            throw;
                        }


                    }
                }

                if (string.IsNullOrEmpty(index_fail))
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "Insert File book Sucessfully";
                    return Json(returnData, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    returnData.ResponseCode = -1;
                    // returnData.Description = "System Bussy";
                    returnData.Description = index_fail;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }


            }
            catch (System.Exception)
            {
                throw;
            }
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