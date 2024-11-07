using Atomic.Contexts;
using Atomic.Entities;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace Game.Scripts.Contexts.ZombieContext.Pool
{
    public static class ZombieSpawnUseCase
    {
        public static IEntity SpawnZombieInPosition(this IContext gameContext)
        {
            var spawnPoint = gameContext.RandomSpawnZombiePoint();
            return gameContext.SpawnZombie(spawnPoint);
        }

        public static IEntity SpawnZombie(this IContext gameContext, float3 spawnPoint)
        {
            var pool = gameContext.GetZombieSystemData().pool;
            var zombie = pool.Rent();
            zombie.GetPosition().Value = spawnPoint;
            return zombie;
        }

        //TODO: не очень нравиться постоянно вызывать gameContext.GetZombieSystemData().spawnAreas
        private static float3 RandomSpawnZombiePoint(this IContext gameContext)
        {
            var spawnAreas = gameContext.GetZombieSystemData().spawnAreas;
            var randomIndex = Random.Range(0, spawnAreas.Count);
            var spawnArea = spawnAreas[randomIndex];
            float3 min = spawnArea.min;
            float3 max = spawnArea.max;
            var spawnPoint = new float3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
            
            return spawnPoint;
        }
    }
}