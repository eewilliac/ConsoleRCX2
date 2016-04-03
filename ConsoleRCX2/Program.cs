using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ConsoleRCX2
{
    class Program
    {
        static void Main(string[] args)
        {
            RCXPort rcx = new RCXPort();
            rcx.send("51 1");
            Thread.Sleep(1000);
            rcx.send("10");
            Thread.Sleep(1000);
            rcx.send("51 2");
            Thread.Sleep(1000);
            rcx.send("10");
            Thread.Sleep(1000);
            rcx.send("51 3");
            Thread.Sleep(1000);
            rcx.send("10");
            Thread.Sleep(1000);
            rcx.send("51 4");
            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
