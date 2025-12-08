using Domain.Abstractions.Models;
using Domain.Shared;
namespace Domain.Errors
{
	/// <summary>
	/// Класс для обозначения ошибок уровня работы с базой данных. Сюда могут входить
	/// как логические ошибки (например обращение к некорректным данным), так и другие
	/// (например уведомление о том что почта пользователя может быть занята)
	/// </summary>
	public static class PersistenceErrors
	{
        public static class Item
        {
            public static readonly Error NotFound = new(
                $"{typeof(Item).Name}.NotFound",
                $"Предмет не найден");
            public static readonly Error NotEnoughInStock = new(
                $"{typeof(Item).Name}.NotEnoughInStock",
                $"Недостаточно товара");
        }
    }
}
