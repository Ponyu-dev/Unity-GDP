using System;
using System.Collections.Generic;
using System.Linq;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Players.Hero;
using _EventBus.Scripts.Players.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _EventBus.Scripts.Game.Managers
{
    public interface ITurnManager
    {
        event Action<PlayerType?> OnGameFinish;
        public void Initialize();
        public void StartTurn();
        public void ClearAll();
    }
    
    public class TurnManager : ITurnManager, IDisposable
    {
        public event Action<PlayerType?> OnGameFinish;
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
            _eventBus.Subscribe<NextHeroEvent>(OnNextHero);
        }

        public void Initialize()
        {
            Debug.Log("[TurnManager] Initialize");
            if (!_heroFactory.GetAllEntities().Any())
            {
                Debug.Log("In _heroFactory GetAllEntities is null or empty");
                return;
            }
            
            //TODO Если хотим что бы сперва ходили все герои одного игрока.
            //foreach (var hero in _heroFactory.GetAllEntities())
            //  _turnQueue.Enqueue(hero);
            
            //TODO Для очередности хода
            ReorderQueue();
        }
        
        private void ReorderQueue()
        {
            // Получаем все элементы сразу
            var entities = _heroFactory.GetAllEntities().ToList();

            // Разделяем на два списка по типам
            var redHeroes = entities.Where(hero => hero.PlayerType == PlayerType.Red).ToList();
            var blueHeroes = entities.Where(hero => hero.PlayerType == PlayerType.Blue).ToList();

            // Определяем минимальную длину, чтобы чередовать по одному элементу
            var minCount = Math.Min(redHeroes.Count, blueHeroes.Count);
            int redIndex = 0, blueIndex = 0;

            // Чередуем элементы
            for (var i = 0; i < minCount * 2; i++)
            {
                if (i % 2 == 0 && redIndex < redHeroes.Count)
                {
                    _turnQueue.Enqueue(redHeroes[redIndex++]);
                }
                else if (blueIndex < blueHeroes.Count)
                {
                    _turnQueue.Enqueue(blueHeroes[blueIndex++]);
                }
            }

            // Добавляем оставшиеся элементы, если в одном из списков их больше
            for (int i = redIndex, count = redHeroes.Count; i < count; i++)
            {
                _turnQueue.Enqueue(redHeroes[i]);
            }

            for (int i = blueIndex, count = blueHeroes.Count; i < count; i++)
            {
                _turnQueue.Enqueue(blueHeroes[i]);
            }
        }

        public void StartTurn()
        {
            Debug.Log("[TurnManager] StartTurn");
            StartNextTurn();
        }

        public void ClearAll()
        {
            _turnQueue.Clear();
            _heroFactory.ClearAll();
        }

        private void StartNextTurn()
        {
            Debug.Log("[TurnManager] StartNextTurn");
            if (!_heroFactory.TryGetMissingPlayerType(out var remainingType))
            {
                // Обработка конца игры
                OnGameFinish?.Invoke(remainingType);
                return;
            }

            //Берем первого героя в очереди.
            var currentEntity = _turnQueue.Dequeue();
            //Переносим его в конец.
            _turnQueue.Enqueue(currentEntity);

            //Запускаем у него старт хода.
            _eventBus.RaiseEvent(new TurnStartedEvent(currentEntity)).Forget();
        }

        private void OnHeroDied(DiedEvent evt)
        {
            Debug.Log("[TurnManager] OnHeroDied");
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
        }

        private void OnNextHero(NextHeroEvent evt)
        {
            StartNextTurn();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<DiedEvent>(OnHeroDied);
            _eventBus.Unsubscribe<NextHeroEvent>(OnNextHero);
        }
    }

}