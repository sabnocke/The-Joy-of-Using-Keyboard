using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace assembly_testground
{
    internal class Object
    {
        Type obj = typeof(Object)!;
        public void Test()
        {
            Console.WriteLine("Assembly: " + obj.Assembly.FullName); // get assembly and it's full name
            Console.WriteLine("Base Type: " + obj.BaseType.FullName); // get base type and it's full name
            Console.WriteLine("IsEnum: " + obj.IsEnum); // Is current type enum
            Console.WriteLine("IsInterface: " + obj.IsInterface); // Is current type interface
            Console.WriteLine("IsNested: " + obj.IsNested); // Is current type nested class
            Console.WriteLine("IsNotPublic: " + obj.IsNotPublic); // Is current type not public
            Console.WriteLine("IsPublic: " + obj.IsPublic); // Is current type public
            Console.WriteLine("IsSealed: " + obj.IsSealed); // Is current type sealed
            Console.WriteLine("IsValueType: " + obj.IsValueType); // Is current type value type
            Console.WriteLine("Namespace: " + obj.Namespace); // current type namespace
            Console.WriteLine("Constructors: " + obj.GetConstructors().Count()); // current type constructors
            Console.WriteLine("Default Constructor: " + obj.GetConstructor(new Type[] { }).Name); // current type constructors
            Console.WriteLine("Events: " + obj.GetEvents().Count()); // current type events
            //Console.WriteLine("Specific Event: " + obj.GetEvent("MyEvent").Name); // current type specific event
            Console.WriteLine("Fields: " + obj.GetFields().Count()); // current type field
            //Console.WriteLine("Specific Field: " + obj.GetField("MyField").Name); // current type specific field
            Console.WriteLine("Members: " + obj.GetMembers().Count()); // current type members
            Console.WriteLine("Specific Member: " + obj.GetMember("MyField")); // current type specific member
            Console.WriteLine("Properties: " + obj.GetProperties().Count()); // current type properties
            //Console.WriteLine("Specific Property: " + obj.GetProperty("MyProperty").Name); // current type specific property
            Console.WriteLine("Methods: " + obj.GetMethods().Count()); // current type methods
            //Console.WriteLine("Specific Method: " + obj.GetMethod("Method1").Name); // current type specific method
            Console.WriteLine();
            
            foreach (MemberInfo member in obj.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)) {
                Console.WriteLine($"{member.Name}:{member.MemberType}");
            }
            Console.WriteLine();
            foreach (ConstructorInfo constructor in obj.GetConstructors())
            {
                Console.WriteLine($"{constructor.Name}:\n\t{constructor.CallingConvention}\n\t{constructor.DeclaringType}\n\t{constructor.Attributes}");
                Console.WriteLine();
            }
            
            Console.ReadLine();
        }
    }
}
