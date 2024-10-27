using _ECS_RTS.Scripts.EcsEngine.DI;
using _ECS_RTS.Scripts.EcsEngine.Views;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ECS_RTS.Scripts.DI
{
    public class RtsEntryPoint : LifetimeScope
    {
        [BoxGroup("SFX Pool"), SerializeField] private SfxConfigure sfxConfigure;
        [BoxGroup("Arrow Pool"), SerializeField] private ArrowPoolConfigure arrowPoolConfigure;
        [BoxGroup("Team Red Pool"), SerializeField] private PoolEnemyConfigure poolEnemyRedConfigure;
        [BoxGroup("Team Blue Pool"), SerializeField] private PoolEnemyConfigure poolEnemyBlueConfigure;

        private readonly EcsSystemConfigure _ecsSystemConfigure = new();
        
        protected override void Awake()
        {
            base.Awake();
            Random.InitState(System.Environment.TickCount);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<CollisionComponentPresenter>(Lifetime.Transient)
                .AsImplementedInterfaces();
            
            sfxConfigure.Configure(builder);
            arrowPoolConfigure.Configure(builder);
            
            _ecsSystemConfigure.Configure(builder);

            poolEnemyRedConfigure.Configure(builder);
            poolEnemyBlueConfigure.Configure(builder);
        }
    }
}