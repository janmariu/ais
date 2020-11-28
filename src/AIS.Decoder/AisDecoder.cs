using Bitbucket.AIS.Messages;
using Bitbucket.AIS.Parsers;
using System;
using System.Collections.Generic;

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
            if (nmeaMsg.NumberOfSentences > 1)
                throw new Exception("Multipart message. Use GetAisMessage(List<NmeaMessage>) instead");

            return AisParser.DecodeAisString(nmeaMsg.EncodedPayload);
        }

        public AisMessage GetAisMessage(List<NmeaMessage> nmeaMsg)
        {
            var encodedPayload = string.Empty;
            foreach (var msg in nmeaMsg)
            {
                encodedPayload += msg.EncodedPayload;
            }

            return AisParser.DecodeAisString(encodedPayload);
        }
    }
}
