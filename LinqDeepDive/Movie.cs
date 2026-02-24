namespace LinqDeepDive;
public class Movie
{
    public string Title { get; set; } = string.Empty;

	private int _year;

	public int Year
	{
		get 
		{
            Console.WriteLine($"Year von {Title} wurde abgefragt!");
			return _year; 
		}
		set { _year = value; }
	}

	public IEnumerable<string> GetProperties()
	{
		yield return nameof(Title);
		yield return nameof(Year);
		yield return "Something to come in the future";
	}
}
