namespace testGround;

public static class NumSum
{
    public static double Sum(this int input) => input.ToString().Where(char.IsDigit).Sum(char.GetNumericValue);
}