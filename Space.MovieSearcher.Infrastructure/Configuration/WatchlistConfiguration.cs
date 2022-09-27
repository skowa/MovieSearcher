using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Space.MovieSearcher.Domain;

namespace Space.MovieSearcher.Infrastructure.Configuration;

public class WatchlistConfiguration : IEntityTypeConfiguration<Watchlist>
{
    public void Configure(EntityTypeBuilder<Watchlist> builder)
    {
        builder
            .HasKey(watchlist => watchlist.Id);
    }
}
