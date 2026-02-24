
using System.Text.Json;

namespace MovieApi.Service;

public class JsonRepository : IMovieRepository
{
    private const string filename = "movies.json";
    private readonly List<Movie> movies;

    public JsonRepository()
    {
        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            movies = JsonSerializer.Deserialize<List<Movie>>(json) ?? new List<Movie>();
        }
        else
        {
            movies = new List<Movie>()
            {
                new Movie() { Id=1, Title ="Jurrasic Park", Year =1993 },
                new Movie() { Id=2, Title ="Jurrasic World", Year =2025 },
                new Movie() { Id=3, Title ="Inception", Year =2010 },
                new Movie() { Id=4, Title ="Forrest Gump", Year =1993 },
                new Movie() { Id=5, Title ="Das Leben des Brian", Year =1979 }
            };
        }
    }

    public Movie AddMovie(Movie movie)
    {
        movie.Id = movies.Select(m => m.Id).Max() + 1;
        movies.Add(movie);
        Save();
        return movie;
    }

   

    public void DeleteMovie(int id)
    {
        var movie = GetMovieById(id);
        if (movie != null)
        {
            movies.Remove(movie);
            Save();
        }
    }

    public IEnumerable<Movie> GetAll()
    {
        return movies;
    }

    public Movie? GetMovieById(int id)
    {
        return movies.Find(m=>m.Id == id);
    }

    public Movie? GetMovieByName(string name)
    {
        return movies.Find(m=>m.Title.StartsWith(name));
    }

    public bool MovieExists(int id)
    {
        return movies.Any(m => m.Id == id);
    }

    public bool MovieExists(string title)
    {
        return movies.Any(m => m.Title.StartsWith(title));
    }

    public void UpdateMovie(Movie movie)
    {
        var orgMovie = GetMovieById(movie.Id);
        if (orgMovie != null)
        {
            orgMovie.Title = movie.Title;
            orgMovie.Year = movie.Year;
            Save();
        }
    }

    private void Save()
    {
        string json = JsonSerializer.Serialize(movies);
        File.WriteAllText(filename, json);
    }
}
