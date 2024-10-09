using System;
using System.Collections.Generic;
using System.Linq;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Players.Hero;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _EventBus.Scripts.Game.Managers
{
    public interface ITurnManager
    {
        public void Initialize();
        public UniTaskVoid StartTurn();
    }
    
    public class TurnManager : ITurnManager, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly IHeroFactory _heroFactory;
        private readonly Queue<IHeroEntity> _turnQueue;
        
        [Inject]
        public TurnManager(
            EventBus eventBus,
            IHeroFactory heroFactory)
        {
            Debug.Log("[TurnManager] Constructor");
            _eventBus = eventBus;
            _turnQueue = new Queue<IHeroEntity>();
            _heroFactory = heroFactory;
            
            _eventBus.Subscribe<DiedEvent>(OnHeroDied);
            _eventBus.Subscribe<TurnEndedEvent>(OnTurnEnded);
        }

        public void Initialize()
        {
            Debug.Log("[TurnManager] Initialize");
            if (!_heroFactory.GetAllEntities().Any())
            {
                Debug.Log("In _heroFactory GetAllEntities is null or empty");
                return;
            }
            
            foreach (var hero in _heroFactory.GetAllEntities())
                _turnQueue.Enqueue(hero);
        }

        public async UniTaskVoid StartTurn()
        {
            Debug.Log("[TurnManager] StartTurn");
            await UniTask.Delay(3000);
            StartNextTurn();
        }

        private void StartNextTurn()
        {
            if (_turnQueue.Count == 0)
            {
                // Обработка конца игры
                return;
            }

            //Берем первого героя в очереди.
            var currentEntity = _turnQueue.Dequeue();
            //Переносим его в конец.
            _turnQueue.Enqueue(currentEntity);

            //Запускаем у него старт хода.
            _eventBus.RaiseEvent(new TurnStartedEvent(currentEntity));
        }

        private void OnHeroDied(DiedEvent evt)
        {
            // Удаление погибшего героя из очереди
            var newQueue = new Queue<IHeroEntity>();
            while (_turnQueue.Count > 0)
            {
                var entity = _turnQueue.Dequeue();
                if (entity.HeroType != evt.Target.HeroType)
                {
                    newQueue.Enqueue(entity);
                }
            }
            while (newQueue.Count > 0)
            {
                _turnQueue.Enqueue(newQueue.Dequeue());
            }

            StartNextTurn();
        }

        private void OnTurnEnded(TurnEndedEvent evt)
        {
            StartNextTurn();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<DiedEvent>(OnHeroDied);
            _eventBus.Unsubscribe<TurnEndedEvent>(OnTurnEnded);
        }
    }

}