using System;
using System.Collections.Generic;
using _EventBus.Scripts.Players.Hero;
using _EventBus.Scripts.Players.Player;
using UnityEngine;
using VContainer;

namespace _EventBus.Scripts.Game.Factories
{
    //TODO Может быть вообще не нужный класс.
    public interface IHeroFactory
    {
        public IHeroEntity CreateEntity(PlayerType playerType, HeroConfig heroConfig);
        public void RemoveEntity(Guid entityId);
        public IHeroEntity GetEntity(Guid entityId);
        public IEnumerable<IHeroEntity> GetAllEntities();
    }
    
    public class HeroFactory : IHeroFactory
    {
        private readonly Dictionary<Guid, IHeroEntity> _entity = new();

        [Inject]
        public HeroFactory()
        {
            Debug.Log("[HeroFactory] Constructor");
        }
        
        public IHeroEntity CreateEntity(PlayerType playerType, HeroConfig heroConfig)
        {
            Debug.Log("[HeroFactory] CreateEntity");
            var entity = new HeroEntity(playerType, heroConfig);
            _entity[entity.Id] = entity;
            return entity;
        }

        public void RemoveEntity(Guid entityId)
        {
            _entity.Remove(entityId);
        }

        public IHeroEntity GetEntity(Guid entityId)
        {
            if (_entity.TryGetValue(entityId, out var entity))
            {
                return entity;
            }
            throw new InvalidOperationException($"HeroEntity with ID {entityId} does not exist.");
        }

        public IEnumerable<IHeroEntity> GetAllEntities()
        {
            return _entity.Values;
        }
    }
}