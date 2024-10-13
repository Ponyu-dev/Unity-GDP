using System.Collections.Generic;
using System.Linq;
using _RTS.Scripts.ECS.SpawnStrategy;
using _RTS.Scripts.ECS.SpawnStrategy.Base;
using _RTS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;

namespace _RTS.Scripts.ECS.Systems
{
    public class CubeSpawnSystem : IEcsInitSystem
    {
        private readonly IReadOnlyList<ISpawnStrategy> _spawnStrategies;
        
        private readonly Transform _redContainer;
        private readonly Transform _blueContainer;
        private readonly GameObject _redCubePrefab;
        private readonly GameObject _blueCubePrefab;
        private readonly int _countArmy = 10; // Общее кол-во кубов
        private readonly float _spacing = 2f; // Зазор между кубами

        [Inject]
        public CubeSpawnSystem(
            Transform redContainer,
            Transform blueContainer,
            GameObject redCubePrefab,
            GameObject blueCubePrefab,
            IReadOnlyList<ISpawnStrategy> spawnStrategies)
        {
            Debug.Log("CubeSpawnSystem Constructor");
            _redCubePrefab = redCubePrefab;
            _redContainer = redContainer;
            _blueCubePrefab = blueCubePrefab;
            _blueContainer = blueContainer;
            _spawnStrategies = spawnStrategies;
        }

        public void Init(IEcsSystems systems)
        {
            // Получаем экземпляр мира по умолчанию.
            var world = systems.GetWorld();

            SpawnArmy(_spawnStrategies.OfType<SquareSpawnStrategy>().First(), world, _redContainer, _countArmy, Team.Red, _redCubePrefab, _spacing);
            SpawnArmy(_spawnStrategies.OfType<TriangleSpawnStrategy>().First(), world, _blueContainer, _countArmy, Team.Blue, _blueCubePrefab, _spacing);
        }

        private void SpawnArmy(ISpawnStrategy spawnStrategy,EcsWorld world, Transform container, int count, Team team, GameObject prefab, float spacing)
        {
            spawnStrategy.SpawnArmy(world, container, count, team, prefab, _spacing);
        }
    }
}