using System;
using System.Linq;
using System.Threading;

namespace ConsoleAppWithExit
{
    class Program
    {
        // NOTE: Console app that exits when user type CTRL-C....
        private static bool cancel = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Application has started . Pressed CTRL-C to end...");

            var mainThread = new Thread(Worker);
            mainThread.Start();

            var autoResetEvent = new AutoResetEvent(false);
            Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    eventArgs.Cancel = true;
                    autoResetEvent.Set();
                };

            autoResetEvent.WaitOne();
            cancel = true;
            Console.WriteLine("Shutting down..");
        }

        private static void Worker()
        {
            while (!cancel)
            {
                Console.WriteLine("Worker is working...");
                Thread.Sleep(1000);
            }

            Console.WriteLine("Worker thred is done!!!!");
        }
    }
}
