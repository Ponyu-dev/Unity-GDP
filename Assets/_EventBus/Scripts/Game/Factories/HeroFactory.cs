using System;
using System.Collections.Generic;
using System.Linq;
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
        public IHeroEntity GetEntity(HeroType entityId);
        public IHeroEntity GetRandomEntity(IHeroEntity hero);
        public IEnumerable<IHeroEntity> GetAllEntities();
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

        public IHeroEntity GetEntity(HeroType heroType)
        {
            if (_entity.TryGetValue(heroType, out var entity))
            {
                return entity;
            }
            throw new InvalidOperationException($"HeroEntity with ID {heroType} does not exist.");
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
    }
}