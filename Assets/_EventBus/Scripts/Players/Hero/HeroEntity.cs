using System;
using System.Collections.Generic;
using _EventBus.Scripts.Players.Abilities;
using _EventBus.Scripts.Players.Abilities.Base;
using _EventBus.Scripts.Players.Components;
using _EventBus.Scripts.Players.Player;
using Sirenix.Utilities;
using UnityEngine;
using Random = System.Random;

namespace _EventBus.Scripts.Players.Hero
{
    public interface IHeroEntity
    {
        public PlayerType PlayerType { get; }
        public HeroType HeroType { get; }

        public AudioClip StartTurnClip();
        public AudioClip LowHealthClip();
        public AudioClip AbilityClip();
        public AudioClip DeathClip();
        public void AddComponent<T>(T component);
        public void RemoveComponent<T>();
        public bool HasComponent<T>();
        public T GetComponent<T>();
        public bool TryGetComponent<T>(out T component);
        public Dictionary<Type, object> GetAllComponents();
    }
    
    public class HeroEntity : IHeroEntity
    {
        private readonly Random _random = new();
        
        public PlayerType PlayerType { get; private set; }
        public HeroType HeroType => _config.type;
        private HeroConfig _config;

        public AudioClip StartTurnClip()
        {
            if (_config.clipsStartTurn.IsNullOrEmpty()) return null;
            
            var count = _config.clipsStartTurn.Length;
            return _config.clipsStartTurn[_random.Next(count)];
        }

        public AudioClip LowHealthClip() => _config.clipsLowHealth;

        public AudioClip AbilityClip()
        {
            if (_config.clipsAbility.IsNullOrEmpty()) return default;
            
            var count = _config.clipsAbility.Length;
            return _config.clipsAbility[_random.Next(count)];
        }

        public AudioClip DeathClip() => _config.clipsDeath;

        private readonly Dictionary<Type, object> _components;
        
        public HeroEntity(PlayerType playerType, HeroConfig heroConfig)
        {
            PlayerType = playerType;
            _config = heroConfig;
            _components = new Dictionary<Type, object>()
            {
                { typeof(HitPointsComponent), new HitPointsComponent(_config.health) },
                { typeof(AttackComponent), new AttackComponent(_config.damage) },
                { typeof(IAbility), heroConfig.GetAbility()}
            };
            Debug.Log($"[HeroEntity] Constructor {PlayerType} {_config.type}");
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