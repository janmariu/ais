using System;
using System.Collections.Generic;
using Bitbucket.AIS.Messages;
using Bitbucket.AIS.Parsers;

namespace Bitbucket.AIS
{
    public class AisDecoder
    {
        public NmeaMessage GetNmeaMessage(string nmea)
        {
            return NmeaParser.Parse(nmea);
        }

        public AisMessage GetAisMessage(NmeaMessage nmeaMsg)
        {
            if(nmeaMsg.NumberOfSentences > 1)
                throw new Exception("Multipart message. Use GetAisMessage(List<NmeaMessage>) instead");

            var parser = new AisParser();

            return parser.DecodeAisString(nmeaMsg.EncodedPayload);
        }

        public AisMessage GetAisMessage(List<NmeaMessage> nmeaMsg)
        {
            var encodedPayload = string.Empty;
            foreach(var msg in nmeaMsg)
            {
                encodedPayload += msg.EncodedPayload;
            }

            return new AisParser().DecodeAisString(encodedPayload);
        }
    }
}
