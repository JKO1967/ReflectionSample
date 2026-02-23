
using ReflectionSample.Extensions;
using SampleInterface;
using System.Reflection;

namespace ReflectionSample
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            //LoadExternalAssembly();

            //Person person = new Person
            //{
            //    Id = 1,
            //    FirstName = "John",
            //    LastName = "Doe",
            //    DateofBirth = new DateOnly(1990, 1, 1)
            //};

            //// anonyme Methode
            //bool x = person.FirstName.Any(c => c == 'o');

            //person.LoggingDelegate += (m => { 
            //    Console.WriteLine($"[LOG] {m}");
            //    Console.WriteLine($"kommt aus der anonymen Funktion");
            //});
            //person.LoggingDelegate += LogClone;

            //person.LoggingAction = LogClone;

            //Person? clonePerson = person.Clone() as Person;

            //ReadTypeInfo(person);

            //ActionAndFuncSample();
            ExtensionSamples();
        }

        private static void ExtensionSamples()
        {
            List<int> list = new List<int>();
            for (int i = 1; i <= 20; i++)
            {
                list.Add(i);
            }
            
            Company company = new Company { Name = "Microsoft", Handelsregisterauszug = "HRB 12345" };
            company.WhatsMyName();

            Console.WriteLine(list.Sum());
            Console.WriteLine(list.Max());
            Console.WriteLine(list.Product());
        }

        private static void ActionAndFuncSample()
        {
            Func<int, int, double> mathFunction = Add;
            //Console.WriteLine(mathFunction(5, 10));
            mathFunction += Multiply;
            //Console.WriteLine(mathFunction(5, 10));
            mathFunction += (int a, int b) => a / b;
            //Console.WriteLine(mathFunction2(15, 0));
            //double result = mathFunction2(15, 0);
            foreach (var item in mathFunction.GetInvocationList())
            {
                Console.WriteLine(item.DynamicInvoke(5, 10));
            }


            Calculate(Add, 5, 10);
            Calculate(Multiply, 5, 10);
            // Console.WriteLine(mathFunction(5,10));
        }

        private static double Multiply(int arg1, int arg2)
        {
            return arg1 * arg2;
        }

        private static double Add(int arg1, int arg2)
        {
            return arg1 + arg2;
        }

        private static double Calculate(Func<int, int, double> mathFunc, int arg1, int arg2)
        {
            return mathFunc(arg1, arg2);
        }

        private static void LogClone(string message)
        {
            Console.WriteLine(message);
        }

        private static void ReadTypeInfo(Person person)
        {
            Type personType = person.GetType();
            //Type personType2 = typeof(Person);

            foreach (var item in personType.GetProperties())
            {
                Console.WriteLine($"{item.Name}: {item.GetValue(person)}");
            }

            // GetMethods gibt mir standardmäßig keine privaten Methoden zurück
            foreach (var item in personType.GetMethods())
            {
                Console.WriteLine($"{item.Name} (IsPublic: {item.IsPublic}, IsPrivate: {item.IsPrivate})");
            }

            foreach (var item in personType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Console.WriteLine($"{item.Name} (IsPublic: {item.IsPublic}, IsPrivate: {item.IsPrivate})");
                if (item.IsPrivate)
                {
                    Console.WriteLine(item.Invoke(person, []));
                }
            }
        }

        private static void LoadExternalAssembly()
        {
            // SampleLibrary muss manuell ins Applikationsverzeichnis kopiert werden, da kein Projektverweis besteht
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\SampleLibrary.dll";

            // Laden der Assembly
            Assembly sampleAssembly = Assembly.LoadFile(assemblyPath);

            Type? writerType = null;
            // MethodInfo? methodInfo = null;

            // Suche nach der Klasse ToUpperWriter und der Methode Write
            foreach (Type type in sampleAssembly.GetTypes())
            {
                Console.WriteLine(type.FullName);
                if (type.IsAssignableTo(typeof(ISampleWriter)))
                {
                    writerType = type;
                    break;
                }
                //if (type.Name == "ToUpperWriter")
                //{
                //    writerType = type;
                //    methodInfo = type.GetMethod("Write");
                //    break;
                //}
            }

            if (writerType != null) // && methodInfo != null)
            {
                ISampleWriter? instance = (ISampleWriter?)Activator.CreateInstance(writerType);

                if (instance!=null)
                {
                    Console.WriteLine(instance.Write("hello world"));
                }
                
                //Console.WriteLine(methodInfo.Invoke(instance, ["hello world"]));
            }
        }
    }
}
