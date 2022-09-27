using Microsoft.EntityFrameworkCore;
using Space.MovieSearcher.Domain.Repositories;

namespace Space.MovieSearcher.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
