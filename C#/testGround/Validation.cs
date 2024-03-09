using System.Text.RegularExpressions;
using static System.Console;

namespace testGround;

internal static class Validation
{
    private static bool CheckUnique(string password)
    {
        for (var i = 1; i < password.Length; i++)
            if (password[i] == password[i - 1])
                return false;
        return true;
    }

    public static string Verify(this string password)
    {
        var verified = true;
        Dictionary<string, bool> boolsName = new()
        {
            { @"Heslo neobsahuje požadnovaný počet znaků.", password.Length > 8 },
            { @"Heslo neobsahuje číslice.", Regex.IsMatch(password, @"\d", RegexOptions.IgnoreCase) },
            { @"Heslo neobsahuje velké písmeno.", Regex.IsMatch(password, @"[A-Z]") },
            { @"Heslo obsahuje dva stejné znaky za sebou.", CheckUnique(password) }
        };

        WriteLine($"Heslo: {password}");
        foreach (var (key, value) in boolsName)
            if (!value)
            {
                WriteLine($"*\t{key}");
                verified = false;
            }

        return verified ? "*\tPassword is correct." : "";
    }
}