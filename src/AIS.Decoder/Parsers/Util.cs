using System;

namespace Bitbucket.AIS.Parsers
{
    public static class Util
    {
        public static double GetLongitude(string binary)
        {
            int b = Convert.ToInt32(binary, 2);
            int tmp = b;

            if (b > 0x7FFFFFF)
            {
                tmp = b ^ 0xFFFFFFF;
                tmp = ~tmp;
            }

            double result = tmp / 600000.0;
            return result;
        }

        public static double GetLatitude(string binary)
        {
            int b = Convert.ToInt32(binary, 2);
            int tmp = b;

            if (b > 0x3FFFFFF)
            {
                tmp = b ^ 0xFFFFFFF;
            }

            double result = tmp / 600000.0;
            return result;
        }
    }
}
