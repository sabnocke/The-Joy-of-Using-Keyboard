using System;
using System.Collections;
using System.Diagnostics;

namespace Arche;

public static class Standard
{
    /// <summary>
    /// Better Console.Write(-line)
    /// </summary>
    /// <param name="item">variable that should be printed</param>
    /// <param name="sep">separator symbol</param>
    /// <param name="end">end-line symbol</param>
    public static void Cout(dynamic? item = null, string sep = " ", string end = "")
    {
        if (item == null)
        {
            Console.WriteLine();
        }
        else if (item is IEnumerable and not string)
        {
            foreach (var @char in item)
            {
                Console.Write($"{@char}{sep}");
            }
            Console.WriteLine(end);
        }
        else
        {
            Console.WriteLine($"{item}{sep}");
        }
    }
}

public class Measure<T>
where T : class
{
    private Delegate? _cstDelegate;
    private readonly AssemblyConstructor<T> _assemblyConstructor;
    private readonly Stopwatch _stopwatch = new Stopwatch();
    public Measure() => _assemblyConstructor = new AssemblyConstructor<T>();

    public void Setup(string methodName) => _cstDelegate = _assemblyConstructor.Delegation(methodName);

    public dynamic? Time(params object[] args)
    {
        _stopwatch.Restart();
        //_stopwatch.Start();
        dynamic? result = _cstDelegate?.DynamicInvoke(args);
        _stopwatch.Stop();
        Standard.Cout($"[Finished in {_stopwatch.ElapsedMilliseconds}ms]");
        return result;
    }
}

public static class Compare
{
    public static bool Within<T>(this T value, T lower, T upper, bool inclusive = false)
        where T : IComparable<T>
    {
        switch (inclusive)
        {
            case false:
                if (value.CompareTo(lower) < 0) return false;
                if (value.CompareTo(upper) > 0) return false;
                return true;
            case true:
                if (value.CompareTo(lower) <= 0) return false;
                if (value.CompareTo(upper) >= 0) return false;
                return true;
            //default: 
            //return false;
        }
    }
}
