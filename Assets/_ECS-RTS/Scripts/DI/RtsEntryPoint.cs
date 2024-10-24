using _ECS_RTS.Scripts.EcsEngine.DI;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ECS_RTS.Scripts.DI
{
    public class RtsEntryPoint : LifetimeScope
    {
        [BoxGroup("Team Red"), SerializeField] private PoolEnemyConfigure poolEnemyRedConfigure;
        [BoxGroup("Team Blue"), SerializeField] private PoolEnemyConfigure poolEnemyBlueConfigure;

        private EcsSystemConfigure _ecsSystemConfigure = new();
        
        protected override void Awake()
        {
            base.Awake();
            Random.InitState(System.Environment.TickCount);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            _ecsSystemConfigure.Configure(builder);
            
            poolEnemyRedConfigure.Configure(builder);
            poolEnemyBlueConfigure.Configure(builder);
        }
    }
}