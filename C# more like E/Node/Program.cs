using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Node
{
    internal class Program
    {
        /*
         * Co to vlastne umi?
         * 
         * .Push = push
         * .Peak = peak
         * .Print = print
         * .Pop = pop
         * .integCck = overi jestli na sebe vsechno navazuje
         * Sorty: .callQuicksort = quicksort ; .bubble_sort = bubblesort
         * (lepsi zpusob nemam)
         * 
         * .GenerateLst = generuje list nahodnych cisel a pak je vlozi do stacku
         * .GenerateArr = --||-- ale s array (technicky rychlejsi)
         * (parametry to upravuji)
         */
        static void Main(string[] args)
        {                               // pro lepsi orientaci
            Node node = new();          // bubble sort, List
            Node secondNode = new();    // quicksort, List
            Node thirdNode = new();     // bubblesort, array
            Node fourthNode = new();    // quicksort, list

            Func<bool, bool> method = node.bubble_sort;
            Func<bool, bool> methodQuick = secondNode.callQuicksort;
            Func<bool, bool> methodArrBubble = thirdNode.bubble_sort;
            Func<bool, bool> methodArrQuick = fourthNode.callQuicksort;
            //Never seen a delegate???

            node.GenerateLst(max_value: 500000,min_value:1);
            Console.WriteLine($"Runtime: {measure(method).TotalMilliseconds} ms");
            
            secondNode.GenerateLst(max_value:500000);
            Console.WriteLine($"Runtime: {measure(methodQuick).TotalMilliseconds} ms");
            
            thirdNode.GenerateArr(max_value:500000);
            Console.WriteLine($"Runtime: {measure(methodArrBubble,true).TotalMilliseconds} ms");
            
            thirdNode.GenerateArr(max_value:500000);
            Console.WriteLine($"Runtime: {measure(methodArrQuick,true).TotalMilliseconds} ms");
        }
        
        public static TimeSpan measure(Func<bool, bool> method, bool sw = false)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            method(sw);
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}