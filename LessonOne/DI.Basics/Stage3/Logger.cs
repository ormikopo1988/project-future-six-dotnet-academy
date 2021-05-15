using System;

namespace DI.Basics.Stage3
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("Message logged: {0}", message);
        }
    }

    public interface ILogger
    {
        void Log(string message);
    }
}