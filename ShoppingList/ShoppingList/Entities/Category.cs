using System;
using System.Collections.Generic;

namespace ShoppingList.Entities
{
    public partial class Category
    {
        public Category()
        {
            ShopItem = new HashSet<ShopItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ShopItem> ShopItem { get; set; }
    }
}
