using System;
using System.Collections.Generic;

namespace ShoppingList.Entities
{
    public partial class ShopItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Quantity { get; set; }
        public int? CategoryId { get; set; }
        public int? CartId { get; set; }

        public Cart Cart { get; set; }
        public Category Category { get; set; }
    }
}
