using Domain.Config;

namespace Domain.Abstractions
{
    public interface ITopCategoryCalculator
    {
        Task<TopCategoryConfig> CalculateTopCategoryAsync();
    }
}
