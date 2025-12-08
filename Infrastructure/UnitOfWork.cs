using Domain.Abstractions;
using Domain.Shared;

namespace Infrastructure
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
    	private readonly AppDbContext _dbContext;

    	public UnitOfWork(AppDbContext dbContext)
    	{
    		_dbContext = dbContext;
    	}

    	public async Task<Result<T>> SaveChangesAsync<T>(Result<T> result, CancellationToken cancellationToken = default)
    	{
    		if (result.IsFailure) return result;

    		await _dbContext.SaveChangesAsync(cancellationToken);
    		return result;
    	}

        public async Task<Result> SaveChangesAsync(Result result, CancellationToken cancellationToken = default)
        {
            if (result.IsFailure) return result;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
