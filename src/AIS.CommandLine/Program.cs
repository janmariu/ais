using System;
using Bitbucket.AIS;

namespace Bitbucket.AIS.CommandLine
{
    class Program
    {

        static void Main(string[] args)
        {
            if(Console.IsInputRedirected)
            {
                var decoder = new CommandLineDecoder(Console.In, Console.Out);
                decoder.read();
            } 
            else
            {
                Console.WriteLine("Usage: Redirect some input.");
            }
        }
    }
}
