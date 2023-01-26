using System.Reflection;
using System.Security.Policy;

namespace assembly_testground
{
    internal class Program
    {
        public void Cout(object? _string)
        {
            Console.WriteLine(_string);
        }
        static void Main(string[] args)
        {
            Example example = new();
            Object @object = new();
            Type Example = typeof(Example);
            Assembly assembly = typeof(Example).Assembly;
            Console.WriteLine(assembly.FullName);
            AppDomain appDomain = AppDomain.CurrentDomain;
            Assembly[] assemblies = appDomain.GetAssemblies();
            //foreach(Assembly assem in assemblies)
            //{
            //    Console.WriteLine(assem.ToString());
            //}
            
            @object.Test();
        }
    }
}