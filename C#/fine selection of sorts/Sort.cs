using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace fine_selection_of_sorts
{
    internal class Data
    {
        public Action<T> CreateDelegate<T>(string typeName, string methodName)
        {
            Assembly assembly= Assembly.GetExecutingAssembly();
            Type type= assembly.GetType(typeName ?? throw new ArgumentNullException(nameof(typeName)))!;
            MethodInfo methodInfo = type.GetMethod(methodName ?? throw new ArgumentNullException(nameof(methodName)))!;
            return (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), methodInfo);
        }
    }
    
    internal class Sort : Data
    {
        public Sort() { }

        public void Call(int[] arr, string st)
        {
            if (st == "") { throw new Exception("Wrong input on string!"); }
            Action<int[]> myDelegate = CreateDelegate<int[]>(typeName: "fine_selection_of_sorts.Sort", methodName: $"{st}");
            TimeSpan time = measure(myDelegate, arr);
            Cout($"Runtime: {arr.Length} ints over {time.TotalMilliseconds}ms");
        }

        public TimeSpan measure(Action<int[]> func, int[] arr)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            func(arr);
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        public static void Stats(int turnCount, int switchCount)
        {
            Cout($"Turn Count: {turnCount}");
            Cout($"Switch Count: {switchCount}");
        }

        public static void Cout<T>(T rnd, bool line = true)
        {
            switch (line)
            {
                case true: Console.WriteLine(rnd); break;
                case false: Console.Write(rnd); break;
            }
            
        }
        
        public static bool swap(int[] arr, int x, int y)
        {
            int temp = arr[x]; 
            arr[x] = arr[y];
            arr[y] = temp;
            return true;
        }

        public bool test_ascent(int[] arr)
        {
            for(int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1]) { Cout<string>("False"); return false; }
            }
            Cout<string>("True");
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

        public static void bubblesort(int[] arr)
        {
            int turnCount = 0; int swithCount = 0;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int a = 0; a < arr.Length - 1; a++)
                {
                    if (arr[a] > arr[a + 1])
                    {
                        swap(arr, a, a + 1);
                        swithCount++;
                    }
                    turnCount++;
                }
            }
            Stats(turnCount, swithCount);
        }

        public static void shakersort(int[] arr)
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
                        swap(arr, i, i + 1);
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
                        swap(arr, i, i - 1);
                        swapped = true;
                    }
                }
                if(swapped == false) { break; }
                start++;

            }
        }

        public static void InsertSort(int[] arr)
        {
            int BinarySearch(int[] arr, int target, int low, int high)
            {
                while(low <= high)
                {
                    int mid = (low + high) / 2;
                    if(target == arr[mid])
                    {
                        return mid + 1;
                    }
                    else if (target > arr[mid])
                    {
                        low = mid + 1;
                    }
                    else
                    {
                        high = mid - 1;
                    }
                }
            return low;
            }
            bool insSort() {
                int lng = arr.Length;
                for(int i = 0; i < lng; i++)
                {
                    int j = i - 1;
                    int selected = arr[i];
                    int loc = BinarySearch(arr, selected, 0, j);
                    while( j >= loc)
                    {
                        arr[j + 1] = arr[j];
                        j--;
                    }
                    arr[j + 1] = selected;
                }
                return true;
            }
            insSort();
        } // ma slaba stranka

        public static void SelectSort(int[] arr)
        {
            bool slcSort()
            {
                int loc = 0;
                int lng = arr.Length;
                while(loc <= lng - 1)
                {
                    int j = locOfSmallest(arr, loc, lng - 1);
                    swap(arr, loc, j);
                    loc++;
                }
                return true;
            }
            int locOfSmallest(int[] arr, int loc, int lng)
            {
                int i = loc;
                int j = i;
                while(i <= lng)
                {
                    if (arr[i] < arr[j])
                    {
                        j = i;
                    }
                    i++;
                }
                return j;
            }
            slcSort();
        } // ma slaba stranka #2

        public static void MergeSort(int[] arr)
        {
            void mrgSort(int start, int end)
            {
                if(start < end)
                {
                    int mid = (start + end) / 2;
                    mrgSort(start, mid);
                    mrgSort(mid + 1, end);
                    merge(mid, start, end);
                }
            }
            void merge(int mid, int start, int end)
            {
                int dex1 = mid - start + 1;
                int dex2 = end - mid + 1;
                int[] L = new int[dex1];
                int[] R = new int[dex2];
                int flg, flg2;
                for(flg = 0; flg < dex1; ++flg)
                {
                    L[flg] = arr[start + flg];
                }
                for(flg2 = 0; flg2 < dex2; ++flg2)
                {
                    R[flg2] = arr[mid + 1 + flg2];
                }
                flg = 0; flg2 = 0;
                int k = 1;
                while(flg < dex1 && flg < dex2)
                {
                    if (L[flg] > R[flg2])
                    {
                        arr[k] = L[flg];
                        flg++;
                    }
                    else
                    {
                        arr[k] = R[flg2];
                        flg2++;
                    }
                    k++;
                }
                while(flg < dex1)
                {
                    arr[k] = L[flg];
                    flg++; k++;
                }
                while(flg < dex2)
                {
                    arr[k] = R[flg];
                    flg2++; k++;
                }

            }
            int start = 0; int end = arr.Length - 1;
            mrgSort(start, end);
        } // dear god

        public static void QuickSort(int[] arr)
        {
            int partition(int[] arr, int start, int end)
            {
                int mid = (start + end) / 2;
                int pivot = mid;
                while (start < end)
                {
                    while (arr[start] < arr[mid]) { start++; }
                    while (arr[end] > arr[mid]) { end--; }
                    while (start < end)
                    {
                        int temp = arr[start];
                        arr[start] = arr[end];
                        arr[end] = temp;
                    }
                }
                return mid;
            }
            void qSort(int[] arr, int start, int end)
            {
                int midPoint = partition(arr, start, end);
                if (start < midPoint) { qSort(arr, start, midPoint); }
                if (end > midPoint) { qSort(arr, midPoint + 1, end); }
            }
            int start = 0; int end = arr.Length;
            qSort(arr,start,end);
        }

        public static void CombSort(int[] arr)
        {
            int getnextgap(int gap)
            {
                gap = (gap * 10) / 13;
                if(gap <= 1) { return 1; }
                return gap;
            }
            void cSort()
            {
                int len = arr.Length;
                int gap = len;
                bool swapped = true;
                while(gap != 1 && swapped)
                {
                    gap = getnextgap(gap);
                    swapped = false;
                    for(int i = 0; i < (len - gap); i++)
                    {
                        if (arr[i] > arr[i + gap])
                        {
                            swap(arr, i, i + gap);
                            swapped = true;
                        }
                    }
                }
            }
            cSort();
        }

        public static void PigeonHoleSort(int[] arr)
        {
            int[] filler()
            {
                int max = arr.Max();
                int min = arr.Min();
                int range = max - min + 1;
                int[] holes = new int[range];
                for(int i = 0; i < range - 1; i++)
                {
                    holes = holes.Append(0).ToArray();
                }
                return holes;
            }
            void PHS()
            {
                int max = arr.Max();
                int min = arr.Min();
                int range = max - min + 1;
                int[] holes = filler();
                foreach(int n in arr)
                {
                    holes[n - min]++;
                }
                int i = 0;
                for(int count = 0; count < range; count++) {
                    while (holes[count] > 0)
                    {
                        holes[count]--;
                        arr[i] = count + min;
                        i++;
                    }
                }
            }
            PHS();
        } // haha, holub
        
    }
}
