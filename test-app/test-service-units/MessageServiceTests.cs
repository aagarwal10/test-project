using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using test_service_implementation;

namespace test_service_units
{
    public class MessageServiceTests
    {
        private const string _message = "Hello World!!";

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ConsoleWriter_Success_Test()
        {
            var configurationMock = new Mock<IConfiguration>();

            configurationMock.Setup(c=> c[It.Is<string>(s => s == "CurrentWriterType")])
                             .Returns("console");

            var messageService = new MessageService(configurationMock.Object);

            messageService.Write(_message);

            var writerType = messageService.Writer.GetType();

            Assert.That(writerType == typeof(ConsoleWriter));
        }

        [Test]
        public void DbWriter_Success_Test()
        {
            var configurationMock = new Mock<IConfiguration>();

            configurationMock.Setup(c => c[It.Is<string>(s => s == "CurrentWriterType")])
                             .Returns("database");

            var messageService = new MessageService(configurationMock.Object);

            messageService.Write(_message);

            var writerType = messageService.Writer.GetType();

            Assert.That(writerType == typeof(DbWriter));
        }

        [Test]
        public void EmptyWriterType_ExpectedException_Test()
        {
            var configurationMock = new Mock<IConfiguration>();

            configurationMock.Setup(c => c[It.Is<string>(s => s == "CurrentWriterType")])
                             .Returns(string.Empty);

            var messageService = new MessageService(configurationMock.Object);

            var expectedException = Assert.Throws<NullReferenceException>(() => messageService.Write(_message));

            Assert.That(expectedException.Message.Equals("'CurrentWriterType' in appsetting.json is empty"));
        }

        [Test]
        public void UnSupportedWriterType_ExpectedException_Test()
        {
            var configurationMock = new Mock<IConfiguration>();

            configurationMock.Setup(c => c[It.Is<string>(s => s == "CurrentWriterType")])
                             .Returns("WebService");

            var messageService = new MessageService(configurationMock.Object);

            var expectedException = Assert.Throws<ApplicationException>(() => messageService.Write(_message));

            Assert.That(expectedException.Message.Equals("No supported writer type configured correctly. " +
                                                   "Please check 'CurrentWriterType' in appsetting.json"));
        }

    }
}