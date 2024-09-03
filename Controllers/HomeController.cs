using Eticaret.Entities;
using Eticaret.Models;
using Eticaret2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace Eticaret.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            var items = db.Products
                .Where(i => i.IsHome && i.IsApproved)
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = i.Image ?? "1.jpg",
                    CategoryId = i.CategoryId
                })
                .ToList();

            return View(items);
        }
        public ActionResult Details(int id)
        {
            var model = new HomeDetailsViewModel
            {
                Product = db.Products.FirstOrDefault(x => x.Id == id),
                Commentlist = db.Comments.ToList()
            };

            return View(model);
        }
        public ActionResult List(int?id)
        {
            var items = db.Products
                .Where(i => i.IsApproved)
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = i.Image ?? "default.jpg",
                    CategoryId = i.CategoryId
                }).AsQueryable();
            if (id != null)
            {
                items = items.Where(i=>i.CategoryId == id);
            }

            return View(items.ToList());
        }
        public PartialViewResult GetCategories()
        {
            return PartialView(db.Categories.ToList());
        }
        public ActionResult AddComment(int id)
        {

            return RedirectToAction("Details");
        }
    }
}