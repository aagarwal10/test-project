using Microsoft.Extensions.Configuration;
using System;
using test_service_interfaces;

namespace test_service_implementation
{
    public class MessageService : IMessageService
    {
        private readonly IConfiguration _configuration;

        public IWriter Writer { get; private set; }

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

            //Strategy pattern 
            Writer = (currentWriterType.ToUpper()) switch
            {
                "CONSOLE" => new ConsoleWriter(),
                "DATABASE" => new DbWriter(),
                _ => throw new ApplicationException("No supported writer type configured correctly. " +
                                                    "Please check 'CurrentWriterType' in appsetting.json"),
            };

            Writer.Write(message);
        }
    }
}