using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Bitbucket.AIS.CommandLine;
using System.IO;
using System.Linq;
using Bitbucket.AIS.Messages;

namespace AIS.DecoderTests
{
    [TestClass]
    public class CommandLine
    {
        [TestMethod]
        public void parseMultiPartMessage()
        {
            var result = RunCommandLineDecoder(new[] {
                @"\s:2573245,c:1607282879*02\!BSVDM,2,1,2,A,53o2rL400000aL<<000dpn1<hDU0pE800000000l0p@4540Ht040E4Sk0000,0*69",
                @"\s:2573245,c:1607282879*02\!BSVDM,2,2,2,A,00000000000,2*3F"
            });
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void parseTwoLines()
        {
            var result = RunCommandLineDecoder(new[] {
                @"\s:2573234,c:1607282879*04\!BSVDM,1,1,,A,13mq:Q0000PH7tDQw:IN44On2HRV,0*11",
                @"\s:2573234,c:1607282879*04\!BSVDM,1,1,,A,13mq:Q0000PH7tDQw:IN44On2HRV,0*11"
            });
            Assert.AreEqual(3, result.Count, "Expected to get header and two data rows as output.");
            Assert.AreEqual("MMSI;Accuracy;NavigationStatus;Rot;Sog;Hdg;Lat;Lon;Cog;Timestamp;Raw", result[0]);
            Assert.AreEqual(@"257837700;1;0;0;0;143;59.396435;5.26999;360;59;\s:2573234,c:1607282879*04\!BSVDM,1,1,,A,13mq:Q0000PH7tDQw:IN44On2HRV,0*11", result[1]);
            Assert.AreEqual(@"257837700;1;0;0;0;143;59.396435;5.26999;360;59;\s:2573234,c:1607282879*04\!BSVDM,1,1,,A,13mq:Q0000PH7tDQw:IN44On2HRV,0*11", result[2]);


 //     
        }

        [TestMethod]
        public void parseOneLine()
        {
            var result = RunCommandLineDecoder(new[] { @"\s:2573234,c:1607282879*04\!BSVDM,1,1,,A,13mq:Q0000PH7tDQw:IN44On2HRV,0*11" });
            Assert.AreEqual(2, result.Count, "There should be a header and a data line in the result.");
            Assert.AreEqual("MMSI,Accuracy,NavigationStatus,Rot,Sog,Hdg,Lat,Lon,Cog,Timestamp", result[0]);
            Assert.AreEqual("257837700,1,0,0,0,143,59.396435,5.26999,360,59", result[1]);
        }

        private List<String> RunCommandLineDecoder(string[] rawAisData)
        {
            var resultStream = new MemoryStream();
            var decoder = new CommandLineDecoder(
                new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(string.Join("\r\n", rawAisData)))),
                new StreamWriter(resultStream));
            decoder.read();

            resultStream.Seek(0, SeekOrigin.Begin);

            var result = new List<String>();
            var reader = new StreamReader(resultStream);

            while (reader.Peek() > 0)
            {
                result.Add(reader.ReadLine());
            }

            return result;
        }
    }
}