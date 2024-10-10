using System;
using System.Collections.Generic;
using System.Linq;
using _EventBus.Scripts.Game.Presenters;
using _EventBus.Scripts.Players.Components;
using _EventBus.Scripts.Players.Hero;
using _EventBus.Scripts.Players.Player;
using UnityEngine;
using VContainer;
using Random = System.Random;

namespace _EventBus.Scripts.Game.Factories
{
    public interface IHeroFactory
    {
        public IHeroEntity CreateEntity(PlayerType playerType, HeroConfig heroConfig);
        public void RemoveEntity(HeroType entityId);
        public bool HasEntity(HeroType entityId);
        public IHeroEntity GetRandomEntity(IHeroEntity hero);
        public IEnumerable<IHeroEntity> GetAllEntities();
        public IEnumerable<IHeroEntity> GetEntitiesByPredicate(Func<IHeroEntity, bool> predicate);
        public bool TryGetMissingPlayerType(out PlayerType? remainingType);
        public void ClearAll();
    }
    
    public class HeroFactory : IHeroFactory
    {
        private readonly Random _random = new();
        private readonly Dictionary<HeroType, IHeroEntity> _entity = new();

        [Inject]
        public HeroFactory()
        {
            Debug.Log("[HeroFactory] Constructor");
        }
        
        public IHeroEntity CreateEntity(PlayerType playerType, HeroConfig heroConfig)
        {
            Debug.Log("[HeroFactory] CreateEntity");
            var entity = new HeroEntity(playerType, heroConfig);
            _entity[entity.HeroType] = entity;
            return entity;
        }

        public void RemoveEntity(HeroType heroType)
        {
            _entity.Remove(heroType);
        }

        public bool HasEntity(HeroType heroType)
        {
            return _entity.ContainsKey(heroType);
        }

        public IHeroEntity GetRandomEntity(IHeroEntity hero)
        {
            // Отбираем всех Живых героев, которые не являются текущим и принадлежат другому игроку
            var validHeroes = _entity.Values
                .Where(it => it.GetComponent<HitPointsComponent>().Value > 0)
                .Where(it => it.PlayerType != hero.PlayerType)
                .Where(it => it.HeroType != hero.HeroType)
                .ToList();

            // Если нет подходящих героев, возвращаем null или выбрасываем исключение
            if (validHeroes.Count == 0)
            {
                throw new InvalidOperationException("No valid heroes available.");
            }

            // Выбираем случайного героя
            var randomHero = validHeroes[_random.Next(validHeroes.Count)];

            return randomHero;
        }
        
        public IEnumerable<IHeroEntity> GetAllEntities()
        {
            return _entity.Values;
        }

        public IEnumerable<IHeroEntity> GetEntitiesByPredicate(Func<IHeroEntity, bool> predicate)
        {
            return _entity.Values.Where(predicate);
        }
        
        public bool TryGetMissingPlayerType(out PlayerType? remainingType)
        {
            // Получаем уникальные типы игроков в списке героев
            var playerTypes = _entity.Values.Select(hero => hero.PlayerType).Distinct().ToList();

            // Проверяем наличие обоих типов игроков
            var hasRed = playerTypes.Contains(PlayerType.Red);
            var hasBlue = playerTypes.Contains(PlayerType.Blue);

            if (hasRed && hasBlue)
            {
                remainingType = null; // Оба типа присутствуют, нет отсутствующего типа
                return true;
            }

            // Если остался только один тип, возвращаем его как "оставшийся"
            remainingType = hasRed ? PlayerType.Red : hasBlue ? PlayerType.Blue : (PlayerType?)null;
            return false;
        }

        public void ClearAll()
        {
            foreach (var entity in _entity)
            {
                var destroy = entity.Value.GetComponent<DestroyComponent>();
                destroy.Destroy();
                
                entity.Value.RemoveComponent<IHeroPresenter>();
            }
            
            _entity.Clear();
        }
    }
}