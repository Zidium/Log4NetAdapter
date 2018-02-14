using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Xunit;
using Zidium;
using Zidium.Api;

namespace Log4NetAppender.Tests
{
    public class LoggerTests
    {
        [Fact]
        public void LogDebugTest()
        {
            var logMessages = new List<Tuple<Guid, LogMessage>>();

            var client = Client.Instance;
            client.WebLogManager.OnAddLogMessage += (component, message) =>
            {
                logMessages.Add(new Tuple<Guid, LogMessage>(component.Info.Id, message));
            };

            var events = new List<SendEventBase>();
            client.EventPreparer = new EventPreparer(events);

            var loggerName = Guid.NewGuid().ToString();
            var logger = LogManager.GetLogger(loggerName);
            var text = "Message." + Guid.NewGuid();
            logger.Debug(text);

            Assert.Equal(1, logMessages.Count);
            var logMessage = logMessages[0];
            Assert.Equal(ComponentId, logMessage.Item1);
            Assert.Equal(LogLevel.Debug, logMessage.Item2.Level);
            Assert.Equal(text, logMessage.Item2.Message);
            Assert.Equal(loggerName, logMessage.Item2.Context);

            Assert.Equal(0, events.Count);

            LogManager.Flush(0);
        }

        [Fact]
        public void LogInfoTest()
        {
            var logMessages = new List<Tuple<Guid, LogMessage>>();

            var client = Client.Instance;
            client.WebLogManager.OnAddLogMessage += (component, message) =>
            {
                logMessages.Add(new Tuple<Guid, LogMessage>(component.Info.Id, message));
            };

            var events = new List<SendEventBase>();
            client.EventPreparer = new EventPreparer(events);

            var loggerName = Guid.NewGuid().ToString();
            var logger = LogManager.GetLogger(loggerName);
            var text = "Message." + Guid.NewGuid();
            logger.Info(text);

            Assert.Equal(1, logMessages.Count);
            var logMessage = logMessages[0];
            Assert.Equal(ComponentId, logMessage.Item1);
            Assert.Equal(LogLevel.Info, logMessage.Item2.Level);
            Assert.Equal(text, logMessage.Item2.Message);
            Assert.Equal(loggerName, logMessage.Item2.Context);

            Assert.Equal(0, events.Count);

            LogManager.Flush(0);
        }

        [Fact]
        public void LogWarnTest()
        {
            var logMessages = new List<Tuple<Guid, LogMessage>>();

            var client = Client.Instance;
            client.WebLogManager.OnAddLogMessage += (component, message) =>
            {
                logMessages.Add(new Tuple<Guid, LogMessage>(component.Info.Id, message));
            };

            var events = new List<SendEventBase>();
            client.EventPreparer = new EventPreparer(events);

            var loggerName = Guid.NewGuid().ToString();
            var logger = LogManager.GetLogger(loggerName);
            var text = "Message." + Guid.NewGuid();
            logger.Warn(text);

            Assert.Equal(1, logMessages.Count);
            var logMessage = logMessages[0];
            Assert.Equal(ComponentId, logMessage.Item1);
            Assert.Equal(LogLevel.Warning, logMessage.Item2.Level);
            Assert.Equal(text, logMessage.Item2.Message);
            Assert.Equal(loggerName, logMessage.Item2.Context);

            Assert.Equal(1, events.Count);

            LogManager.Flush(0);
        }

        [Fact]
        public void LogErrorTest()
        {
            var logMessages = new List<Tuple<Guid, LogMessage>>();

            var client = Client.Instance;
            client.WebLogManager.OnAddLogMessage += (component, message) =>
            {
                logMessages.Add(new Tuple<Guid, LogMessage>(component.Info.Id, message));
            };

            var events = new List<SendEventBase>();
            client.EventPreparer = new EventPreparer(events);

            var loggerName = Guid.NewGuid().ToString();
            var logger = LogManager.GetLogger(loggerName);
            var text = "Message." + Guid.NewGuid();
            logger.Error(text);

            Assert.Equal(1, logMessages.Count);
            var logMessage = logMessages[0];
            Assert.Equal(ComponentId, logMessage.Item1);
            Assert.Equal(LogLevel.Error, logMessage.Item2.Level);
            Assert.Equal(text, logMessage.Item2.Message);
            Assert.Equal(loggerName, logMessage.Item2.Context);

            Assert.Equal(1, events.Count);

            LogManager.Flush(0);
        }

        [Fact]
        public void LogFatalTest()
        {
            var logMessages = new List<Tuple<Guid, LogMessage>>();

            var client = Client.Instance;
            client.WebLogManager.OnAddLogMessage += (component, message) =>
            {
                logMessages.Add(new Tuple<Guid, LogMessage>(component.Info.Id, message));
            };

            var events = new List<SendEventBase>();
            client.EventPreparer = new EventPreparer(events);

            var loggerName = Guid.NewGuid().ToString();
            var logger = LogManager.GetLogger(loggerName);
            var text = "Message." + Guid.NewGuid();
            logger.Fatal(text);

            Assert.Equal(1, logMessages.Count);
            var logMessage = logMessages[0];
            Assert.Equal(ComponentId, logMessage.Item1);
            Assert.Equal(LogLevel.Fatal, logMessage.Item2.Level);
            Assert.Equal(text, logMessage.Item2.Message);
            Assert.Equal(loggerName, logMessage.Item2.Context);

            Assert.Equal(1, events.Count);

            LogManager.Flush(0);
        }

        [Fact]
        public void LogExceptionTest()
        {
            var logMessages = new List<Tuple<Guid, LogMessage>>();

            var client = Client.Instance;
            client.WebLogManager.OnAddLogMessage += (component, message) =>
            {
                logMessages.Add(new Tuple<Guid, LogMessage>(component.Info.Id, message));
            };

            var loggerName = Guid.NewGuid().ToString();
            var logger = LogManager.GetLogger(loggerName);
            var exception = new Exception("Test");
            logger.Error("My exception", exception);

            Assert.Equal(1, logMessages.Count);
            var logMessage = logMessages[0];
            Assert.Equal(ComponentId, logMessage.Item1);
            Assert.Equal(LogLevel.Error, logMessage.Item2.Level);
            Assert.Equal("My exception : Test", logMessage.Item2.Message);
            Assert.Equal(loggerName, logMessage.Item2.Context);

            Assert.Equal(1, logMessage.Item2.Properties.Count);
            var prop = logMessage.Item2.Properties.First();
            Assert.Equal("Stack", prop.Name);

            LogManager.Flush(0);
        }

        private Guid ComponentId
        {
            get { return new Guid(((Log4NetAppenderLog)LogManager.GetRepository().GetAppenders().First(t => t.Name == "ZidiumLog")).ComponentId); }
        }
    }
}
