namespace MovieApi.Service;

public interface IMovieRepository
{
    IEnumerable<Movie> GetAll();

    Movie? GetMovieById(int id);

    Movie GetMovieByName(string name);

    bool MovieExists(int id);

    bool MovieExists(string title);

    Movie AddMovie(Movie movie);

    void UpdateMovie(Movie movie);

    void DeleteMovie(int id);


}
