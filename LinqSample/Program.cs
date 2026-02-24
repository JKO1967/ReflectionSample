

using System.Diagnostics;
using System.Text;

namespace LinqSample;

internal class Program
{
    static void Main(string[] args)
    {
        //StringKonkatenation();

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

        Console.WriteLine("---------------- Die 5 sparsamsten BMWs ----------------");

        // Method Syntax
        query = cars.Where(c => c.Manufacturer.Equals("BMW", StringComparison.InvariantCultureIgnoreCase))
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name)
                .Take(5);

        // Query Syntax
        // Voricht bei ToUpper, da eine Kopie des Strings für jedes Element in der Liste gemacht wird - Performancemäßig ungünstig
        // bessere Lösung siehe Method Syntax
        query2 = (from car in cars
                  where car.Manufacturer.ToUpper() == "BMW"
                  orderby car.Combined descending, car.Name
                  select car).Take(5);

        Console.WriteLine("---------------- Der erste Audi in der Liste ----------------");

        Car? audi = cars.FirstOrDefault(c => c.Manufacturer.Equals("Audi", StringComparison.InvariantCultureIgnoreCase));
        // wenn es keinen Audi gibt dann wird eine NullReferenceException geworfen
        Car audi2 = cars.First(c => c.Manufacturer.Equals("Audi", StringComparison.InvariantCultureIgnoreCase));

        // Find ist mit .NET 6 optimiert worden und ist seitdem performanter als FirstOrDefault
        Car? audi3 = cars.Find(c => c.Manufacturer.Equals("Audi", StringComparison.InvariantCultureIgnoreCase));

        // gibt den ersten gefundenen zurück, oder wenn er keinen findet null,
        // aber wenn es mehr als einen Audi gibt, dann wird eine InvalidOperationException geworfen
        try
        {
            Car? audi4 = cars.SingleOrDefault(c => c.Manufacturer.Equals("Audi", StringComparison.InvariantCultureIgnoreCase));
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Es gibt mehr wie einen Audi");
        }

        try
        {
            Car? audi5 = cars.Single(c => c.Manufacturer.Equals("Audi", StringComparison.InvariantCultureIgnoreCase));
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Es gibt mehr wie einen Audi");
        }

        Console.WriteLine("---------------- Gibt es einen VW in der Liste ----------------");

        bool hasVW = cars.Any(c => c.Manufacturer.Equals("VW", StringComparison.InvariantCultureIgnoreCase));

        Console.WriteLine("---------------- wir joinen ----------------");
        // Liste der 10 sparsamsten Autos (Ausgabe Name, Verbrauch, Headquarter)    

        // MethodSyntax
        var joinQuery = cars.OrderByDescending(c => c.Combined)
            .ThenBy(c => c.Name)
            .Take(10)
            .Join(manufacturers, c => c.Manufacturer, m => m.Name, (c, m) => new
            {
                c.Name,
                Verbrauch = c.Combined,
                Land = m.HeadQuarter
            });

        // QuerySyntax - nicht performant da Take erst am Ende und somit erstmal alles gejoined wird
        //var joinQuery2 = (from car in cars
        //                  orderby car.Combined descending, car.Name
        //                  join manufacturer in manufacturers on car.Manufacturer equals manufacturer.Name
        //                  select new
        //                  {
        //                      car.Name,
        //                      Verbrauch = car.Combined,
        //                      Land = manufacturer.HeadQuarter
        //                  }).Take(10);

        // Dank deferred Execution kann ich mir aber die Statements zusammensetzen
        var joinQuery2 = (from car in cars
                          orderby car.Combined descending, car.Name
                          select car)
                          .Take(10);

        var joinResult = from c in joinQuery2
                         join m in manufacturers
                         on c.Manufacturer equals m.Name
                         select new
                         {
                             c.Name,
                             Verbrauch = c.Combined,
                             Land = m.HeadQuarter
                         };

        foreach (var item in joinQuery)
        {
            Console.WriteLine($"{item.Name} aus {item.Land} hat einen Verbrauch von {item.Verbrauch}");
        }

