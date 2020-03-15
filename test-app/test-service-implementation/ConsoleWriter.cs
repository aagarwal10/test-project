using System;
using test_service_interfaces;

namespace test_service_implementation
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("message", nameof(message));
            }

            Console.WriteLine(message);
        }
    }
}
