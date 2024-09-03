using Eticaret.Entities;
using Eticaret2.Entities;
using Eticaret2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eticaret2.Controllers
{
    public class FavoritesController : Controller
    {
        private DataContext db = new DataContext();
        
        // GET: Favorites
        public ActionResult Index()
        {
            var favorite = db.Favorites.Where(i=>i.UserName==User.Identity.Name);


            return View(favorite.ToList());
        }
        [Authorize]
        public ActionResult AddToFavorite(int id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == id);
            var favorite = db.Favorites.Where(i => i.UserName == User.Identity.Name);
            
            if (product != null)
            {
                if (favorite.FirstOrDefault(i => i.ProductId == id) != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    db.Favorites.Add(new Favorite()
                    {
                        UserName = User.Identity.Name,
                        ProductId = id
                    });
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromFavorite(int id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == id);
            var favorite = db.Favorites.Where(i => i.UserName == User.Identity.Name);

            if (product != null)
            {
                var favoriteline = favorite.FirstOrDefault(i => i.ProductId == id);
                if (favoriteline != null)
                {
                    db.Favorites.Remove(favoriteline);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
        public PartialViewResult FavoritesIcon()
        {
            var favoritemodel = new FavoriteViewModel();
            favoritemodel.Count = db.Favorites.Where(i => i.UserName == User.Identity.Name).ToList().Count();
            return PartialView(favoritemodel);
        }
    }
}