﻿using System.Numerics;


namespace testGround;

public static class Std
{
    public static T ChangeType<T>(this object obj)
    {
        return (T)Convert.ChangeType(obj, typeof(T));
    }

    public static int Prc(this object obj)
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
    private T[]? Control { get; set; }


    private static void Swap(ref T a, ref T b) => (a, b) = (b, a);
    
    public bool IsSorted()
    {
        if (Control == null) throw new NullReferenceException($"IsSorted cannot be called on array of null");
        for (var i = 0; i < Control.Length - 1; i++)
            if (Control[i].CompareTo(Control[i + 1]) > 0)
                return false;
        return true;
    }

    public Sort<T> bubble_sort(T[] array, out T[] arr)
    {
        var end = (array.Length - 1).ChangeType<T>();
        for (var i = 0; i < array.Length; i++)
        {
            for (var j = 0.ChangeType<T>(); j < end; j++)
                if (array[j.Prc()].CompareTo(array[(j + 1).Prc()]) > 0)
                    Swap(ref array[j.Prc()], ref array[(j + 1).Prc()]);
            end--;
        }

        Control = array;
        arr = array;
        return this;
    }

    public Sort<T> shaker_sort(T[] array, out T[] arr)
    {
        var end = array.Length - 1;
        var start = 0;
        var swapped = true;
        while (swapped)
        {
            swapped = false;
            for (var i = start; i < end; i++)
            for (var k = start; k < end; k++)
                if (array[k].CompareTo(array[k + 1]) > 0)
                {
                    swapped = true;
                    Swap(ref array[k], ref array[k + 1]);
                }

            if (swapped) break;
            end--;
            swapped = false;
            for (var i = end; i > start; i--)
                if (array[i].CompareTo(array[i + 1]) < 0)
                {
                    swapped = true;
                    Swap(ref array[i], ref array[i + 1]);
                }

            if (swapped) break;
            start--;
        }

        Control = array;
        arr = array;
        return this;
    }

    public Sort<T> insertion_sort(T[] array, out T[] arr)
    {
        for (var i = 1; i < array.Length; i++)
            if (array[i] < array[i - 1])
                for (var j = i - 1; j > 0; j--)
                    Swap(ref array[j], ref array[j - 1]);
        Control = array;
        arr = array;
        return this;
    }

    public Sort<T> select_sort(T[] array, out T[] arr)
    {
        var length = array.Length.ChangeType<T>();

        for (var n = 0.ChangeType<T>(); n < length; n++)
        {
            var minIdx = n;
            for (var j = n + 1; j < length; j++)
                if (array[j.ChangeType<int>()] < array[minIdx.ChangeType<int>()])
                    minIdx = j;
            Swap(ref array[n.Prc()], ref array[minIdx.Prc()]);
        }

        arr = array;
        Control = array;
        return this;
    }

    public Sort<T> quick_sort(T[] arr, out T[] values)
    {
        var length = arr.Length;
        const int min = 0;
        var max = length - 1;

        Quicksort();
        values = arr;
        Control = values;
        return this;

        void Quicksort()
        {
            var mid = Partition(min, max);
            if (min < mid - 1) Partition(min, mid - 1);
            if (mid < max) Partition(mid, max);
        }

        int Partition(int small, int large)
        {
            var mid = arr[(small + large) / 2];
            var left = small;
            var right = large;
            while (left <= right)
            {
                while (arr[left] < mid) left++;
                while (arr[right] > mid) right--;
                if (left > right) continue;
                Swap(ref arr[left], ref arr[right]);
                left++;
                right--;
            }

            return left;
        }
    }
}