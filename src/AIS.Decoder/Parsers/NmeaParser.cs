using Bitbucket.AIS.Messages;
using System;
using System.Text.RegularExpressions;

namespace Bitbucket.AIS.Parsers
{
    public class NmeaParser
    {
        public static NmeaMessage Parse(string nmea)
        {
            if (!nmea.StartsWith("!"))
            {
                try
                {
                    var match = Regex.Match(nmea, "![A,B,D,I,N,R,S,T,X,S]{2}VD[M,O].*\\*..", RegexOptions.None, TimeSpan.FromMilliseconds(100));
                    if (match.Success)
                    {
                        nmea = match.Value;
                    }
                    else
                    {
                        throw new InvalidNmeaException($"Not a valid AIVDM/AIVDO NMEA string: {nmea}");
                    }
                }
                catch (RegexMatchTimeoutException)
                {
                    throw new InvalidNmeaException($"Timed out parsing AIVDM/AIVDO NMEA string: {nmea}");
                }
            }

            var result = new NmeaMessage();

            var nmeaparts = nmea.Split(',');

            result.MessageType = nmeaparts[0];
            result.NumberOfSentences = Int32.Parse(nmeaparts[1]);
            result.SentenceNumber = Int32.Parse(nmeaparts[2]);
            if (Int32.TryParse(nmeaparts[3], out int seqId))
                result.SequentialMessageId = seqId;
            result.Channel = nmeaparts[4];
            result.EncodedPayload = nmeaparts[5];
            result.Checksum = nmeaparts[6];
            result.ReceivedDate = DateTime.Now;

            return result;
        }
    }
}
