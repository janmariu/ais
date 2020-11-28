using Bitbucket.AIS.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bitbucket.AIS.UnitTests
{
    [TestClass]
    public class PositionReportTests
    {
        static AisMessage GetAisMsg(string aisstring = "!AIVDM,1,1,,A,14eG;o@034o8sd<L9i:a;WF>062D,0*7D")
        {
            var aisDecoder = new AisDecoder();
            var nmea = aisDecoder.GetNmeaMessage(aisstring);
            return aisDecoder.GetAisMessage(nmea);
        }

        [TestMethod]
        public void Parse_DecodesAsExpected_SOG()
        {
            Assert.AreEqual(19.6, new AisDecoder().GetAisMessage(
                new AisDecoder().GetNmeaMessage("!AIVDM,1,1,,A,14eG;o@034o8sd<L9i:a;WF>062D,0*7D"))
                .PositionReport.Sog);
        }

        [TestMethod]
        public void Parse_DecodesAsExpected_MMSI()
        {
            Assert.AreEqual(316001245, GetAisMsg().MMSI);
        }

        [TestMethod]
        public void Parse_DecodesAsExpected_HDG()
        {
            Assert.AreEqual(235, GetAisMsg().PositionReport.Hdg);
        }

        [TestMethod]
        public void Parse_DecodesAsExpected_COG()
        {
            Assert.AreEqual(235, GetAisMsg().PositionReport.Cog);
        }

        [TestMethod]
        public void Parse_DecodesAsExpected_LAT()
        {
            Assert.AreEqual(49.200283, GetAisMsg().PositionReport.Lat, 0.0001);
        }

        [TestMethod]
        public void Parse_DecodesPosition_TwoPositivePositions()
        {
            //Latitude: 37.452907°
            //Longitude: 126.611952°
            var msg = GetAisMsg("!AIVDM,1,1,,B,16S`2cPP00a3UF6EKT@2:?vOr0S2,0 * 00");
            Assert.AreEqual(126.611952, msg.PositionReport.Lon, 0.0001);
            Assert.AreEqual(37.452907, msg.PositionReport.Lat, 0.0001);
        }

        [TestMethod]
        public void Parse_DecodesAsExpected_LON()
        {
            var msg = GetAisMsg("!AIVDM,1,1,,A,14eG;o@034o8sd<L9i:a;WF>062D,0*7D");
            Assert.AreEqual(-123.877748,
                msg.PositionReport.Lon, 0.0001);
        }
    }
}
