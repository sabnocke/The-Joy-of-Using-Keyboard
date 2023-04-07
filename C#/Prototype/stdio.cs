using System;
using System.Collections;

namespace Prototype
{
    class stdio
    {
        public void cout(dynamic item = null, string end = " ")
        {
            if (item == null)
            {
                Console.WriteLine();
            }
            else if (item is IEnumerable && !(item is string))
            {
                foreach (var @char in item)
                {
                    Console.Write($"{@char}{end}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(item);
            }

        }
    }
}
