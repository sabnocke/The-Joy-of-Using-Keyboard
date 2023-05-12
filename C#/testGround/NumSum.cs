namespace testGround;

public static class NumSum
{
    public static double Sum(this int input)
    {
        double sum = 0;
        foreach (var @char in input.ToString())
            if (char.IsDigit(@char))
                sum += char.GetNumericValue(@char);

        return sum;
    }
}