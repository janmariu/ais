using System;
using Bitbucket.AIS.Messages;

namespace Bitbucket.AIS.Parsers
{
    public class NmeaParser
    {
        public static NmeaMessage Parse(string nmea)
        {
            if(!nmea.StartsWith("!")) {
                throw new InvalidNmeaException("NMEA AIs string does not start with !xxVDM or !xxVDO");
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
