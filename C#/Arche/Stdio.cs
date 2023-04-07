using System;
using System.Collections;


namespace Arche
{
    public static class Standard
    {
        /// <summary>
        /// Better Console.Write(-line)
        /// </summary>
        /// <param name="item">variable that should be printed</param>
        /// <param name="end">what should line end with</param>
        public static void Cout(dynamic item = null, string end = " ")
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
                Console.WriteLine($"{item}{end}");
            }
        }

        
    }

    public static class Compare
    {
        public static bool Within<T>(this T value, T lower, T upper, bool inclusive = false)
            where T : IComparable<T>
        {
            switch (inclusive)
            {
                case false:
                    if (value.CompareTo(lower) < 0) return false;
                    if (value.CompareTo(upper) > 0) return false;
                    return true;
                case true:
                    if (value.CompareTo(lower) <= 0) return false;
                    if (value.CompareTo(upper) >= 0) return false;
                    return true;
                default: // this doesn't need to be here
                    return false;
            }
        }
    }
}
