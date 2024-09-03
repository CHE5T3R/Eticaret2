using Eticaret.Entities;
using Eticaret2.Entities;
using Eticaret2.Identity;
using Eticaret2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Eticaret2.Controllers
{
    public class AccountController : Controller
    {
        private DataContext db = new DataContext();
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);

        }
        [Authorize]
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var orders = db.Orders
                .Where(i => i.UserName == username)
                .Select(i => new UserOrderModel()
                {
                    Id = i.Id,
                    OrderNumber = i.OrderNumber,
                    OrderDate = i.OrderDate,
                    Total = i.Total,
                    OrderState = i.OrderState
                }).OrderByDescending(i => i.OrderDate).ToList();


            return View(orders);
        }


        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                //Kayıt işlemleri

                var user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.SurName;
                user.Email = model.Email;
                user.UserName = model.UserName;

                int passwordControl = model.Password.Length;
                if (passwordControl < 6)
                {
                    TempData["Başarısız"] = "Şifre 6 haneden küçük olamaz";
                    return View(model);
                }
                var result = UserManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    //kullanıcı oluştu ve kullanıcıyı bir role atayabilirsin.
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    TempData["Başarısız"] = "Kayıt olma başarısız";
                }


            }
            return View(model);
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                    //Login işlemleri
                    var user = UserManager.Find(model.UserName, model.Password);
                    if (user != null)
                    {
                        // Varolan kullanıcıyı sisteme dahil et.
                        // ApplicationCookie oluşturup sisteme bırak.

                        var authManager = HttpContext.GetOwinContext().Authentication;
                        var identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                        var authProperties = new AuthenticationProperties();
                        authProperties.IsPersistent = model.RememberMe;
                        authManager.SignIn(authProperties, identityclaims);

                        if (!String.IsNullOrEmpty(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Başarısız"] = "Kullanıcı adı veya şifre hatalı";
                        ModelState.AddModelError("LoginUserError", "Böyle bir kullanıcı yok.");
                    }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index", "Home");
        }
        [Authorize]

        public ActionResult Details(int id)
        {
            var model = db.Orders.Where(i => i.Id == id)
                .Select(i => new OrderDetailsModel()
                {
                    OrderId = i.Id,
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

        public ActionResult Return(int id)
        {
            Order model = db.Orders.FirstOrDefault(i => i.Id == id);
            db.Orders.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}