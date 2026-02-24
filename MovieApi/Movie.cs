using System.ComponentModel.DataAnnotations;

namespace MovieApi;

public class Movie
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;

    public int Year { get; set; }

    public string? Director { get; set; }
}
