namespace Space.MovieSearcher.Application.Options;

public class EmailOfferOptions
{
    public int OfferDelayInDays { get; set; }

    public string ReceivingEmail { get; set; }

    public int MinimumUnwatchedMoviesAmount { get; set; }
}
