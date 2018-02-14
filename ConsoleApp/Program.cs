using System;
using log4net;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var logger = LogManager.GetLogger(typeof(Program));
                logger.Info("My message");
                throw new Exception("Test");
            }
            catch (Exception exception)
            {
                var logger = LogManager.GetLogger(typeof(Program));
                logger.Error("My error", exception);
            }

            Console.ReadKey();
            LogManager.Flush(0);
        }
    }
}
