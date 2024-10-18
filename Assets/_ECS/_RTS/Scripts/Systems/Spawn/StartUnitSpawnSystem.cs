using _ECS._RTS.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace _ECS._RTS.Scripts.Systems
{
    public class StartUnitSpawnSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<ContainerView, SpawnPoints, SpawnRotation, SpawnPrefabs, Base>> _filter;
        private readonly EcsCustomInject<EntityManager> _entityManager;
        
        public void Init(IEcsSystems systems)
        {
            var containerViewPool = _filter.Pools.Inc1;
            var spawnPointsPool = _filter.Pools.Inc2;
            var spawnRotationPool = _filter.Pools.Inc3;
            var spawnPrefabsPool = _filter.Pools.Inc4;
            
            foreach (var entity in _filter.Value)
            {
                var container = containerViewPool.Get(entity).Value;
                var spawnRotation = spawnRotationPool.Get(entity).Value;
                var prefabs = spawnPrefabsPool.Get(entity).Value;

                for (int i = 0, count = prefabs.Length; i < count; i++)
                {
                    var prefab = prefabs[i];
                    var position = spawnPointsPool.Get(entity).Value[i];
                    
                    _entityManager.Value.Create(prefab, position, spawnRotation, container);
                }
            }
        }
    }
}