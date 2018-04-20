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
                db.Database.ExecuteSqlCommand($"Insert Into ShopItem (name, Quantity) " +
                                            $"Values ('{shopItem.Name}', '{shopItem.Quantity}')");

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

                ShopItem shopItemToEdit = db.ShopItem
                       .FromSql($"Select * from ShopItem where Id = {id}")
                       .SingleOrDefault();

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
                db.Database.ExecuteSqlCommand($"Update ShopItem " +
                                             $"Set Item = '{shopItem.Name}', Quantity = '{shopItem.Quantity}' " +
                                              $"Where Id = {shopItem.Id} ");
                return RedirectToAction("Index");

            }
            return View(shopItem);
        }

        //------------------------------------------- Delete----------------------------------------//

        public IActionResult Delete(int? id)
        {

            if (id != null)
            {
                ShopItem shopItemToDelete = db.ShopItem
                    .FromSql($"Select * from ShopItem where Id = {id}")
                    .SingleOrDefault();


                if (shopItemToDelete != null)
                {
                    db.Database
                     .ExecuteSqlCommand($"Delete From ShopItem " +
                              $"Where Id = {id} ");
                }


            }
            return RedirectToAction("Index");
        }




        //------------------------------------------- Find----------------------------------------//


        public ViewResult Find(string item, int? aantal)
        {
            return View("Index",
                  db.ShopItem.FromSql(
                      $"Select * from ShopItem " +
                      $"where Name like '{item ?? "%"}%' "+
                      $"and Quantity like {aantal ?? byte.MaxValue}")
                  .ToList());
        }













    }
}