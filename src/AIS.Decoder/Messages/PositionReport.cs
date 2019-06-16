//http://catb.org/gpsd/AIVDM.html#_types_1_2_and_3_position_report_class_a
using System;

namespace Bitbucket.AIS.Messages
{
    public class PositionReport
    {
	    public Int32 MMSI { get; set; }

        public DateTime Received { get; set; }

        public Int32 Accuracy { get; set; }

        public Int32 NavigationStatus { get; set; }

        public Double Rot { get; set; }

        public Double Sog { get; set; }

        public Int32 Hdg { get; set; }

        public Double Lat { get; set; }

        public Double Lon { get; set; }

        public Double Cog { get; set; }

        public int Timestamp { get; set; }

        public override string ToString()
        {
            return $"Received: {Received.ToString("yyyy-MM-dd HH:mm:ss")} SOG: {Sog}, HDG {Hdg}, LAT {Lat}, LON: {Lon}";
        }

        public static PositionReport CreateFromBinary(string decodedAisString)
        {
            if(decodedAisString.Length < 168)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("decodedAisString was to short for a positionmessage.");
                Console.ForegroundColor = ConsoleColor.Green;
                return null;
            }

            return new PositionReport()
            {
                Received = DateTime.Now,
                MMSI = Convert.ToInt32(decodedAisString.Substring(8, 30), 2),
                NavigationStatus = Convert.ToInt32(decodedAisString.Substring(38, 4), 2),
		        Rot = Convert.ToInt32(decodedAisString.Substring(42, 8), 2) / 10.0,
                Sog = Convert.ToInt32(decodedAisString.Substring(50, 10), 2) / 10.0,
                Accuracy = Convert.ToInt32(decodedAisString.Substring(60, 1), 2),
                Lon = GetLongitude(decodedAisString.Substring(61, 28)),
                Lat = GetLatitude(decodedAisString.Substring(89, 27)),
                Cog = Convert.ToInt32(decodedAisString.Substring(116, 12), 2) / 10.0,
                Hdg = Convert.ToInt32(decodedAisString.Substring(128, 9), 2),
                Timestamp = Convert.ToInt32(decodedAisString.Substring(137,6), 2)
                //137
            };
        }

        private static double GetLongitude(string binary)
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

        private static double GetLatitude(string binary)
        {            
            int b = Convert.ToInt32(binary, 2);
            int tmp = b;

            if(b > 0x3FFFFFF)
            {
                tmp = b ^ 0xFFFFFFF;
            }

            double result = tmp / 600000.0;            
            return result;
        }
    }
}
