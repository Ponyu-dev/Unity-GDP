using System;
using System.Collections.Generic;
using _EventBus.Scripts.Players.Components;
using _EventBus.Scripts.Players.Player;
using UnityEngine;
using Random = System.Random;

namespace _EventBus.Scripts.Players.Hero
{
    //TODO Может быть сделать Transient
    public interface IHeroEntity
    {
        public PlayerType PlayerType { get; }
        public HeroType HeroType { get; }
        public HeroConfig Config { get; }

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
        public HeroType HeroType => Config.type;
        public HeroConfig Config { get; private set; }

        public AudioClip StartTurnClip()
        {
            var count = Config.clipsStartTurn.Length;
            return Config.clipsStartTurn[_random.Next(count)];
        }

        public AudioClip LowHealthClip() => Config.clipsLowHealth;

        public AudioClip AbilityClip()
        {
            throw new NotImplementedException();
        }

        public AudioClip DeathClip() => Config.clipsDeath;

        private readonly Dictionary<Type, object> _components;
        
        public HeroEntity(PlayerType playerType, HeroConfig heroConfig)
        {
            PlayerType = playerType;
            Config = heroConfig;
            _components = new Dictionary<Type, object>()
            {
                { typeof(HitPointsComponent), new HitPointsComponent(Config.health) },
                { typeof(AttackComponent), new AttackComponent(Config.damage) },
            };
            Debug.Log($"[HeroEntity] Constructor {PlayerType} {Config.type}");
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