using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Space.MovieSearcher.Domain;

namespace Space.MovieSearcher.Infrastructure.Configuration;

public class WatchlistMovieConfiguration : IEntityTypeConfiguration<WatchlistMovie>
{
    public void Configure(EntityTypeBuilder<WatchlistMovie> builder)
    {
        builder
            .HasKey(watchlistMovie => watchlistMovie.Id);

        builder
            .HasOne(watchlistMovie => watchlistMovie.Watchlist)
            .WithMany()
            .HasForeignKey(watchlistMovie => watchlistMovie.WatchlistId);
    }
}
