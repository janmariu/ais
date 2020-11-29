using Bitbucket.AIS;
using Bitbucket.AIS.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AIS.DecoderTests
{
    [TestClass]
    public class BaseStationTests
    {
        static AisMessage GetAisMsg(string nmea = "!BSVDO,1,1,,B,402M6C1vBvk<iOH79B`U@`?02<00,0*22")
        {
            return AisDecoder.DecodeAis(AisDecoder.ParseNmea(nmea));
        }

        [TestMethod]
        public void Parse_DecodesAsExpected()
        {
            var msg = GetAisMsg();
            Assert.AreEqual(2020, msg.BaseStationReport.Year);
            Assert.AreEqual(11, msg.BaseStationReport.Month);
            Assert.AreEqual(29, msg.BaseStationReport.Day);
            Assert.AreEqual(19, msg.BaseStationReport.Hour);
            Assert.AreEqual(12, msg.BaseStationReport.Minute);
            Assert.AreEqual(49, msg.BaseStationReport.Seconds);
            Assert.AreEqual(false, msg.BaseStationReport.FixQuality);
            Assert.AreEqual(-08.7137450, msg.BaseStationReport.Latitude, 0.0000001);
            Assert.AreEqual(70.9225067, msg.BaseStationReport.Longitude, 0.0000001);
            Assert.AreEqual(15, msg.BaseStationReport.EPFDType);
            Assert.AreEqual(true, msg.BaseStationReport.Raim);
        }
    }
}
