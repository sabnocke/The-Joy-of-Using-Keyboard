using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace fine_selection_of_sorts
{
    internal class Object
    {
        private Type obj;

        public Object(Type obj)
        {
            this.obj = obj;
        }
        public void Test()
        {
            Console.WriteLine();
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
            Console.WriteLine($"\nList of members:");
            foreach (MemberInfo member in obj.GetMembers(BindingFlags.Public | BindingFlags.Static))
            {
                Console.WriteLine($"{member.Name}:\n{member.MemberType}, {member.ReflectedType}");
            }
            Console.WriteLine();
            Console.WriteLine();
            string name = "measure";
            MethodInfo testedObj = obj.GetMethod($"{name}");
            ParameterInfo[] specObj = obj.GetMethod($"{name}").GetParameters();
            Console.WriteLine(
            $"{testedObj.Name}:"
            + $"\n return parameters: {testedObj.ReturnParameter}"
            + $"\n class declaring this member: {testedObj.DeclaringType}"
            + $"\n isStatic: {testedObj.IsStatic}"
            + $"\n arguments: ");
            printParams(specObj);
            Console.ReadLine();
        }
        public void printParams(ParameterInfo[] args)
        {
            foreach(ParameterInfo parameter in args)
            {
                Console.WriteLine($"  {parameter}");
            }
        }
     }
}
