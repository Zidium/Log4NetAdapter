using System.Collections.Generic;
using Zidium.Api;

namespace Log4NetAppender.Tests
{
    internal class EventPreparer : IEventPreparer
    {
        public EventPreparer(List<SendEventBase> events)
        {
            _events = events;
        }

        private List<SendEventBase> _events;

        public void Prepare(SendEventBase eventObj)
        {
            _events.Add(eventObj);
        }
    }
}
