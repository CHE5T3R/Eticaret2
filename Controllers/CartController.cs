using Eticaret.Entities;
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

        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }

        public ActionResult AddToCart(int id)
        {
            var product = db.Products.FirstOrDefault(i=>i.Id == id);

            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var product = db.Products.FirstOrDefault(i=>i.Id == id);

            if (product != null)
            {
                GetCart().RemoveProduct(product);
            }

            return RedirectToAction("Index");
        }
        
        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
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
            if(cart.Cartlines.Count==0)
            {
                ModelState.AddModelError("", "Sepetinizde ürün bulunmamaktadır.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Siparişi veritabanına kaydet
                    //cart ı sıfırla
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
    }
}