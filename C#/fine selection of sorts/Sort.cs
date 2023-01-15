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
        public TimeSpan timer;
        
        public Action<T> CreateDelegate<T>(string typeName, string methodName)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Assembly assembly= Assembly.GetExecutingAssembly();
            Type type= assembly.GetType(typeName ?? throw new ArgumentNullException(nameof(typeName)))!;
            MethodInfo methodInfo = type.GetMethod(methodName ?? throw new ArgumentNullException(nameof(methodName)))!;
            stopwatch.Stop();
            timer = stopwatch.Elapsed;
            return (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), methodInfo);
        }
    }
    
    internal class Sort : Data
    {
        public Sort() { }


        public void Call(double[] arr, string st)
        {
            if (st == "") { throw new Exception("Wrong input on string!"); }
            Action<double[]> myDelegate = CreateDelegate<double[]>(typeName: "fine_selection_of_sorts.Sort", methodName: $"{st}");
            TimeSpan time = measure(myDelegate, arr);
            Cout($"Runtime: {arr.Length} ints over {time.TotalMilliseconds}ms{Environment.NewLine}Creation of delegate: {timer.TotalMilliseconds} ms");
            
        }

        public TimeSpan measure(Action<double[]> func, double[] arr)
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

        public static void Cout(object? rnd, bool line = true)
        {
            switch (line)
            {
                case true: Console.WriteLine(rnd); break;
                case false: Console.Write(rnd); break;
            }
            
        }
        
        public static bool swap(double[] arr, int x, int y)
        {
            int temp = (int)arr[(int)x]; 
            arr[(int)x] = arr[(int)y];
            arr[(int)y] = temp;
            return true;
        }

        public bool test_ascent(int[] arr)
        {
            for(int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1]) { Cout("False"); return false; }
            }
            Cout("True");
            return true;
        }

        public void Print(int[] arr)
        {
            foreach(int n in arr)
            {
                Cout($"{n} ", false);
            }
            Cout("");
        }

        public static void bubblesort(double[] arr)
        {
            int turnCount = 0; int switchCount = 0;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int a = 0; a < arr.Length - 1; a++)
                {
                    if (arr[a] > arr[a + 1])
                    {
                        swap(arr, a, a + 1);
                        switchCount++;
                    }
                    turnCount++;
                }
            }
            Stats(turnCount, switchCount);
        }

        public static void shakersort(double[] arr)
        {
            int turnCount = 0; int switchCount = 0;
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
                        switchCount++;
                    }
                    turnCount++;
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
                        switchCount++;
                    }
                    turnCount++;
                }
                if(swapped == false) { break; }
                start++;
            }
            Stats(turnCount, switchCount);
        }

        public static void insersort(double[] arr)
        {
            int turnCount = 0; int switchCount = 0;
            int BinarySearch(double[] arr, int target, int low, int high)
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
            void insSort() {
                int lng = arr.Length;
                
                for (int i = 0; i < lng; i++)
                {
                    int j = i - 1;
                    int selected = (int)arr[(int)i];
                    int loc = BinarySearch(arr, selected, 0, j);
                    while( j >= loc)
                    {
                        arr[j + 1] = arr[j];
                        j--;
                        switchCount++;
                    }
                    arr[j + 1] = selected;
                    turnCount++;
                }
            }
            insSort();
            Stats(turnCount, switchCount);
        } // ma slaba stranka ; a jeste k tomu vsemu to ani nefunguje

        public static void SelectSort(double[] arr)
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
            int locOfSmallest(double[] arr, int loc, int lng)
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

        public static void MergeSort(double[] arr)
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
                double[] L = new double[dex1];
                double[] R = new double[dex2];
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

        public static void quicksort(double[] arr)
        {
            int turnCount = 0; int switchCount = 0;
            int partition(double[] arr, int start, int end)
            {
                int mid = (start + end) / 2;
                int pivot = mid;
                while (start < end)
                {
                    while (arr[start] < arr[mid]) { start++; }
                    while (arr[end] > arr[mid]) { end--; }
                    while (start <= end)
                    {
                        swap(arr, start, end);
                        switchCount++;
                        start++;
                        end--;
                    }
                    turnCount++;
                }
                return mid;
            }
            void qSort(double[] arr, int start, int end)
            {
                int midPoint = partition(arr, start, end);
                if (start < midPoint) { qSort(arr, start, midPoint); }
                if (end > midPoint) { qSort(arr, midPoint + 1, end); }
            }
            int start = 0; int end = arr.Length - 1;
            qSort(arr,start,end);
            Stats(turnCount, switchCount);
        }

        public static void combsort(double[] arr)
        {
            Stopwatch stopwatch = new Stopwatch();
            int turnCount = 0; int switchCount = 0;
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
                            switchCount++;
                            swapped = true;
                        }
                        turnCount++;
                    }
                }
            }
            //stopwatch.Start();
            cSort();
            //stopwatch.Stop();
            //TimeSpan time = stopwatch.Elapsed;
            //Cout(time.TotalMilliseconds);
            Stats(turnCount, switchCount);
        }

        public static void pigeonhole(double[] arr)
        {
            int turnCount = 0; int switchCount = 0;
            double max = arr.Max();
            double min = arr.Min();
            double range = max - min + 1;
            double[] filler()
            {
                double[] holes = new double[(int)range];
                for(int i = 0; i < range - 1; i++)
                {
                    holes = holes.Append(0).ToArray();
                }
                return holes;
            }
            void PHS()
            {
                double[] holes = filler();
                foreach(double n in arr)
                {
                    holes[(int)(n - min)]++;
                }
                int i = 0;
                for(int count = 0; count < range; count++) {
                    while (holes[count] > 0)
                    {
                        holes[count]--;
                        arr[i] = count + min;
                        i++;
                        switchCount++;
                    }
                    turnCount++;
                }
            }
            PHS();
            Stats(turnCount, switchCount);
        } // haha, holub
        
    }
}
