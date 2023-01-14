namespace fine_selection_of_sorts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sort sort= new();
            int[] arr = { 8, 4, 1, 56, 3, -44, 23, -6, 28, 0 };
            Data data= new Data();
            sort.Call(arr, "bubblesort");
        }
    }
}