using Bitbucket.AIS.Messages;
using Bitbucket.AIS.Parsers;
using System;
using System.Collections.Generic;

namespace Bitbucket.AIS
{
    public static class AisDecoder
    {
        public static NmeaMessage ParseNmea(string nmea)
        {
            return NmeaParser.Parse(nmea);
        }

        public static AisMessage DecodeAis(NmeaMessage nmeaMsg)
        {
            if (nmeaMsg.NumberOfSentences > 1)
                throw new Exception("Multipart message. Use GetAisMessage(List<NmeaMessage>) instead");

            return AisParser.DecodeAisString(nmeaMsg.EncodedPayload);
        }

        public static AisMessage DecodeAis(List<NmeaMessage> nmeaMsg)
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
