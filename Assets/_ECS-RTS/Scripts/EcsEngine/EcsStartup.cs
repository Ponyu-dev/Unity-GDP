using System;
using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Systems;
using _ECS_RTS.Scripts.EcsEngine.Systems.Animators;
using _ECS_RTS.Scripts.EcsEngine.Systems.Requests;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Helpers;
using VContainer;
using VContainer.Unity;

namespace _ECS_RTS.Scripts.EcsEngine
{
    public interface IEcsStartup
    {
        public EcsEntityBuilder CreateEntity(string worldName = null);
    }
    
    public sealed class EcsStartup : IEcsStartup, IStartable, ITickable, IDisposable
    {
        private readonly EcsWorld _world;
        private readonly EcsWorld _events;
        private readonly IEcsSystems _systems;
        private readonly EntityManager _entityManager;
        
        public EcsEntityBuilder CreateEntity(string worldName = null)
        {
            return new EcsEntityBuilder(_systems.GetWorld(worldName));
        }

        [Inject]
        public EcsStartup(EntityManager entityManager)
        {
            _entityManager = entityManager;
            _world = new EcsWorld ();
            _events = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _systems.AddWorld(_events, EcsWorlds.EVENTS);
            _systems
                // Game Logic
                //.Add(new MovementSystem())
                //.Add(new FireRequestSystem())
                //.Add(new SpawnRequestSystem())
                .Add(new HealthEmptySystem())
                .Add(new DeathRequestSystem())
                //.Add(new CollisionRequestSystem())
                //.Add(new TakeDamageRequestSystem())
                //.Add(new ArrowDestroySystem())

                //View:
                .Add(new TransformViewSynchronizerSystem())
                .Add(new AnimatorIdleListenerSystem())
                .Add(new AnimatorWalkListenerSystem())
                .Add(new AnimatorRunListenerSystem())
                .Add(new AnimatorAttackListenerSystem())
                .Add(new AnimatorTakeDamageListenerSystem())
                .Add(new AnimatorDeathListenerSystem())
                //Editor:
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsWorlds.EVENTS))

#endif
                //Clean Up:
                .Add(new OneFrameEventSystem())
                .DelHere<DeathEvent>();
        }


        public void Start()
        {
            _entityManager.Initialize(_world);
            _systems.Inject(_entityManager);
            _systems.Init();
        }

        public void Tick()
        {
            _systems?.Run();
        }

        public void Dispose()
        {
            _systems?.Destroy();
            _world?.Destroy();
        }
    }
}