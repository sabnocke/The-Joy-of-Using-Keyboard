namespace exercism;
using static Console;
using static ProteinTranslation;

internal static class Program
{
    private static void PrintArray(this string[] collection, char sep = ' ')
    {
        foreach (var v in collection)
        {
            Write(v);
            Write(sep);
        }
        WriteLine();
    }
    private static void Main()
    {
        Proteins("AUGUUUUCU").PrintArray();
        Proteins("UGA").PrintArray();
        WriteLine(Proteins("UGA") == Array.Empty<string>());
        WriteLine("Hello, World!");
    }
}