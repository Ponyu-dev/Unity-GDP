using System.Collections.Generic;
using System.Linq;
using CubeECS.Scripts.ECS.SpawnStrategy;
using CubeECS.Scripts.ECS.SpawnStrategy.Base;
using CubeECS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;

namespace CubeECS.Scripts.ECS.Systems
{
    public class SpawnSystem : IEcsInitSystem
    {
        private readonly IReadOnlyList<ISpawnStrategy> _spawnStrategies;
        
        private readonly Transform _redContainer;
        private readonly Transform _blueContainer;
        private readonly GameObject _redCubePrefab;
        private readonly GameObject _blueCubePrefab;
        private readonly int _countArmy = 50; // Общее кол-во кубов
        private readonly float _spacing = 2f; // Зазор между кубами

        [Inject]
        public SpawnSystem(
            Transform redContainer,
            Transform blueContainer,
            GameObject redCubePrefab,
            GameObject blueCubePrefab,
            IReadOnlyList<ISpawnStrategy> spawnStrategies)
        {
            Debug.Log("SpawnSystem Constructor");
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

            SpawnArmy(_spawnStrategies.OfType<SquareSpawnStrategy>().First(), world, _redContainer, _blueContainer.position, _countArmy, Team.Red, _redCubePrefab, _spacing);
            SpawnArmy(_spawnStrategies.OfType<SquareSpawnStrategy>().First(), world, _blueContainer, _redContainer.position, _countArmy, Team.Blue, _blueCubePrefab, _spacing);

            //SpawnArmy(_spawnStrategies.OfType<CircleSpawnStrategy>().First(), world, _redContainer, _blueContainer.position, _countArmy, Team.Red, _redCubePrefab, _spacing);
            //SpawnArmy(_spawnStrategies.OfType<DiamondSpawnStrategy>().First(), world, _blueContainer, _redContainer.position, _countArmy, Team.Blue, _blueCubePrefab, _spacing);
            //SpawnArmy(_spawnStrategies.OfType<SquareSpawnStrategy>().First(), world, _redContainer, _blueContainer.position, _countArmy, Team.Red, _redCubePrefab, _spacing);
            //SpawnArmy(_spawnStrategies.OfType<TriangleSpawnStrategy>().First(), world, _blueContainer,_redContainer.position, _countArmy, Team.Blue, _blueCubePrefab, _spacing);
        }

        private void SpawnArmy(ISpawnStrategy spawnStrategy, EcsWorld world, Transform container, Vector3 direction, int count, Team team, GameObject prefab, float spacing)
        {
            spawnStrategy.SpawnArmy(world, container, direction, count, team, prefab, _spacing);
        }
    }
}