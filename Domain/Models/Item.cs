using System.Text.Json.Serialization;
using Domain.Abstractions.Models;
using Domain.Enums;

namespace Domain.Models
{
    public class Item : Entity
    {
        public Item(string name, ItemCategory category, decimal price, int stock) : base()
        {
            Name = name;
            Category = category;
            Price = price;
            Stock = stock;
        }
        public Item(Guid id, string name, ItemCategory category, decimal price, int stock) : base(id)
        {
            Name = name;
            Category = category;
            Price = price;
            Stock = stock;
        }

        public string Name { get; set; } = string.Empty;
        public ItemCategory Category { get; private set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        [JsonIgnore]
        public List<Purchase> Purchases { get; set; } = [];

        private Item() { }
    }
}
