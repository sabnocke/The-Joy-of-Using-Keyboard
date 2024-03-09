
namespace testGround;

public class Exercism
{
    private static double 
        SuccessRate(int speed)
        => speed switch
            {
                0 => 0.0,
                > 0 and < 5 => 1.0,
                >= 5 and <= 8 => .9,
                9 => .8,
                10 => .7,
                _ => .5
            };

    private static double
        ProductionRatePerHour(int speed) =>
        221 * speed * SuccessRate(speed);

    public static int
        WorkingItemsPerMinute(int speed) =>
        (int)(ProductionRatePerHour(speed) / 60);
}

public class RemoteControlCar
{
    private int _distance = 0;
    private int _battery = 100;
    
    public static RemoteControlCar Buy()
    {
        return new RemoteControlCar();
    }

    public string DistanceDisplay()
    {
        return $"Driven {_distance} meters";
    }

    public string BatteryDisplay()
    {
        return _battery > 0 ? $"Battery at {_battery}%" : "Battery empty" ;
    }

    public void Drive()
    {
        if (_battery == 0) return;
        _distance += 20;
        _battery -= 1;
    }
}

class BirdCount
{
    private readonly int[] _birdsPerDay;

    public BirdCount(int[] birdsPerDay)
    {
        this._birdsPerDay = birdsPerDay;
    }

    public static List<int> LastWeek() => new() { 0, 2, 5, 3, 7, 8, 4 };

    public int Today() => _birdsPerDay[^1];
    

    public void IncrementTodaysCount() => _birdsPerDay[^1] += 1;

    public bool HasDayWithoutBirds() => _birdsPerDay.Any(i => i == 0);


    public int CountForFirstDays(int numberOfDays) => _birdsPerDay.Take(numberOfDays).Sum();
    

    public int BusyDays() => _birdsPerDay.Count(i => i >= 5);
   
}

static class Leap
{
    private static int Sign(this int number) => number == 0 ? 0 : 1;
    
    public static bool IsLeapYear(int year)
    {
        var y = (year % 100).Sign() + (year % 400).Sign() + (year % 4).Sign();
        return y % 2 == 0;
    }
}

static class SavingsAccount
{
    public static float InterestRate(decimal balance)
        => balance switch
        {
            < 0 => 3.123f,
            < 1000 => 0.5f,
            >= 1000 and < 5000 => 1.621f,
            >= 5000 => 2.475f
        };

    public static decimal Interest(decimal balance)
    {
        return (balance * (decimal)InterestRate(balance));
    }

    public static decimal AnnualBalanceUpdate(decimal balance)
    {
        return balance + Interest(balance);
    }

    public static int YearsBeforeDesiredBalance(decimal balance, decimal targetBalance)
    {
        var years = 0;
        for (; balance < targetBalance; years++)
            balance = AnnualBalanceUpdate(balance);
        return years;
    }
}

public static class ProteinTranslation
{
    private static string Mapping(string? s)
        => s switch
        {
            "AUG" => "Methionine",
            "UUU" or "UUC" => "Phenylalanine",
            "UUA" or "UUG" => "Leucine",
            "UCU" or "UCC" or "UCA" or "UCG" => "Serine",
            "UAU" or "UAC" => "Tyrosine",
            "UGU" or "UGC" => "Cysteine",
            "UGG" => "Tryptophan",
            "UAA" or "UAG" or "UGA" => "STOP",
            _ => ""
        };

    public static string[] Proteins(string strand)
    {

        var batch = strand.Chunk(3).Select(item => Mapping(new string(item)));
        return new string[10];
    }
}