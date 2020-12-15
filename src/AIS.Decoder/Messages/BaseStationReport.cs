using Bitbucket.AIS.Parsers;
using System;

namespace Bitbucket.AIS.Messages
{
    public class BaseStationReport
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Seconds { get; set; }
        public int Minute { get; set; }
        public bool FixQuality { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int EPFDType { get; set; }
        public int SOTDMA { get; set; }
        public bool Raim { get; set; }

        public static BaseStationReport CreateFromBinary(string binary)
        {
            if (binary.Length < 168)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("decodedAisString was to short for a BaseStationReport.");
                Console.ForegroundColor = ConsoleColor.Green;
                return null;
            }

            return new BaseStationReport()
            {
                Year = Convert.ToInt32(binary.Substring(38, 14), 2),
                Month = Convert.ToInt32(binary.Substring(52, 4), 2),
                Day = Convert.ToInt32(binary.Substring(56, 5), 2),
                Hour = Convert.ToInt32(binary.Substring(61, 5), 2),
                Minute = Convert.ToInt32(binary.Substring(66, 6), 2),
                Seconds = Convert.ToInt32(binary.Substring(72, 6), 2),
                FixQuality = Convert.ToInt32(binary.Substring(78, 1), 2) == 1 ? true : false,
                Latitude = Util.GetLongitude(binary.Substring(79, 28)),
                Longitude = Util.GetLatitude(binary.Substring(107, 27)),
                EPFDType = Convert.ToInt32(binary.Substring(134, 4), 2),
                Raim = Convert.ToInt32(binary.Substring(148, 1), 2) == 1 ? true : false,
                SOTDMA = Convert.ToInt32(binary.Substring(149, 19), 2)                
            };
        }

        public override string ToString()
        {
            return  $"Timestamp: {Year}-{Month}-{Day} {Hour}:{Minute}:{Seconds} " +
                $"FixQuality: {FixQuality} " +
                $"Latitude: {Latitude} " +
                $"Longitude: {Longitude} " +
                $"EPFDType: {EPFDType} " +
                $"RAIM: {Raim} " +
                $"SOTDMA: {SOTDMA}";
        }
    }
}
