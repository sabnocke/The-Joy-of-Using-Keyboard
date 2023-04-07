using Arche;
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