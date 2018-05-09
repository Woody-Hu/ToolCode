using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOIUtility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolCode;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MutexLock useLocak = new MutexLock("aaaaa");

            useLocak.LockAction(TestMethod);

            Console.Read();

        }

        private static void TestMethod()
        {
            for (int i = 0; i < 100; i++)
            {
                System.Threading.Thread.Sleep(100);
                Console.WriteLine(i.ToString());
            }
        }
    }
}
