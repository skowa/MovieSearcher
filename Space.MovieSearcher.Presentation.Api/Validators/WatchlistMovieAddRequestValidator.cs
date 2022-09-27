using FluentValidation;
using Space.MovieSearcher.Presentation.Api.Models;

namespace Space.MovieSearcher.Presentation.Api.Validators;

public class WatchlistMovieAddRequestValidator : AbstractValidator<WatchlistMovieAddRequest>
{
    public WatchlistMovieAddRequestValidator()
    {
        RuleFor(request => request.UserId).GreaterThan(0);
        RuleFor(request => request.MovieId).NotNull().NotEmpty();
    }
}
