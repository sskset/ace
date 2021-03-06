﻿using System;
using System.Threading.Tasks;

namespace ACE
{
    /// <summary>
    /// EventStation
    /// </summary>
    public interface IEventStation
    {
        /// <summary>
        /// Direct handle a event in current thread.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        void Invoke<T>(T @event) where T : IEvent;

        /// <summary>
        /// Subscribe event from RabbitMQ.
        /// </summary>
        /// <param name="subscriptionId">A unique identifier for the subscription. Two subscriptions with the same subscriptionId and type will get messages delivered in turn. </param>
        /// <param name="topic">RabbitMQ exchange routing key</param>
        void Subscribe(string subscriptionId, string[] topics, bool withAutoDelete = false, bool exclusive = false);

        /// <summary>
        /// Subscribe event from RabbitMQ in parallel.
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="topic"></param>
        /// <param name="capacity">Worker numbers</param>
        void SubscribeInParallel(string subscriptionId, string[] topics, int capacity, bool withAutoDelete = false, bool exclusive = false);

        void SubscribeAsync(string subscriptionId, string[] topics, Func<IEvent, Task> onMessage, bool withAutoDelete = false, bool exclusive = false);

        void Subscribe(string subscriptionId, string[] topics, Action<IEvent> onMessage, bool withAutoDelete = false, bool exclusive = false);
    }
}
