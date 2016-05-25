using NServiceBus;
using System;

namespace ServiceShared
{
    public class SendJoke : IEvent
    {
        public Guid Id { get; set; }

        public string Message { get; set; }
    }
}