using DataAccess.DTO;
using FPTLibrary.Models;
using System;
using System.Web.Mvc;

namespace FPTLibrary.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult OrderRecord()
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
                        if (userSession.RoleID == 3)
                        {
                            var result = new DataAccess.DAOImpl.OrderDAOImpl().Orders_GetListByUser(userSession.UserID);
                            return View(result);

                        }
                        return RedirectToAction("DoNotHavePermission", "Home");
                    }
                    else
                    {
                        var result = new DataAccess.DAOImpl.OrderDAOImpl().Orders_GetListByUser(userSession.UserID);

                        return View(result);

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult OrderDetail(int OrderID, DateTime Date)
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

                        var orderDetail = new DataAccess.DAOImpl.OrderDetaiDAOlImpl().OrderDetail_GetOrderDetail(OrderID);

                        var result = new DataAccess.DAOImpl.OrderDAOImpl().Order_GetOrderID(userSession.UserID, Date);


                        result.ListOrderDetail = orderDetail;
                        foreach (var item in result.ListOrderDetail)
                        {
                            item.BookCost = new DataAccess.DAOImpl.BookDAOImpl()
                                .Book_GetDetail(item.BookISBN)
                                .Cost;

                            item.BookName = new DataAccess.DAOImpl.BookDAOImpl()
                                .Book_GetDetail(item.BookISBN)
                                .BookName;
                            
                            result.Total += item.Quantity * item.BookCost;


                        }
                        
                        return View(result);


                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}