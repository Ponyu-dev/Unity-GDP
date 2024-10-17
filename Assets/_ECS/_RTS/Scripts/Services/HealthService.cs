using System.Collections.Generic;
using _ECS._RTS.Scripts.Components;
using _ECS._RTS.Scripts.Reactivies;
using UnityEngine;
using VContainer;

namespace _ECS._RTS.Scripts.Services
{
    public class HealthService
    {
        private readonly Dictionary<int, ReactiveHealth> _healthMap = new();

        [Inject]
        public HealthService()
        {
            Debug.Log("[HealthService] Constructor");
        }
        
        public void RegisterEntity(int entityId, int initialHealth)
        {
            _healthMap[entityId] = new ReactiveHealth(initialHealth, initialHealth);
        }

        public void UnregisterEntity(int entityId)
        {
            _healthMap.Remove(entityId);
        }

        public ReactiveHealth GetHealth(int entityId)
        {
            return _healthMap.TryGetValue(entityId, out var health) ? health : null;
        }

        public void SyncFromEcs(int entityId, Health ecsHealth)
        {
            if (_healthMap.TryGetValue(entityId, out var reactiveHealth))
            {
                reactiveHealth.SyncFromEcs(ecsHealth);
            }
        }

        public void SyncToEcs(int entityId, ref Health ecsHealth)
        {
            if (_healthMap.TryGetValue(entityId, out var reactiveHealth))
            {
                reactiveHealth.SyncToEcs(ref ecsHealth);
            }
        }
    }
}