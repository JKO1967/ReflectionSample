
namespace MovieApi.Service;

public class EfMovieRepository : IMovieRepository
{
    
    private readonly MovieContext _movieContext;

    public EfMovieRepository(MovieContext context)
    {
        _movieContext = context;

        if (!_movieContext.Movies.Any())
        {
            context.Movies.AddRange(
                new Movie() { Title ="Jurrasic Park", Year =1993 },
                new Movie() { Title ="Jurrasic World", Year =2025 },
                new Movie() { Title ="Inception", Year =2010 },
                new Movie() { Title ="Forrest Gump", Year =1993 },
                new Movie() { Title ="Das Leben des Brian", Year =1979 }
            );
            context.SaveChanges();
        }
    }

    public Movie AddMovie(Movie movie)
    {
        _movieContext.Movies.Add( movie );
        _movieContext.SaveChanges();
        return movie;
    }

    public void DeleteMovie(int id)
    {
        var movie = GetMovieById(id);
        if (movie != null)
        {
            _movieContext.Movies.Remove( movie );
            _movieContext.SaveChanges();
        }
    }

    public IEnumerable<Movie> GetAll()
    {
        return _movieContext.Movies;
    }

    public Movie? GetMovieById(int id)
    {
        return _movieContext.Movies.FirstOrDefault( x => x.Id == id);
    }

    public Movie? GetMovieByName(string name)
    {
        return _movieContext.Movies.FirstOrDefault(m=>m.Title.StartsWith(name));
    }

    public bool MovieExists(int id)
    {
        return _movieContext.Movies.Any(x => x.Id == id);
    }

    public bool MovieExists(string title)
    {
        return _movieContext.Movies.Any(m => m.Title.StartsWith(title));
    }

    public void UpdateMovie(Movie movie)
    {
        var movieToUpdate = GetMovieById(movie.Id);
        if (movieToUpdate != null)
        {
            movieToUpdate.Title = movie.Title;
            movieToUpdate.Year = movie.Year;
            movieToUpdate.Director = movie.Director;
            _movieContext.SaveChanges();
        }
    }
}
