using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Arche;

namespace testGround
{
    public static class Std
    {
        public static T ChangeType<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static int prc(this object obj)
        {
            return obj.ChangeType<int>();
        }
    }
    
    
    internal class Sort<T> 
        where T : 
        INumber<T>, 
        IComparable<T>, 
        IEquatable<T>, 
        IAdditionOperators<T, T, T>, 
        IAdditionOperators<T, int, T>,
        IComparisonOperators<T, int, bool>
    {
        // insert sort
        // quick sort
        // merge sort
        private T[]? control {  get; set; }
        

        private bool swap(ref T a, ref T b)
        {
            (a, b) = (b, a);
            return true;
        }

        public bool IsSorted()
        {
            if (control == null)
            {
                throw new NullReferenceException($"IsSorted cannot be called on array of null");
            }
            for (int i = 0; i < control.Length -1; i++)
            {
                if (control[i].CompareTo(control[i + 1]) > 0)
                {
                    return false;
                }
            }
            return true;

        }
        public Sort<T> bubble_sort(T[] array, out T[] arr)
        {
            
            T end = (array.Length - 1).ChangeType<T>();
            for(int i  = 0; i < array.Length; i++)
            {
                for(T j = 0.ChangeType<T>(); j < end; j++)
                {
                    if (array[j.prc()].CompareTo(array[(j + 1).prc()]) > 0){
                        swap(ref array[j.prc()], ref array[(j + 1).prc()]);
                    }
                }
                end--;
            }
            control = array;
            arr = array;
            return this;
        }

        public Sort<T> shaker_sort(T[] array, out T[] arr)
        {
            int end = array.Length - 1;
            int start = 0;
            bool swapped = true;
            while(swapped) { 
                swapped = false;
                for (int i = start; i < end; i++)
                {
                    for(int k = start; k < end; k++)
                    {
                        if (array[k].CompareTo(array[k + 1]) > 0)
                        {
                            swapped = true;
                            swap(ref array[k], ref array[k + 1]);
                        }
                    }
                }
                if (swapped) { break; }
                end--;
                swapped = false;
                for (int i = end; i > start; i--)
                {
                    
                    if (array[i].CompareTo(array[i + 1]) < 0)
                    {
                        swapped = true;
                        swap(ref array[i], ref array[i + 1]);
                    }
                    
                }

                if (swapped) { break; }
                start--;
            }
            control = array;
            arr = array;
            return this;
        }
        public Sort<T> insertion_sort(T[] array, out T[] arr)
        {
            for (int i = 1;i < array.Length; i++) {
                if (array[i] < array[i - 1])
                {
                    for( int j = i-1; j > 0; j--)
                    {
                        swap(ref array[j], ref array[j - 1]);
                    }
                }
            }
            control = array;
            arr = array;
            return this;
        }
        public Sort<T> select_sort(T[] array, out T[] arr)
        {
            T length = array.Length.ChangeType<T>();

            for (T n = 0/*.ChangeType<T>()*/; n < length; n++)
            {
                T min_idx = n;
                for (T j = n + 1; j < length; j++)
                {
                    if (array[j.ChangeType<int>()] < array[min_idx.ChangeType<int>()])
                    {
                        min_idx = j;
                    }
                }
                swap(ref array[n.prc()], ref array[min_idx.prc()]);
            }
            arr = array;
            control = array;
            return this;
        }
        public Sort<T> quick_sort(T[] arr, out T[] values)
        {
            int length = arr.Length;
            int min = 0;
            int max = length - 1;
            int partition(int small, int large)
            {
                T mid = arr[(small + large) / 2];
                int left = small;
                int right = large;
                while(left <= right)
                {
                    while (arr[left] < mid) { left++; }
                    while (arr[right] > mid) { right--; }
                    if (left <= right)
                    {
                        swap(ref arr[left], ref arr[right]);
                        left++;
                        right--;
                    }
                }
                return left;
            }
            void qcksrt()
            {
                int mid = partition(min, max);
                if(min < mid -1) partition(min, mid - 1);
                if(mid < max) partition(mid, max);
            }
            qcksrt();
            values = arr;
            control = values;
            return this;
        }
    }
}
