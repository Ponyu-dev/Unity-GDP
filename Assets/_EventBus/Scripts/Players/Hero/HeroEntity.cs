using System;
using System.Collections.Generic;
using _EventBus.Scripts.Players.Components;
using _EventBus.Scripts.Players.Player;
using UnityEngine;

namespace _EventBus.Scripts.Players.Hero
{
    //TODO Может быть сделать Transient
    public interface IHeroEntity
    {
        public PlayerType PlayerType { get; }
        public Guid Id { get; }
        public void AddComponent<T>(T component);
        public void RemoveComponent<T>();
        public bool HasComponent<T>();
        public T GetComponent<T>();
        public bool TryGetComponent<T>(out T component);
        public Dictionary<Type, object> GetAllComponents();
    }
    
    public class HeroEntity : IHeroEntity
    {
        public PlayerType PlayerType { get; private set; }
        public Guid Id { get; private set; }
        private readonly Dictionary<Type, object> _components;
        
        public HeroEntity(PlayerType playerType, HeroConfig heroConfig)
        {
            Id = Guid.NewGuid();
            PlayerType = playerType;
            _components = new Dictionary<Type, object>()
            {
                { typeof(HitPointsComponent), new HitPointsComponent(heroConfig.health) },
                { typeof(AttackComponent), new AttackComponent(heroConfig.damage) },
            };
            Debug.Log($"[HeroEntity] Constructor {PlayerType} {Id} {heroConfig.type}");
        }

        public void AddComponent<T>(T component)
        {
            _components[typeof(T)] = component;
        }

        public void RemoveComponent<T>()
        {
            _components.Remove(typeof(T));
        }

        public bool HasComponent<T>()
        {
            return _components.ContainsKey(typeof(T));
        }

        public T GetComponent<T>()
        {
            if (_components.TryGetValue(typeof(T), out var component))
            {
                return (T)component;
            }
            throw new InvalidOperationException($"Entity does not have component of type {typeof(T)}");
        }

        public bool TryGetComponent<T>(out T component)
        {
            if (_components.TryGetValue(typeof(T), out var obj) && obj is T c)
            {
                component = c;
                return true;
            }
            component = default;
            return false;
        }

        public Dictionary<Type, object> GetAllComponents()
        {
            return _components;
        }
    }
}