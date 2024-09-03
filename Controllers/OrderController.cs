using Eticaret.Entities;
using Eticaret2.Entities;
using Eticaret2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eticaret2.Controllers
{
    [Authorize(Roles ="admin")]
    public class OrderController : Controller
    {
        DataContext db = new DataContext();
        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Orders.Select(i => new AdminOrderModel()
            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Total = i.Total,
                Count = i.OrderLines.Count()
            }).OrderByDescending(i => i.OrderDate).ToList();

            return View(orders);
        }

        public ActionResult Details(int id)
        {
            var model = db.Orders.Where(i => i.Id == id)
                .Select(i => new OrderDetailsModel()
                {
                    OrderId = i.Id,
                    UserName = i.UserName,
                    OrderNumber = i.OrderNumber,
                    Total = i.Total,
                    OrderDate = i.OrderDate,
                    OrderState = i.OrderState,
                    AdresBasligi = i.AdresBasligi,
                    Adres = i.Adres,
                    Sehir = i.Sehir,
                    Semt = i.Semt,
                    Mahalle = i.Mahalle,
                    PostaKodu = i.PostaKodu,
                    OrderLines = i.OrderLines.Select(x => new OrderLineModel()
                    {
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                        Image = x.Product.Image,
                        Quantity = x.Quantity,
                        Price = x.Price
                    }).ToList()

                }).FirstOrDefault();
            return View(model);
        }

        public ActionResult UpdateOrderState(int OrderId, EnumOrderState OrderState) 
        {
            var order = db.Orders.FirstOrDefault(i=>i.Id == OrderId);

            if(order != null)
            {
                order.OrderState = OrderState;
                db.SaveChanges();
                TempData["Başarılı"] = "Bilgileriniz Kaydedildi.";

                return RedirectToAction("Details", new { id = OrderId });
            }


            return RedirectToAction("Index"); 
        }
    }
}