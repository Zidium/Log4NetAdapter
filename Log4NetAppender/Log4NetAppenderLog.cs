using System;
using log4net.Appender;
using log4net.Core;
using Zidium.Api;

namespace Zidium
{
    public class Log4NetAppenderLog : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            var level = LogLevelHelper.GetLogLevel(loggingEvent.Level);
            var log = Component.Log.GetTaggedCopy(loggingEvent.LoggerName);
            var message = loggingEvent.RenderedMessage;
            log.Write(level, message, loggingEvent.ExceptionObject);
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
            Client.Instance.WebLogManager.Flush();
            return true;
        }
    }
}
