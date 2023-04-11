using Arche;

namespace testGround
{
    internal static class NumConverter
    {
        public static string Convert(this int num, int convertFrom, int convertTo)
        {
            
            if (convertFrom == convertTo) return num.ToString();
            if (!(convertFrom.Within(1, 10) && convertTo.Within(1, 10)))
            {
                throw new ArgumentException("Ciselna soustava musi byt mezi 1 a 10");
            }


            string result =
                (convertFrom != 10) ?
                Converto(Decimator(num, convertFrom), convertTo) : Converto(num, convertTo);

            return result;
        }

        private static int Decimator(int num, int convertFrom)
        {
            int copy = num;
            int sum = 0; int exponent = 0;
            while (copy > 0)
            {
                sum += copy % 10 * (int)Math.Pow(convertFrom, exponent);
                copy /= 10; exponent++;
            }
            num = sum;

            return num;
        }

        private static string Converto(int num, int convertTo)
        {
            string result = "";
            while (num > 0)
            {
                result = (num % convertTo) + result;
                num /= convertTo;
            }
            return result;
        }
    }
}
