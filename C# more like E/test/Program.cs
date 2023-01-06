using System.Diagnostics;

namespace test
{
    internal class Program
    {
        public static List<int> rndList(int n = 10)
        // generates list of random numbers
        // measured inception: (ms)(8,9894; 0,0182; 0,0108)
        {
            Random rnd = new Random();
            List<int> list = new List<int>(Enumerable.Range(1, n).Select(i => rnd.Next())); return list;
        }

        public static int[] rndArray(int length = 10)
        // generates array of random numbers
        // measured inception: (ms)(0,1972; 0,153; 0,1117)
        {
            Random rnd = new Random();
            int[] array = new int[length];
            for(int a = 0; a < length; a++)
            {
                array[a] = rnd.Next();
            }
            return array;
        }
        
        public static void print(List<int> list)
        // prints array/list
        {
            Console.WriteLine($"{string.Join(',', list)}");
        }
        
        public static void swap(List<int> list, int ix_1, int ix_2)
        // generic swap function
        {
            (list[ix_1], list[ix_2]) = (list[ix_2], list[ix_1]);
        }
        
        public static void bubbleSort(List<int> list)
        // bubble sort
        {
            int length = list.Count();
            for(int a = 0; a < length; a++)
            {
                for(int b = 0; b < length - 1; b++)
                {
                    if (list[b] > list[b + 1]) { swap(list, b, b + 1); }
                }
            }
        }

        static int partition(List<int> arr, int one, int two)
        {
            int rnd1 = one, rnd2 = two;
            int mid = (one + two) / 2;
            while (rnd1 < rnd2)
            {
                while (arr[rnd1] < arr[mid]) { rnd1++; }
                // preskoci vsechno mensi nez mid z leva
                while (arr[rnd2] > arr[mid]) { rnd2--; }
                // preskoci vsechno vetsi nez mid z prava
                while (rnd1 <= rnd2)
                {
                    swap(arr, rnd1, rnd2);
                    rnd1++;
                    rnd2--;
                }
            }
            return rnd1;
        }
        static void quicksort(List<int> arr, int one, int two)
        {
            int rnd_value = partition(arr, one, two);
            if (one < rnd_value - 1) { quicksort(arr, one, rnd_value - 1); }
            if (two > rnd_value) { quicksort(arr, rnd_value, two); }
        }
        public static TimeSpan measure(Action<List<int>> ancestor, int arg = 10)
        // should measure time neccessary for completion of ancestor
        {
            Stopwatch stopwatch = new Stopwatch();
            List<int> list = new List<int>();
            list.AddRange(rndList(arg));
            stopwatch.Start();
            ancestor(list);
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }


        public static TimeSpan measure_adv(Action<List<int>, int, int> ancestor, int first, int last, List<int> list)
        // should measure time neccessary for completion of ancestor, but more advanced
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ancestor(list, first, last);
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        static void Main(string[] args)
        {
            Stopwatch stopwatch= new Stopwatch();
            Action<List<int>> descendentOfBubble = bubbleSort;
            Action<List<int>, int, int> descendentOfQuick = quicksort;
            List<int> list = new List<int>();
            list.AddRange(rndList(200000));
            int first = 0; int last = list.Count - 1;
            TimeSpan time = measure(descendentOfBubble, 200000);
            TimeSpan time2 = measure_adv(descendentOfQuick, first, last, list);
            //stopwatch.Start();
            //rndList(1000000);
            //stopwatch.Stop();
            //TimeSpan timed = stopwatch.Elapsed;
            //stopwatch.Restart();
            //rndArray(1000000);
            //stopwatch.Stop();
            //TimeSpan timed2= stopwatch.Elapsed;
            Console.WriteLine($"Time elapsed bubble sort: {time.TotalMilliseconds} {Environment.NewLine}Time elapsed quick sort: {time2.TotalMilliseconds}");
            //Console.WriteLine($"Time elapsed rndList: {timed.TotalMilliseconds}{Environment.NewLine}Time elapsed rndArray: {timed2.TotalMilliseconds}");
            //Console.WriteLine($"Comparison timed2 to timed: {timed2.CompareTo(timed)}");
            //Console.WriteLine($"If -1 timed2 < timed; if 0 timed2 == timed; if 1 timed2 > timed");
        }
    }
}