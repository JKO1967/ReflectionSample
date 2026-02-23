namespace ReflectionSample.Extensions;
public static class MyExtensions
{
    public static long Product(this IEnumerable<int> sequence)
    {
        long result = 1;
        foreach (var item in sequence)
        {
            result *= item;
        }
        return result;
    }

    public static void WieIstDasWetterHeute(this object o)
    {
        Console.WriteLine("Es wird langsam wärmer, ber noch sehr regnerisch!");
    }

    public static string WhatsMyName(this IName o)
    {
        return $"My name is {o.Name}";
    }
}
