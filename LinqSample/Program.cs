

namespace LinqSample;

internal class Program
{
    static void Main(string[] args)
    {        
        List<Car> cars = ProcessCars("fuel.csv");
        List<Manufacturer> manufacturers = ProcessManufacturers("manufacturers.csv");

        Console.WriteLine($"Anzahl eingelesener Autos: {cars.Count()}");
        Console.WriteLine($"Anzahl eingelesener Hersteller: {manufacturers.Count()}");

        Console.WriteLine("---------------- Die 10 sparsamsten Autos ----------------");

        // Method Syntax
        var query = cars.OrderByDescending(c => c.Combined).ThenBy(c=>c.Name).Take(10);

        // Query Syntax
        var query2 = (from car in cars
                      orderby car.Combined descending, car.Name
                      select car).Take(10);

        foreach (var car in query)
        {
            Console.WriteLine($"{car.Manufacturer} {car.Name} ({car.Year}): {car.Combined} MPG");
        }

    }

    private static List<Manufacturer> ProcessManufacturers(string fileName)
    {
        return File.ReadAllLines(fileName)
            .Where(line => line.Length > 0)
            .Select(l =>
            {
                string[] columns = l.Split(',');

                return new Manufacturer
                {
                    Name = columns[0],
                    HeadQuarter = columns[1],
                    Year = int.TryParse(columns[2], out int result) ? result : 0
                };
            }).ToList();
    }

    private static List<Car> ProcessCars(string fileName)
    {
        return File.ReadLines(fileName)
            .Skip(1)
            .Where(line => line.Length > 0)
            .Select(l =>
            {
                string[] columns = l.Split(',');
                
                return new Car
                {
                    Year = int.TryParse(columns[0], out int result) ? result : 0,
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Combined = double.Parse(columns[7])
                };
            }).ToList();
    }
}
