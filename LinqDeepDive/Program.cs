
namespace LinqDeepDive;

internal class Program
{
    static void Main(string[] args)
    {
        var movies = new List<Movie>()
        {
            new Movie() { Title ="Jurrasic Park", Year =1993 },
            new Movie() { Title ="Jurrasic World", Year =2025 },
            new Movie() { Title ="Inception", Year =2010 },
            new Movie() { Title ="Forrest Gump", Year =1993 },
            new Movie() { Title ="Das Leben des Brian", Year =1979 }
        };

        var newMovie = movies.Where(m => m.Year > 2000);

        var namedMethodMovies = movies.Where(AktuelleFilme);

        var newQueryMovies = from m in movies
                             where m.Year > 2000
                             select m;

        foreach (var movie in newQueryMovies)
        {
            Console.WriteLine(movie.Title);
        }

        Console.WriteLine("-------------------- meine Extension Methode -----------------------------------");

        var newMovie2 = movies.Filter(m => m.Year > 2000);

        foreach (var movie in newMovie2.Take(2))
        {
            Console.WriteLine(movie.Title);
        }

        foreach (var item in newMovie2.First().GetProperties().Take(2))
        {
            Console.WriteLine(item);
        }

        foreach (var item in MyExtensions.Random().Skip(10).Take(10))
        {
            Console.WriteLine(item);
        }
    }

    private static bool AktuelleFilme(Movie movie)
    {
        return movie.Year > 2000;
    }
}
