using Microsoft.Extensions.Configuration;
using System;
using test_service_interfaces;

namespace test_service_implementation
{
    public class MessageService : IMessageService
    {
        private readonly IConfiguration _configuration;

        public MessageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Write(string message)
        {
            var currentWriterType = _configuration["CurrentWriterType"];

            if (string.IsNullOrEmpty(currentWriterType))
            {
                throw new NullReferenceException("'CurrentWriterType' in appsetting.json is empty");
            }

            IWriter writer;

            //Strategy pattern 
            switch (currentWriterType.ToUpper())
            {
                case "CONSOLE":
                    writer = new ConsoleWriter();
                    break;

                case "DATABASE":
                    writer = new DbWriter();
                    break;

                default:
                    throw new ApplicationException("No supported writer type configured correctly. " +
                                                   "Please check 'CurrentWriterType' in appsetting.json");
            }

            writer.Write(message);
        }
    }
}