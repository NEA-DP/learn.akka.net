using System;

namespace SharedActors.Messages
{
    public class DeviceEventMessage
    {
        public Guid Id { get; }
        public string Value { get; }

        public DeviceEventMessage(Guid id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}