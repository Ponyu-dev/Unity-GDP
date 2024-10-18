using _ECS._RTS.Scripts.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS._RTS.Scripts.Systems
{
    public class UnitRandomSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnBaseConfig, Base>> _filter;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<SpawnRequest> _spawnPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Prefab> _prefabPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<ContainerView> _containerViewPool = EcsWorlds.EVENTS;

        private float _timeSinceLastSpawn = 0f; // Общий таймер для спавна
        private bool _firstSpawnDone = false; // Флаг для первого спавна
        private readonly float _spawnDelay = 1f; // Задержка спавна в секундах

        public void Init(IEcsSystems systems)
        {
            var spawnBaseConfigPool = _filter.Pools.Inc1;

            foreach (var entity in _filter.Value)
            {
                var config = spawnBaseConfigPool.Get(entity);
                
                // Спавним все объекты из конфигурации
                for (int i = 0, count = config.Prefabs.Value.Length; i < count; i++)
                {
                    Spawn(config, i, i); // Спавним каждый объект на свою позицию
                }
            }

            _firstSpawnDone = true; // Первый спавн завершен
        }

        public void Run(IEcsSystems systems)
        {
            if (!_firstSpawnDone) return; // Если первый спавн не завершен, выходим

            //TODO Вернуть потом когда закончу с механикой атаки и дамага.
            /*
            // Обновляем общий таймер
            _timeSinceLastSpawn += Time.deltaTime;

            // Проверяем, прошло ли время задержки
            if (!(_timeSinceLastSpawn >= _spawnDelay)) return;
            
            // Перебираем все базы
            foreach (var entity in _filter.Value)
            {
                var config = _filter.Pools.Inc1.Get(entity);
                // Спавним случайный объект и случайную позицию
                var randomPrefabIndex = Random.Range(0, config.Prefabs.Value.Length);
                var randomPositionIndex = Random.Range(0, config.Points.Value.Length);
                Spawn(config, randomPrefabIndex, randomPositionIndex);
            }

            // Сбрасываем таймер
            _timeSinceLastSpawn = 0f;*/
        }

        private void Spawn(SpawnBaseConfig config, int prefabIndex, int positionIndex)
        {
            var prefab = config.Prefabs.Value[prefabIndex];
            var position = config.Points.Value[positionIndex];
            var container = config.Container.Value;
            var rotation = config.Rotation.Value;

            var spawnEvent = _eventWorld.Value.NewEntity();

            _spawnPool.Value.Add(spawnEvent) = new SpawnRequest();
            _positionPool.Value.Add(spawnEvent) = new Position { Value = position };
            _rotationPool.Value.Add(spawnEvent) = new Rotation { Value = rotation };
            _prefabPool.Value.Add(spawnEvent) = new Prefab { Value = prefab };
            _containerViewPool.Value.Add(spawnEvent) = new ContainerView { Value = container };
        }
    }
}