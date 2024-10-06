using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace _EventBus.Scripts.ServiceEventBus
{
    [UsedImplicitly]
    public sealed class EventBus
    {
        private readonly Dictionary<Type, IEventHandlerCollection> _handlers = new();

        public void Subscribe<T>(Action<T> handler)
        {
            var evtType = typeof(T);

            if (!_handlers.TryGetValue(evtType, out var handlerCollection))
            {
                handlerCollection = new EventHandlerCollection<T>();
                _handlers[evtType] = handlerCollection;
            }

            handlerCollection.Subscribe(handler);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            var evtType = typeof(T);
            if (_handlers.TryGetValue(evtType, out var handlerCollection))
            {
                handlerCollection.Unsubscribe(handler);
            }
        }

        public void RaiseEvent<T>(T evt)
        {
            var evtType = evt.GetType();
            Debug.Log(evtType);

            if (!_handlers.TryGetValue(evtType, out var handlerCollection))
            {
                Debug.LogWarning($"No subscribers found for type: {evtType}");
                return;
            }

            try
            {
                handlerCollection.RaiseEvent(evt);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error occurred while raising event: {ex}");
            }
        }

        private interface IEventHandlerCollection
        {
            void Subscribe(Delegate handler);
            void Unsubscribe(Delegate handler);
            void RaiseEvent<TEvent>(TEvent evt);
        }

        private sealed class EventHandlerCollection<T> : IEventHandlerCollection
        {
            private readonly HashSet<Delegate> _handlers = new();

            public void Subscribe(Delegate handler)
            {
                _handlers.Add(handler);
            }

            public void Unsubscribe(Delegate handler)
            {
                _handlers.Remove(handler);
            }

            public void RaiseEvent<TEvent>(TEvent evt)
            {
                if (evt is not T concreteEvent) return;

                foreach (var handler in _handlers)
                {
                    if (handler is Action<T> action)
                        action.Invoke(concreteEvent);
                }
            }
        }
    }
}