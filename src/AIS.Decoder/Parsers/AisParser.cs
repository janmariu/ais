using Bitbucket.AIS.Messages;
using System;
using System.Text;

namespace Bitbucket.AIS.Parsers
{
    internal class AisParser
    {
        public static AisMessage DecodeAisString(string armoredString)
        {
            var aisMsg = new AisMessage();
            string binary = DecodeVDXArmoredString(armoredString);

            aisMsg.MessageType = Convert.ToInt32(binary.Substring(0, 6), 2);
            aisMsg.RepeatIndicator = Convert.ToInt32(binary.Substring(6, 2), 2);
            aisMsg.MMSI = Convert.ToInt32(binary.Substring(8, 30), 2);

            switch (aisMsg.MessageType)
            {
                case 1:
                case 2:
                case 3:
                    aisMsg.PositionReport = PositionReport.CreateFromBinary(binary);
                    break;
                case 5:
                    aisMsg.VoygageReport = VoyageReport.CreateFromBinary(binary);
                    break;
            }

            return aisMsg;
        }

        private static string DecodeVDXArmoredString(string vdxString)
        {
            string binarystring = "";

            Byte[] bytes = Encoding.ASCII.GetBytes(vdxString);
            foreach (var b in bytes)
            {
                int tmpValue = b - 48;
                if (tmpValue > 40)
                    tmpValue = tmpValue - 8;

                string bitstring = Convert.ToString(tmpValue, 2);

                while (bitstring.Length < 6)
                    bitstring = "0" + bitstring;

                binarystring += bitstring;
            }

            return binarystring;
        }
    }
}
