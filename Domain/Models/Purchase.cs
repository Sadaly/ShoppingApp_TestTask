using System.Text.Json.Serialization;
using Domain.Abstractions.Models;

namespace Domain.Models
{
    public class Purchase : Entity
    {
        [JsonIgnore]
        public Item Item { get; set; }
        public Guid ItemId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; } = 0;

        public Purchase(Item item, decimal price, int stock) : base()
        {
            ItemId = item.Id;
            Item = item;
            Price = price;
            Stock = stock;
        }

        private Purchase() { }
    }
}
