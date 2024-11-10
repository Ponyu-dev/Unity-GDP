using System.Collections.Generic;
using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Team;
using Game.Scripts.Contexts.Base.EntityPool;
using Game.Scripts.Contexts.ZombieContext.MovementSystem;
using UnityEngine;

namespace Game.Scripts.Contexts.ZombieContext
{
    public sealed class ZombieContextInstaller : SceneContextInstallerBase
    {
        [SerializeField] private SceneEntity player;
        [SerializeField] private TeamType teamType;
        
        [SerializeField] private SceneEntity prefabZombie;
        
        [SerializeField] private int initialPoolCount;
        [SerializeField] private Transform poolTransform;
        [SerializeField] private float spawnPeriod = 2;
        [SerializeField] private int zombiesActiveMax = 5;
        [SerializeField] private List<Bounds> spawnAreas = new();

        public override void Install(IContext context)
        {
            context.AddAttackPlayer(new Const<IEntity>(player));
            context.AddZombieActiveMax(new Const<int>(zombiesActiveMax));
            
            var worldTransform = context.GetWorldTransform();
            var zombieSystemData = new ZombieSystemData
            {
                pool = new ScenePool(prefabZombie, poolTransform, worldTransform, initialPoolCount),
                spawnAreas = spawnAreas,
                spawnCycle = new Cycle(spawnPeriod)
            };
            context.AddZombieSystemData(zombieSystemData);

            context.AddSystem<ZombieSpawnSystem>();
            context.AddSystem<ZombieMovementSystem>();
        }
    }
}