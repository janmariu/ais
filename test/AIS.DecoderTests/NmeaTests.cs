using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bitbucket.AIS.Messages;
using Bitbucket.AIS.Parsers;

namespace Bitbucket.AIS.UnitTests
{
    [TestClass]
    public class NmeaTests
    {
        [TestMethod]
        public void Parse_NonMultipartNmea()
        {
            NmeaMessage msg = NmeaParser.Parse("!AIVDM,1,1,,A,14eG;o@034o8sd<L9i:a;WF>062D,0*7D");

            Assert.AreEqual("!AIVDM", msg.MessageType);
            Assert.AreEqual(1, msg.NumberOfSentences);
            Assert.AreEqual(1, msg.SentenceNumber);
            Assert.AreEqual(null, msg.SequentialMessageId);
            Assert.AreEqual("A", msg.Channel);
            Assert.AreEqual("14eG;o@034o8sd<L9i:a;WF>062D", msg.EncodedPayload);
            Assert.AreEqual("0*7D", msg.Checksum);
        }
    }
}
