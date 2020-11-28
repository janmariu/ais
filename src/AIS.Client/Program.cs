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
            Console.ForegroundColor = ConsoleColor.Green;
            var aisDecoder = new AisDecoder();

            TcpClient client = new TcpClient("153.44.253.27", 5631);
            NetworkStream networkStream = client.GetStream();
            StreamReader reader = new StreamReader(networkStream);

            var nmeaList = new List<NmeaMessage>();

            while (true)
            {
                string nmeaString = reader.ReadLine();

                try
                {
                    NmeaMessage nmeaMsg = aisDecoder.GetNmeaMessage(nmeaString);
                    nmeaList.Add(nmeaMsg);

                    if (nmeaMsg.SentenceNumber == nmeaMsg.NumberOfSentences)
                    {
                        var aisMsg = aisDecoder.GetAisMessage(nmeaList);
                        Console.WriteLine(aisMsg.ToString());
                        nmeaList.Clear();
                    }
                }
                catch (InvalidNmeaException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.Green;
                }
            }
        }

        static void Main()
        {
            Run();
        }
    }
}