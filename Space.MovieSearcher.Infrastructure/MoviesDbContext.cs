using Microsoft.EntityFrameworkCore;

namespace Space.MovieSearcher.Infrastructure;

public class MoviesDbContext : DbContext
{
    public MoviesDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoviesDbContext).Assembly);
}
