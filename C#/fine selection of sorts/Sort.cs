using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fine_selection_of_sorts
{
    internal class Sort
    {
        public Sort() { }

        public void Cout<T>(T rnd, bool line = true)
        {
            switch (line)
            {
                case true: Console.WriteLine(rnd); break;
                case false: Console.Write(rnd); break;
            }
            
        }
        
        public bool swap(int[] arr, int x, int y)
        {
            int temp = arr[x]; 
            arr[x] = arr[y];
            arr[y] = temp;
            return true;
        }

        public bool test_ascent(int[] arr)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > arr[i + 1]) { Cout<string>("False"); return false; }
            }
            return true;
        }

        public void Print(int[] arr)
        {
            foreach(int n in arr)
            {
                Cout<string>($"{n} ", false);
            }
            Cout<string>("");
        }

        public bool BubbleSort(int[] arr)
        {
            for(int i = 0; i < arr.Length - 1;i++)
            {
                for(int a = 0; a < arr.Length - 1; a++)
                {
                    if (arr[a]> arr[a + 1])
                    {
                        swap(arr, a, a + 1);
                    }
                }
            }
            return true;
        }

        public bool ShakerSort(int[] arr)
        {
            bool swapped = true;
            int start = 0;
            int end = arr.Length - 1;
            while (swapped)
            {
                swapped = false;
                for(int i = 0; i <= end - 1; i++) 
                {
                    if (arr[i] > arr[i + 1])
                    {
                        swap(arr, i , i + 1);
                        swapped = true;
                    }
                }
                if( swapped == false) { break; }
                end--;
                swapped = false;
                for(int i = end; i > start; i--)
                {
                    if (arr[i] < arr[i - 1])
                    {
                        swap(arr, i , i - 1);
                        swapped = true;
                    }
                }
                if(swapped == false) { break; }
                start++;

            }
            return true;
        }
    }
}
