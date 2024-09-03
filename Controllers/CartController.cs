using Eticaret.Entities;
using Eticaret2.Entities;
using Eticaret2.Identity;
using Eticaret2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eticaret2.Controllers
{

    public class CartController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Cart
        public ActionResult Index()
        {
            var carts = db.Carts.Where(i => i.UserName == User.Identity.Name);


            return View(carts.ToList());
        }

        public ActionResult AddToCart(int id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == id);
            var carts = db.Carts.Where(i => i.UserName == User.Identity.Name);


            if (product != null)
            {
                if (carts.FirstOrDefault(i => i.ProductId == id) != null)
                {
                    foreach (var dbCarts in carts.Where(i => i.ProductId == id))
                    {
                        dbCarts.Quantity += 1;
                    }
                    db.SaveChanges();
                }
                else
                {
                    db.Carts.Add(new Cart()
                    {
                        UserName = User.Identity.Name,
                        ProductId = id,
                        Quantity = 1
                    });
                    db.SaveChanges();
                }

            }

            return RedirectToAction("Index");
        }

        public ActionResult DecreaseFromCart(int id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == id);
            var carts = db.Carts.Where(i => i.UserName == User.Identity.Name);

            if (product != null)
            {
                var cartline = carts.FirstOrDefault(i => i.ProductId == id);
                if (cartline.Quantity != 1)
                {
                    cartline.Quantity -= 1;
                    db.SaveChanges();
                }
                else
                {
                    if (cartline != null)
                    {
                        db.Carts.Remove(cartline);
                        db.SaveChanges();
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == id);
            var carts = db.Carts.Where(i => i.UserName == User.Identity.Name);

            if (product != null)
            {
                var cartline = carts.FirstOrDefault(i => i.ProductId == id);
                if (cartline != null)
                {
                    db.Carts.Remove(cartline);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public PartialViewResult Summary()
        {
            var summaryModel = new SummaryViewModel();
            summaryModel.Count = db.Carts.Where(i => i.UserName == User.Identity.Name).ToList().Count();
            return PartialView(summaryModel);
        }

        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(ShippingDetails shippingdetails)
        {
            var carts = db.Carts.Where(i=>i.UserName == User.Identity.Name);
            
            if (User.Identity.Name == null || User.Identity.Name == "")
            {
                carts = db.Carts.Where(i => i.UserName == shippingdetails.UserName);
            }
                
            var cartList = carts.ToList();
            if (carts.Count() == 0)
            {
                ModelState.AddModelError("", "Sepetinizde ürün bulunmamaktadır.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    SaveOrder(cartList, shippingdetails);


                    foreach (var removecart in cartList)
                    {
                        int id = removecart.Id;
                        Cart cart = db.Carts.Find(id);
                        db.Carts.Remove(cart);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Completed");
                }
                else
                {
                    return View(shippingdetails);
                }
            }


            return View();
        }

        public ActionResult Completed()
        {
            var order = db.Orders.OrderByDescending(p => p.Id).FirstOrDefault();
            ViewBag.OrderNumber = order.OrderNumber;
            ViewBag.OrderId = order.Id;
            return View();
        }

        private void SaveOrder(List<Cart> cartList, ShippingDetails shippingdetails)
        {
            var order = new Order();
            order.OrderNumber = "A" + (new Random()).Next(111111, 999999).ToString();

            order.Total = cartList.Sum(i => i.Quantity * i.Product.Price);

            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.Waiting;

            order.UserName = User.Identity.Name;
            order.AdresBasligi = shippingdetails.AdresBasligi;
            order.Adres = shippingdetails.Adres;
            order.Sehir = shippingdetails.Sehir;
            order.Semt = shippingdetails.Semt;
            order.Mahalle = shippingdetails.Mahalle;
            order.PostaKodu = shippingdetails.PostaKodu;

            order.OrderLines = new List<OrderLine>();

            foreach (var item in cartList)
            {
                var orderline = new OrderLine();
                orderline.OrderId = order.Id;
                orderline.Quantity = item.Quantity;
                orderline.Price = item.Product.Price * item.Quantity;
                orderline.ProductId = item.Product.Id;

                order.OrderLines.Add(orderline);
            }
            db.Orders.Add(order);
            db.SaveChanges();
        }
    }
}