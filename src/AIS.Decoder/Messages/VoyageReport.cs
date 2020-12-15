using System;
using System.Text;

namespace Bitbucket.AIS.Messages
{
    public class VoyageReport
    {
        public Int32 MMSI { get; set; }
        public int AISVersion { get; set; }
        public Int32 IMO { get; set; }
        public string Callsign { get; set; }
        public string Name { get; set; }
        public int ShipType { get; set; }
        public int DimensionToBow { get; set; }
        public int DimensionToStern { get; set; }
        public int DimensionToPort { get; set; }
        public int DimensionToStarBoard { get; set; }
        public int PositionFixType { get; set; }
        public int EtaMonth { get; set; }
        public int EtaDay { get; set; }
        public int EtaHour { get; set; }
        public int EtaMinute { get; set; }
        public double Draught { get; set; }
        public string Destination { get; set; }

        public override string ToString()
        {
            return $"IMO: {IMO} Name: {Name} Callsign: {Callsign} Destination: {Destination}";
        }

        public static VoyageReport CreateFromBinary(string binary)
        {
            if (binary.Length < 424)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("binary was too short for VoyageReport");
                Console.ForegroundColor = ConsoleColor.Green;
                return null;
            }

            return new VoyageReport()
            {
                MMSI = Convert.ToInt32(binary.Substring(8, 30), 2),
                AISVersion = Convert.ToInt32(binary.Substring(38, 2), 2),
                IMO = Convert.ToInt32(binary.Substring(40, 30), 2),
                Callsign = GetString(binary.Substring(64, 42)),
                Name = GetString(binary.Substring(106, 120)),
                ShipType = Convert.ToInt32(binary.Substring(232, 8), 2),
                DimensionToBow = Convert.ToInt32(binary.Substring(240, 9), 2),
                DimensionToStern = Convert.ToInt32(binary.Substring(249, 9), 2),
                DimensionToPort = Convert.ToInt32(binary.Substring(258, 6), 2),
                DimensionToStarBoard = Convert.ToInt32(binary.Substring(264, 6), 2),
                PositionFixType = Convert.ToInt32(binary.Substring(270, 4), 2),
                EtaMonth = Convert.ToInt32(binary.Substring(274, 4), 2),
                EtaDay = Convert.ToInt32(binary.Substring(278, 5), 2),
                EtaHour = Convert.ToInt32(binary.Substring(283, 5), 2),
                EtaMinute = Convert.ToInt32(binary.Substring(288, 6), 2),
                Destination = GetString(binary.Substring(296, 120))
            };
        }

        private static string GetString(string binary)
        {
            string result = string.Empty;

            while ((binary = binary.Remove(0, 6)).Length > 0)
            {
                byte s = Convert.ToByte(binary.Substring(0, 6), 2);

                if (s == 0) //@ is end of string.
                    break;

                if (s < 32)
                {
                    s += 64;
                }
                else if (s > 31 && s > 64)
                {
                    s += 32;
                }

                result += Encoding.ASCII.GetString(new byte[] { s });
            }

            return result.Trim();
        }
    }
}


