
using System.Reflection;

namespace ReflectionSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LoadExternalAssembly();   
        }

        private static void LoadExternalAssembly()
        {
            // SampleLibrary muss manuell ins Applikationsverzeichnis kopiert werden, da kein Projektverweis besteht
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\SampleLibrary.dll";

            // Laden der Assembly
            Assembly sampleAssembly = Assembly.LoadFile(assemblyPath);

            Type? writerType = null;
            MethodInfo? methodInfo = null;

            // Suche nach der Klasse ToUpperWriter und der Methode Write
            foreach (Type type in sampleAssembly.GetTypes())
            {
                Console.WriteLine(type.FullName);
                if (type.Name == "ToUpperWriter")
                {
                    writerType = type;
                    methodInfo = type.GetMethod("Write");
                    break;
                }
            }

            if (writerType != null && methodInfo != null)
            {
                var instance = Activator.CreateInstance(writerType);
                Console.WriteLine(methodInfo.Invoke(instance, ["hello world"]));
            }


        }
    }
}
