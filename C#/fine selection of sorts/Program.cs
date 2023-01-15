namespace fine_selection_of_sorts
{
    internal class Program
    {
        public static int[] rndArray(int length = 10, int min = 1, int max = 100)
        // generates array of random numbers
        // measured inception: (ms)(0,1972; 0,153; 0,1117)
        {
            Random rnd = new Random();
            int[] array = new int[length];
            for (int a = 0; a < length; a++)
            {
                array[a] = rnd.Next(min, max);
            }
            return array;
        }
        public static void printArray(int[] arr)
        {
            foreach(int n in arr)
            {
                Console.Write($"{n} ");
            }
        }
        public static double[] filler(double[] arr, double length = 10, int min = 1, int max = 100)
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
            int[] arr = { 8, 4, 1, 56, 3, -44, 23, -6, 28, 0 };
            double i = 1E+05; int max = 10000; int min = -10000;
            double[] firstArr = new double[(int)i];
            filler(firstArr, length: i, max: max, min: min);
            //printArray(firstArr); Console.WriteLine(); printArray(secondArr); Console.WriteLine(); printArray(thirdArr); Console.WriteLine();
            sort.Call(firstArr, "quicksort");
            

        }

        
    }
}