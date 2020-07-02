using Bitbucket.AIS;
using Bitbucket.AIS.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace AIS.Client
{
    public static class Program
    {
        private static void Run()
        {
            var aisDecoder = new AisDecoder();

            TcpClient client = new TcpClient("153.44.253.27", 5631);
            NetworkStream networkStream = client.GetStream();
            StreamReader reader = new StreamReader(networkStream);

            while (true)
            {
                var nmeaList = new List<NmeaMessage>();
                string nmeaString = reader.ReadLine();
                NmeaMessage nmeaMsg;

                try {
                    nmeaMsg = aisDecoder.GetNmeaMessage(nmeaString);
                } catch(InvalidNmeaException e) {                    
                    Console.Error.WriteLine(e.Message);
                    Console.Error.WriteLine($"Failed message: {nmeaString}");
                    continue;
                }
                
                nmeaList.Add(nmeaMsg);

                while (nmeaMsg.NumberOfSentences > 1 && nmeaMsg.SentenceNumber != nmeaMsg.NumberOfSentences)
                {
                    nmeaString = reader.ReadLine();
                    nmeaMsg = aisDecoder.GetNmeaMessage(nmeaString);
                    nmeaList.Add(nmeaMsg);
                }

                var msg = aisDecoder.GetAisMessage(nmeaList);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(msg.ToString());
            }
        }
    

        static void Main(string[] args)
        {
            Run();
        }
    }
}