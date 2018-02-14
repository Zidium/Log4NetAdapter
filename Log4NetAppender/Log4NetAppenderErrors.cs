using System;
using System.Collections.Generic;
using log4net.Appender;
using log4net.Core;
using Zidium.Api;

namespace Zidium
{
    public class Log4NetAppenderErrors : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            var level = LogLevelHelper.GetLogLevel(loggingEvent.Level);
            if (level <= LogLevel.Info)
                return;

            var message = loggingEvent.RenderedMessage;

            var errorData = Component.Client.ExceptionRender.CreateEventFromLog(Component, level, loggingEvent.ExceptionObject, message, new Dictionary<string, object>());
            errorData.Add();
        }

        private IComponentControl _component;

        private IComponentControl Component
        {
            get
            {
                if (_component == null)
                {
                    _component = _componentId != null ? Client.Instance.GetComponentControl(_componentId.Value) : Client.Instance.GetDefaultComponentControl();
                }
                return _component;
            }
        }

        private Guid? _componentId;

        public string ComponentId
        {
            get { return _componentId != null ? _componentId.ToString() : null; }
            set { _componentId = !string.IsNullOrEmpty(value) ? new Guid(value) : (Guid?)null; }
        }

        public override bool Flush(int millisecondsTimeout)
        {
            Client.Instance.EventManager.Flush();
            return true;
        }
    }
}
