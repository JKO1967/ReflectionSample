
using SampleInterface;
using System.Reflection;

namespace ReflectionSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //LoadExternalAssembly();

            Person person = new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateofBirth = new DateOnly(1990, 1, 1)
            };

            Person? clonePerson = person.Clone() as Person;
            
            ReadTypeInfo(person);
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
