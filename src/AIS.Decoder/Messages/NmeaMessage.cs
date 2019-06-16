using System;

namespace Bitbucket.AIS.Messages
{
    public struct NmeaMessage
    {
        public string MessageType { get; set; }

        public int NumberOfSentences { get; set; }

        public int SentenceNumber { get; set; }

        public int? SequentialMessageId { get; set; }

        public string Channel { get; set; }

        public string EncodedPayload { get; set; }

        public string Checksum { get; set; }

        public DateTime ReceivedDate { get; set; }
    }
}
