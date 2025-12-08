using Domain.Shared;
namespace Domain.Errors
{
	public static class ApplicationErrors
	{
        public static class Item
        {
            public static readonly Error StockInvalid = new(
                $"{typeof(Item).Name}.StockInvalid",
                $"Невозможно использовать отрицательные значения для учёта количества товара");
            public static readonly Error PriceInvalid = new(
                $"{typeof(Item).Name}.PriceInvalid",
                $"Невозможно поставить цену в 0 и ниже");
            public static readonly Error NothingChanged = new(
                $"{typeof(Item).Name}.NothingChanged",
                $"Отправленный запрос никак не изменяет поля предмета");
        }
        public static class Purchase
        {
            public static readonly Error QuantityInvalid = new(
                $"{typeof(Purchase).Name}.QuantityInvalid",
                $"Невозможно взять ");
        }
    }
}
