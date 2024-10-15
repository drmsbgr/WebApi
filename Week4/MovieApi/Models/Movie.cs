
namespace MovieApi.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public decimal Duration { get; set; }

    public Movie()
    {

    }

    public Movie(string title, decimal duration)
    {
        this.Id = Movies.Max(m => m.Id) + 1;
        this.Title = title;
        this.Duration = duration;
    }

    private readonly static List<Movie> _movies = [
        new Movie() { Id = 1, Title = "Buz Devri", Duration = 120 },
        new Movie() { Id = 2, Title = "Interstaller", Duration = 140 },
        new Movie() { Id = 3, Title = "Joker", Duration = 170 },
        new Movie() { Id = 4, Title = "Avengers : End Game", Duration = 160 }
        ];
    public static List<Movie> Movies => _movies;

    public static void EditMovie(Movie edited)
    {
        var found = Movies.Find(x => x.Id.Equals(edited.Id));
        if (found is not null)
        {
            found.Title = edited.Title;
            found.Duration = edited.Duration;
        }
        else
        {
            //bulunamadı
        }
    }

    public static void RemoveMovieById(int id) => Movies.RemoveAll(x => x.Id.Equals(id));
    public static void AddMovie(Movie movie) => Movies.Add(movie);
}