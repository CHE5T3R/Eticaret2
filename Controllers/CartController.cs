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

    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        [Authorize]
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }

        public ActionResult AddToCart(int id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == id);

            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == id);

            if (product != null)
            {
                GetCart().RemoveProduct(product);
            }

            return RedirectToAction("Index");
        }

        public CartModel GetCart() // entity oluştur database den authorize a göre çek
        {
            var cart = (CartModel)Session["Cart"];

            if (cart == null)
            {
                cart = new CartModel();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }

        [Authorize]
        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ActionResult Checkout(ShippingDetails shippingdetails)
        {
            var cart = GetCart();
            if (cart.Cartlines.Count == 0)
            {
                ModelState.AddModelError("", "Sepetinizde ürün bulunmamaktadır.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Siparişi veritabanına kaydet
                    //cart ı sıfırla
                    SaveOrder(cart, shippingdetails);
                    cart.Clear();
                    return View("Completed");
                }
                else
                {
                    return View(shippingdetails);
                }
            }


            return View();
        }

        private void SaveOrder(CartModel cart, ShippingDetails shippingdetails)
        {
            var order = new Order();
            order.OrderNumber = "A" + (new Random()).Next(111111, 999999).ToString();
            order.Total = cart.Total();
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

            foreach (var item in cart.Cartlines)
            {
                var orderline = new OrderLine();
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