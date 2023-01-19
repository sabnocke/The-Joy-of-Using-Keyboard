namespace fine_selection_of_sorts
{
    internal class Program
    {
        public static void printArray(double[] arr)
        {
            foreach(int n in arr)
            {
                Console.Write($"{n} ");
            }
        }
        public static int[] filler(int[] arr, double length = 10, int min = 1, int max = 100)
        {
            Random rnd = new Random();
            for (int a = 0; a < length; a++)
            {
                arr[a] = rnd.Next(min, max);
            }
            return arr;
        }

        static void Main(string[] args)
        {
            Sort sort = new();
            SortV2 sortin = new();
            int[] arr = { 8, 4, 1, 56, 3, -44, 23, -6, 28, 0 };
            int i = 10; int max = 10000; int min = -10000;
            int[] firstArr = new int[i];
            int[] secondArr = new int[i];
            filler(firstArr, length: i, max: max, min: min);
            filler(secondArr, length: i, max: max, min: min);
            sort.Call(firstArr, "mergesort");
            sortin.Run(secondArr);

        }

        
    }
}