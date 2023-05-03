using Arche;
using System.Linq.Expressions;
using testGround;


string[] passwords = {
    "Heslo12",
    "heslo1234",
    "Heslo1234",
    "aa"
};
foreach (var pass in passwords)
{
    Standard.Cout(Validation.Verify(pass));
    Standard.Cout();
}
string rndVar = NumConverter.Convert(57, 10, 2);
Standard.Cout(rndVar);
rndVar = NumConverter.Convert(57, 8, 9);
Standard.Cout(rndVar);

Sort<int> sort = new();
Random rand = new Random();
int min = 0;
int max = 500;
int[] array = Enumerable
    .Range(0, 10)
    .Select(i => rand.Next(min, max))
    .ToArray();


Standard.Cout(sort.quick_sort(array, out int[] handle).IsSorted());
Standard.Cout(handle);
