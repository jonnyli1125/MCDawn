using System;
using System.IO;
using System.Net;

namespace Starter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("MCDawn_.dll"))
            {
                MCDawn_.Gui.Program.Main(args);
                return;
            }
            else
            {
                Console.WriteLine("MCDawn_.dll appears to be mising...");
                Console.WriteLine("Downloading from http://updates.mcdawn.com/MCDawn_.dll");

                try
                {
                    using (WebClient client = new WebClient())
                        client.DownloadFile("http://updates.mcdawn.com/MCDawn_.dll", "MCDawn_.dll");
                }
                catch { Console.WriteLine("Unable to download file. Try again later."); goto exit; }

                Console.WriteLine("Finished downloading! Let's try this again, shall we.");
                MCDawn_.Gui.Program.Main(args);
            }
            exit:
            Console.WriteLine("HURR DURR... Bye C:");
        }
    }
}
