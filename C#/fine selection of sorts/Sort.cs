using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace fine_selection_of_sorts
{
    internal class Data
    {   
        /// <summary>
        /// Generuje delegaty, ktere nic nevraci
        /// </summary>
        /// <typeparam name="T">typ argumentu</typeparam>
        /// <param name="typeName">nacita classu v ramci namespace (musi byt presne)</param>
        /// <param name="methodName">nacita metodu z dane classy, pokud soubezne prijima stejne argumenty pro jake byl delegat vytvoren</param>
        /// <returns>vraci delegat pro danou metodu</returns>
        /// <exception cref="ArgumentNullException">null handling</exception>
        public Action<T> CreateDelegate<T>(string typeName, string methodName)
        {
            Assembly assembly= Assembly.GetExecutingAssembly();
            // vraci assembly ekvivalent prave spousteneho kodu
            Type type = assembly.GetType(typeName ?? throw new ArgumentNullException(nameof(typeName)))!;
            // nacita typ z hodnoty stringu
            MethodInfo methodInfo = type.GetMethod(methodName ?? throw new ArgumentNullException(nameof(methodName)))!;
            // nacita funkci, predanou jako string, z classy v type
            return (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), methodInfo);
            // vraci delegate typu T, ktery deleguje metodu jmenem methodInfo
        }
    }
    
    internal class Sort : Data
    {
        public Sort() { }


        /// <summary>
        /// Jediny funkcni zpusob jak zavolat jakykoliv sort, v ramci teto metody
        /// </summary>
        /// <param name="arr">pole k setrideni</param>
        /// <param name="st">Bere pouze specificke hodnoty/nazvy sortu</param>
        /// <exception cref="Exception"> pouze pokud je string prazdny, pro zbytek se mi to nechtelo delat</exception>
        public void Call(int[] arr, string st)
        {
            if (st == "") { throw new Exception("Wrong input on string!"); }
            Action<int[]> myDelegate = CreateDelegate<int[]>(typeName: "fine_selection_of_sorts.Sort", methodName: $"{st}");
            // viz. class Data
            TimeSpan time = measure(myDelegate, arr);
            // generic funkce, ktera bere delegata a meri jeho runtime
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

        public static void Cout(object? rnd, bool line = true)
        // Cout == Console.Writeline()/Console.Write; pro line lidi
        {
            switch (line)
            {
                case true: Console.WriteLine(rnd); break;
                case false: Console.Write(rnd); break;
            }
            
        }
        
        public static bool swap(int[] arr, int x, int y)
        {
            (arr[x], arr[y]) = (arr[y], arr[x]);
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
            foreach(double n in arr)
            {
                Cout($"{n} ", false);
            }
            Cout("");
        }

        public static void bubblesort(int[] arr)
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

        public static void shakersort(int[] arr)
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

        public static void insertsort(int[] arr)
        {
            int turnCount = 0; int switchCount = 0;
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
            void insSort() {
                int lng = arr.Length;
                
                for (int i = 0; i < lng; i++)
                {
                    int j = i - 1;
                    int selected = arr[i];
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
        } 

        public static void selectsort(int[] arr)
        {
            int turnCount = 0, switchCount = 0;
            bool slcSort()
            {
                int loc = 0;
                int lng = arr.Length;
                while(loc <= lng - 1)
                {
                    int j = locOfSmallest(arr, loc, lng - 1);
                    swap(arr, loc, j);
                    loc++;
                    switchCount++;
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
                    turnCount++;
                }
                return j;
            }
            slcSort();
            Stats(turnCount, switchCount);
        } 

        public static void mergesort(int[] arr)
        {
            int turnCount = 0, switchCount = 0;
            void merge(int[] arr, int left, int mid, int right)
            {
                int n1 = mid - left + 1;
                int n2 = right - mid;
                int[] L = new int[n1];
                int[] R = new int[n2];
                int i, j;
                for (i = 0; i < n1; ++i)
                    L[i] = arr[left + i];
                for (j = 0; j < n2; ++j)
                    R[j] = arr[mid + 1 + j];
                i = 0; j = 0;
                int k = left;
                while (i < n1 && j < n2)
                {
                    if (L[i] <= R[j])
                    {
                        arr[k] = L[i]; i++;
                        switchCount++;
                    }
                    else
                    {
                        arr[k] = R[j]; j++;
                        switchCount++;
                    }
                    k++;
                    turnCount++;
                }
                while (i < n1)
                {
                    arr[k] = L[i];
                    i++; k++;
                    switchCount++;
                }
                while (j < n2)
                {
                    arr[k] = R[j];
                    j++; k++;
                    switchCount++;
                }
            }
            void sort(int[] arr, int left, int right)
            {
                if (left < right)
                {
                    int mid = left + (right - left) / 2;
                    sort(arr, left, mid);
                    sort(arr, mid + 1, right);
                    merge(arr, left, mid, right);
                }
            }
            sort(arr, 0, arr.Length - 1);
            Stats(turnCount, switchCount);
        }

        public static void quicksort(int[] arr)
        {
            int turnCount = 0; int switchCount = 0;
            int partition(int[] arr, int left, int right)
            {
                int dexL = left, dexR = right;
                double pivot = arr[(left + right) / 2];
                while (dexL <= dexR)
                {
                    while (arr[dexL] < pivot)
                        dexL++; turnCount++;
                    while (arr[dexR] > pivot)
                        dexR--; turnCount++;
                    if (dexL <= dexR)
                    {
                        swap(arr, dexL, dexR);
                        switchCount++;
                        dexL++;
                        dexR--;
                    }
                }
                return dexL;
            }
            void qSort(int[] arr, int start, int end)
            {
                int midPoint = partition(arr, start, end);
                if (start < midPoint - 1) { qSort(arr, start, midPoint - 1); }
                if (midPoint < end) { qSort(arr, midPoint, end); }
            }
            int start = 0; int end = arr.Length - 1;
            qSort(arr,start,end);
            Stats(turnCount, switchCount);
        }

        public static void pigeonhole(int[] arr)
        {
            int turnCount = 0; int switchCount = 0;
            int max = arr.Max();
            int min = arr.Min();
            int range = max - min + 1;

            void PHS()
            {
                int[] holes = new int[range];
                for (int a = 0; a < range - 1; a++)
                {
                    holes = holes.Append(0).ToArray();
                }
                foreach (double n in arr)
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
        } 
        
    }

    internal class SortV2
    {
        public SortV2() { }
        public void Cout(object? input)
        {
            Console.WriteLine(input);
        }

        public void Swap(int[] arr, int index, int index2)
        {
           ( arr[index], arr[index2] )= ( arr[index2], arr[index] );
        }

        public void BubbleSort(int[] arr)
        {
            int turnCount = 0, switchCount = 0;
            int lastUnsorted = arr.Length - 1;
            Stopwatch stopwatch = Stopwatch.StartNew();
            for(int i = 0; i < lastUnsorted; i++) {
                for( int a = 0; a < lastUnsorted; a++)
                {
                    if (arr[a] > arr[a + 1])
                    {
                        Swap(arr, a, a + 1);
                        switchCount++;
                    }
                    turnCount++;
                }
                lastUnsorted--;
            }
            stopwatch.Stop();
            Stats(turnCount, switchCount, stopwatch.Elapsed);
        }

        public void ShakerSort(int[] arr)
        {
            int turnCount = 0, switchCount = 0;
            bool swapped = true;
            int start = 0; int end = arr.Length - 1;
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (swapped)
            {
                swapped = false;
                for(int i = start; i < end; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        Swap(arr, i, i + 1);
                        swapped = true;
                        switchCount++;
                    }
                    turnCount++;
                }
                if(swapped == false) { break; }
                end--;
                swapped = false;
                for(int i = end; i > start; i--)
                {
                    if (arr[i] < arr[i - 1]) { 
                        Swap(arr, i, i - 1);
                        swapped = true;
                        switchCount++;
                    }
                    turnCount++;
                }
                if(swapped == false) { break; }
                start++;
            }
            stopwatch.Stop();
            Stats(turnCount, switchCount, stopwatch.Elapsed);
        }

        public void InsertSort(int[] arr)
        {
            int turnCount = 0, switchCount = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            int BinarySearch(int[] arr, int target, int low, int high)
            {
                while (low <= high)
                {
                    int mid = (low + high) / 2;
                    if (target == arr[mid])
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
            void insSort()
            {
                int lng = arr.Length;
                for (int i = 0; i < lng; i++)
                {
                    int j = i - 1;
                    int selected = (int)arr[(int)i];
                    int loc = BinarySearch(arr, selected, 0, j);
                    while (j >= loc)
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
            stopwatch.Stop();
            Stats(turnCount, switchCount, stopwatch.Elapsed);
        }

        public void SelectSort(int[] arr)
        {
            int turnCount = 0, switchCount = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool slcSort()
            {
                int loc = 0;
                int lng = arr.Length;
                while (loc <= lng - 1)
                {
                    int j = locOfSmallest(arr, loc, lng - 1);
                    Swap(arr, loc, j);
                    loc++;
                    switchCount++;
                }
                return true;
            }
            int locOfSmallest(int[] arr, int loc, int lng)
            {
                int i = loc;
                int j = i;
                while (i <= lng)
                {
                    if (arr[i] < arr[j])
                    {
                        j = i;
                    }
                    i++;
                    turnCount++;
                }
                return j;
            }
            slcSort();
            stopwatch.Stop();
            Stats(turnCount, switchCount, stopwatch.Elapsed);
        }

        public void MergeSort(int[] arr)
        {
            int turnCount = 0, switchCount = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            void merge(int[] arr, int left, int mid, int right)
            {
                int n1 = mid - left + 1;
                int n2 = right - mid;
                int[] L = new int[n1];
                int[] R = new int[n2];
                int i, j;
                for (i = 0; i < n1; ++i)
                    L[i] = arr[left + i];
                for (j = 0; j < n2; ++j)
                    R[j] = arr[mid + 1 + j];
                i = 0; j = 0;
                int k = left;
                while (i < n1 && j < n2)
                {
                    if (L[i] <= R[j])
                    {
                        arr[k] = L[i]; i++;
                        switchCount++;
                    }
                    else
                    {
                        arr[k] = R[j]; j++;
                        switchCount++;
                    }
                    k++;
                    turnCount++;
                }
                while (i < n1)
                {
                    arr[k] = L[i];
                    i++; k++;
                    switchCount++;
                }
                while (j < n2)
                {
                    arr[k] = R[j];
                    j++; k++;
                    switchCount++;
                }
            }
            void sort(int[] arr, int left, int right)
            {
                if (left < right)
                {
                    int mid = left + (right - left) / 2;
                    sort(arr, left, mid);
                    sort(arr, mid + 1, right);
                    merge(arr, left, mid, right);
                }
            }
            sort(arr, 0, arr.Length - 1);
            stopwatch.Stop();
            Stats(turnCount, switchCount, stopwatch.Elapsed);
        }

        public void QuickSort(int[] arr)
        {
            int turnCount = 0, switchCount = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            int partition(int[] arr, int left, int right)
            {
                int dexL = left, dexR = right;
                int pivot = arr[(left + right) / 2];
                while (dexL <= dexR)
                {
                    while (arr[dexL] < pivot)
                        dexL++; turnCount++;
                    while (arr[dexR] > pivot)
                        dexR--; turnCount++;
                    if (dexL <= dexR)
                    {
                        Swap(arr, dexL, dexR);
                        switchCount++;
                        dexL++;
                        dexR--;
                        turnCount++;
                    }
                }
                return dexL;
            }
            void qSort(int[] arr, int left, int right)
            {
                int index = partition(arr, left, right);
                if (left < index - 1)
                    qSort(arr, left, index - 1);
                if (index < right)
                    qSort(arr, index, right);
            }
            int start = 0, end = arr.Length - 1;
            qSort(arr, start, end);
            stopwatch.Stop();
            Stats(turnCount, switchCount, stopwatch.Elapsed);
        }

        public void PigeonHoleSort(int[] arr)
        {
            int turnCount = 0, switchCount = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            int max = arr.Max(), min = arr.Min();
            int range = max - min + 1;
            int[] filler()
            {
                int[] holes = new int[range];
                for (int i = 0; i < range - 1; i++)
                {
                    holes = holes.Append(0).ToArray();
                }
                return holes;
            }
            void PHS()
            {
                int[] holes = filler();
                foreach (int n in arr) { holes[n - min]++; }
                int i = 0;
                for (int count = 0; count < range; count++)
                {
                    while (holes[count] > 0)
                    {
                        holes[count]--;
                        arr[i] = count + min;
                        i++; switchCount++;
                    }
                    turnCount++;
                }
            }
            PHS();
            stopwatch.Stop();
            Stats(turnCount, switchCount, stopwatch.Elapsed);
        }

        public void Stats(int turnCount, int switchCount, TimeSpan time)
        {
            Cout($"Amount of turns: {turnCount}");
            Cout($"Amount of switches: {switchCount}");
            Cout($"Runtime: {time.TotalMilliseconds}ms");
        }

        /// <summary>
        /// Spusti vsechny sorty naraz
        /// </summary>
        /// <param name="arr"></param>
        public void Run(int[] arr)
        {
            int[] tstArrBu = new int[arr.Length]; int[] tstArrSh = new int[arr.Length]; int[] tstArrIn = new int[arr.Length]; int[] tstArrSe = new int[arr.Length];
            int[] tstArrMe = new int[arr.Length]; int[] tstArrQu = new int[arr.Length]; int[] tstArrPi = new int[arr.Length];

            arr.CopyTo(tstArrBu, 0); arr.CopyTo(tstArrSh, 0); arr.CopyTo(tstArrIn, 0); arr.CopyTo(tstArrSe, 0); 
            arr.CopyTo(tstArrQu, 0); arr.CopyTo(tstArrPi, 0); arr.CopyTo(tstArrMe, 0);

            BubbleSort(tstArrBu); ShakerSort(tstArrSh); InsertSort(tstArrIn); SelectSort(tstArrSe);
            MergeSort(tstArrMe); QuickSort(tstArrQu); PigeonHoleSort(tstArrPi);
        }

        
    }
}
