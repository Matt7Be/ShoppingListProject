using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Entities;
using ShoppingList.Models;

namespace ShoppingList.Controllers
{
    public class ShoppingController : Controller
    {

        // Scaffold-DbContext "Data Source=DESKTOP-E4NU1GG;Initial Catalog=master;Integrated Security=True;" Microsoft.EntityFrameworkCore.sqlServer -OutputDir Entities -Context ShoppingContext


        ShoppingContext db = new ShoppingContext();

        //------------------------------------------- Index----------------------------------------//

        public IActionResult Index()
        {
            return View(db.ShopItem.Select(s=>s).ToList());

        }

        //------------------------------------------- Create----------------------------------------//
        [HttpGet]
        public IActionResult Create()
        {

            return View(new ShopItem());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ShopItem shopItem)
        {

            if (ModelState.IsValid)
            {
                db.ShopItem.Add(shopItem);
                db.SaveChanges();


                return RedirectToAction("index");

            }


            else
            {
                return View(shopItem);
            }

        }

        //------------------------------------------- Edit----------------------------------------//



        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id != null)
            {

                ShopItem shopItemToEdit = db.ShopItem.Where(s => s.Id == id).Select(s => s).SingleOrDefault();

                if (shopItemToEdit != null)
                {
                    return View(shopItemToEdit);
                }
            }

            return View("Error", new ErrorViewModel());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ShopItem shopItem)
        {

            if (ModelState.IsValid)
            {
                db.ShopItem.Update(shopItem);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(shopItem);
        }

        //------------------------------------------- Delete----------------------------------------//

        public IActionResult Delete(int? id)
        {

            if (id != null)
            {
                ShopItem shopItemToDelete = db.ShopItem.Where(s => s.Id == id).Select(s => s).SingleOrDefault();


                if (shopItemToDelete != null)
                {
                    db.ShopItem.Remove(shopItemToDelete);
                    db.SaveChanges();

                }


            }
            return RedirectToAction("Index");
        }




        //------------------------------------------- Find----------------------------------------//


        public ViewResult Find(string item, int? aantal)
        {
            return View("Index",
                  db.ShopItem
                  .Where(s => s.Name.StartsWith(item ?? "") && s.Quantity <= (aantal ?? byte.MaxValue))
                  .Select(s => s)
                  .ToList());
        }



        //------------------------------------------- Details----------------------------------------//


        [HttpGet]
        public IActionResult Details(int? id)
        {

            if (id != null)
            {

                ShopItem shopItemToDetails = db.ShopItem.Where(s => s.Id == id).Select(s => s).SingleOrDefault();

                if (shopItemToDetails != null)
                {
                    return View(shopItemToDetails);
                }
            }

            return View("Error", new ErrorViewModel());

        }












    }
}