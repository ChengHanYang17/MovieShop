namespace ApplicationCore.Models;

public class MovieDetailsModel
{
    public MovieDetailsModel()
    {
        Genre = new List<GenreModel>();
        Trailer = new List<TrailerModel>();
        Cast = new List<CastModel>();
    }
    public int Id { get; set; }
    public string BackdropUrl { get; set; } = null!;
    public decimal? Budget { get; set; }
    public string ImdbUrl { get; set; } = null!;
    public string OriginalLanguage { get; set; } = null!;
    public string Overview { get; set; } = null!;
    public string PosterUrl { get; set; } = null!;
    public decimal? Price { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public decimal? Revenue { get; set; }
    public int? RunTime { get; set; }
    public string Tagline { get; set; } = null!;
    public string Title { get; set; }
    public string TmdbUrl { get; set; } = null!;

    public List<GenreModel> Genre { get; set; }
    public List<TrailerModel> Trailer { get; set; }
    public List<CastModel> Cast { get; set; }
}