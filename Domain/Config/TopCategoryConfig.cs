using Domain.Enums;

namespace Domain.Config
{
    public class TopCategoryConfig
    {
        public ItemCategory TopSellableCategory { get; set; } = ItemCategory.None;
        public decimal AveragePriceInTopCategory { get; set; }
    }
}