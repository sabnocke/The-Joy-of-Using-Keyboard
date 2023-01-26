using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembly_testground
{
    internal class Example
    {
        private int number;
        public Example(int number = 10, string @string = "abc", double @double = 1.0) { }
        public void Method(int number, string @string = "abc", double @double = 1.0)
        {
            this.number = number;
            Console.WriteLine($"Example.Method runs with: {number}");
        }
    }
}
