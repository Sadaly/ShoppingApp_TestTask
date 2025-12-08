using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Config;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Service.Config
{
    public class TopCategoryConfigProvider : IOptionsMonitor<TopCategoryConfig>, ITopCategoryCalculator
    {
        private TopCategoryConfig _current = new();
        private readonly IServiceScopeFactory _scopeFactory;

        public TopCategoryConfig CurrentValue => _current;
        public TopCategoryConfig Get(string? name) => _current;
        public IDisposable OnChange(Action<TopCategoryConfig, string> listener) => NullDisposable.Instance;

        public TopCategoryConfigProvider(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<TopCategoryConfig> CalculateTopCategoryAsync()
        {
            using var scope = _scopeFactory.CreateScope();

            var _itemRepository = scope.ServiceProvider.GetRequiredService<IItemRepository>();
            var _purchaseRepository = scope.ServiceProvider.GetRequiredService<IPurchaseRepository>();

            var purchases = await _purchaseRepository.GetAllAsync();
            if (purchases.Count == 0)
            {
                _current = new TopCategoryConfig { TopSellableCategory = ItemCategory.None, AveragePriceInTopCategory = 0 };
                return new();
            }

            // Группируем по категориям и считаем выручку
            var categoryRevenue = purchases
                .GroupBy(p => p.Item.Category)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Price));

            var topCategory = categoryRevenue
                .OrderByDescending(kvp => kvp.Value)
                .First().Key;

            // Считаем среднюю цену в топовой категории
            var items = (await _itemRepository.GetAllAsync())
                .Where(i => i.Category == topCategory)
                .ToList();

            var avgPrice = items.Count != 0 
                ? items.Average(i => i.Price) 
                : 0;

            // Обновляем конфигурацию
            _current = new TopCategoryConfig
            {
                TopSellableCategory = topCategory,
                AveragePriceInTopCategory = avgPrice
            };

            return _current;
        }

        private class NullDisposable : IDisposable
        {
            public static NullDisposable Instance = new();
            public void Dispose() { }
        }
    }
}