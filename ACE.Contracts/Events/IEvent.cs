﻿namespace ACE
{
    /// <summary>
    /// Event can be handled by multiple method.
    /// Event handlers will run in ThreadPool.
    /// Event will also send to RabbitMQ exchange, and exchange will distribute this message to queues base on routing rules.
    /// Specific queue consumer will fetch each message and direct handle it in current thread.
    /// Event SHOULD NOT host big size entities since it will be serilized(json) then send to MQ and saved in event log.
    /// </summary>
    public interface IEvent
    {
    }
}
