using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace _EventBus.Scripts
{
    [UsedImplicitly]
    public sealed class EventBus
    {
        private readonly Dictionary<Type, IEventHandlerCollection> _handlers = new();

        public void Subscribe<T>(Action<T> handler) // Синхронный обработчик
        {
            var evtType = typeof(T);

            if (!_handlers.TryGetValue(evtType, out var handlerCollection))
            {
                handlerCollection = new EventHandlerCollection<T>();
                _handlers[evtType] = handlerCollection;
            }

            handlerCollection.Subscribe(handler);
        }

        public void Subscribe<T>(Func<T, UniTask> handler) // Асинхронный обработчик
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

        public void Unsubscribe<T>(Func<T, UniTask> handler)
        {
            var evtType = typeof(T);
            if (_handlers.TryGetValue(evtType, out var handlerCollection))
            {
                handlerCollection.Unsubscribe(handler);
            }
        }

        public async UniTask RaiseEvent<T>(T evt)
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
                await handlerCollection.RaiseEventWithDelay(evt); // Ожидание завершения всех асинхронных обработчиков
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
            UniTask RaiseEventWithDelay<TEvent>(TEvent evt); // Асинхронный вызов
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

            public async UniTask RaiseEventWithDelay<TEvent>(TEvent evt)
            {
                if (evt is not T concreteEvent) return;

                foreach (var handler in _handlers)
                {
                    if (handler is Action<T> syncHandler)
                    {
                        // Вызов синхронного обработчика
                        syncHandler.Invoke(concreteEvent);
                    }
                    else if (handler is Func<T, UniTask> asyncHandler)
                    {
                        // Вызов асинхронного обработчика
                        await asyncHandler(concreteEvent);
                    }
                }
            }
        }
    }
}
