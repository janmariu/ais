using Bitbucket.AIS.Messages;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bitbucket.AIS.CommandLine
{
    public class CommandLineDecoder
    {
        private readonly TextReader reader;
        private readonly TextWriter writer;

        public CommandLineDecoder(TextReader reader, TextWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void read()
        {
            bool headerWritten = false;

            var nmeaMessages = new List<NmeaMessage>();
            while (reader.Peek() > 0)
            {
                var rawNmea = reader.ReadLine();
                var nmeaMsg = AisDecoder.ParseNmea(rawNmea);
                nmeaMessages.Add(nmeaMsg);
                
                if(nmeaMsg.SentenceNumber == nmeaMsg.NumberOfSentences) {
                    var aisMsg = AisDecoder.DecodeAis(nmeaMessages);

                    if(!headerWritten)
                    {
                        writer.WriteLine(CsvBuilder.toHeaderString(aisMsg) + ";Raw");
                        headerWritten = true;
                    }

                    if(new []{ 1, 2, 3 }.Contains(aisMsg.MessageType))
                    {
                        var aisStr = CsvBuilder.toCsvString(aisMsg);
                        writer.WriteLine(aisStr + ";" + rawNmea);
                    }

                    nmeaMessages.Clear();
                }
            }

            writer.Flush();
        }
    }
    class CsvBuilder
    {
        public static string toHeaderString(AisMessage aisMsg)
        {
            switch (aisMsg.MessageType)
            {
                case 1:
                case 2:
                case 3:
                    System.Reflection.PropertyInfo[] p = aisMsg.PositionReport.GetType().GetProperties();
                    return p.Select(prop => prop.Name).Aggregate<string>((result, name) => result += ";" + name);
            }

            return string.Empty;
        }

        public static string toCsvString(AisMessage aisMsg)
        {
            StringBuilder builder = new StringBuilder();
            OrderedDictionary properties = new OrderedDictionary();
            switch(aisMsg.MessageType)
            {
                case 1:
                case 2:
                case 3:
                    properties = getProperties<PositionReport>(aisMsg.PositionReport);
                    break;
            }

            foreach(var key in properties.Keys)
            {
                builder.Append(properties[key]);
                builder.Append(";");
            }
            builder.Remove(builder.Length-1, 1);

            return builder.ToString();
        }

        public static OrderedDictionary getProperties<T>(T item)
        {
            var result = new OrderedDictionary();
            foreach(var property in item.GetType().GetProperties())
            {
                result.Add(property.Name, property.GetValue(item));
            }

            return result;
        }
    }
}
