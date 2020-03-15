using System;
using test_service_interfaces;

namespace test_service_implementation
{
    public class DbWriter : IWriter
    {
        public void Write(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("message", nameof(message));
            }

            //Setup dbContext (EF Core) to write & saveChanges()
        }
    }
}