        Console.WriteLine("---------------- wir joinen aber jetzt mit 2 Schlüsseln ----------------");
        // Liste der 10 sparsamsten Autos (Ausgabe Name, Verbrauch, Headquarter)    

        // MethodSyntax
         joinQuery = cars.OrderByDescending(c => c.Combined)
            .ThenBy(c => c.Name)
            .Take(10)
            .Join(manufacturers, 
                c => new { c.Manufacturer, c.Year }, 
                m => new { Manufacturer = m.Name, m.Year },   // bitte darauf achten dass die Properties in beiden anonymen Typen gleich heißen
                (c, m) => new
            {
                c.Name,
                Verbrauch = c.Combined,
                Land = m.HeadQuarter
            });

         joinQuery2 = (from car in cars
                          orderby car.Combined descending, car.Name
                          select car)
                          .Take(10);

         joinResult = from c in joinQuery2
                         join m in manufacturers
                         on new { c.Manufacturer, c.Year } 
                         equals new { Manufacturer = m.Name, m.Year }
                         select new
                         {
                             c.Name,
                             Verbrauch = c.Combined,
                             Land = m.HeadQuarter
                         };

        foreach (var item in joinQuery)
        {
            Console.WriteLine($"{item.Name} aus {item.Land} hat einen Verbrauch von {item.Verbrauch}");
        }

        // anonyme Typen
        var kurs = new { Name = "Linq Kurs", Dauer = 4, Preis = 399.99 };

        Console.WriteLine("-------------------------- Wir gruppieren -------------------------------");
        Console.WriteLine("-------------------------- Liste aller Hersteller aus der Liste Cars ----------------");

        var distinctQuery = cars.Select(c => c.Manufacturer).Distinct();

        var distinctQuery2 = (from c in cars
                              select c.Manufacturer).Distinct();

        foreach (var item in distinctQuery)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("-------------------------- Group Join Alle Hersteller mit allen Autos -------------------------------");

        var groupJoinQuery = manufacturers
            .GroupJoin(cars, m=>m.Name, c=> c.Manufacturer,
            (m, c) => new { Manufacturer = m, Cars = c})
            .OrderBy(m=>m.Manufacturer.Name).ToList();

        var groupJoinQuery2 = from m in manufacturers
                              join car in cars
                              on m.Name equals car.Manufacturer
                              into carGroup
                              orderby m.Name
                              select new
                              {
                                  Manufacturer = m,
                                  Cars = carGroup
                              };

        foreach (var item in groupJoinQuery)
        {
            Console.WriteLine($"{item.Manufacturer.Name} - {item.Manufacturer.HeadQuarter}");
            foreach (var car in item.Cars)
            {
                Console.WriteLine($"\t\t{car.Name}");
            }
        }

        Console.WriteLine("-----------------------------  Liste aller Hersteller mit den 2 sparsamsten Autos -----------------");

        var groupQuery = cars
            .GroupBy(c => c.Manufacturer)
            .OrderBy(g => g.Key);

        var groupQuery2 = from c in cars
                          orderby c.Manufacturer
                          group c by c.Manufacturer;

        foreach (var item in groupQuery)
        {
            Console.WriteLine(item.Key);
            foreach (var car in item.OrderBy(c=>c.Combined).Take(2))
            {
                Console.WriteLine($"\t\t{car.Name} - {car.Combined}");
            }
        }

        Console.WriteLine("-------------------------------- Select Many --------------------------------------");

        var germanCars = groupJoinQuery.Where(m => m.Manufacturer.HeadQuarter == "Germany")
            .SelectMany(m => m.Cars);
    
        foreach (var item in germanCars)
        {
            Console.WriteLine(item.Name);
        }
    }

    private static void StringKonkatenation()
    {
        string wetter = "Es wird wärmer, aber leider regnet es den ganzen Tag.";
        string result = "";
        Stopwatch stopwatch = Stopwatch.StartNew();
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < 4_000_000; i++)
        {
            //result += wetter;
            stringBuilder.Append(wetter);
        }
        result = stringBuilder.ToString();
        stopwatch.Stop();
        Console.WriteLine($"Das hat {stopwatch.Elapsed.TotalSeconds} gedauert");
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
