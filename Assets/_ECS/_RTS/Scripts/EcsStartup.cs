using System;
using _ECS._RTS.Scripts.Systems;
using _ECS._RTS.Scripts.Systems.Range;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ECS._RTS.Scripts
{
    public class EcsStartup : IInitializable, ITickable, IDisposable
    {
        private readonly EcsWorld _world;
        private readonly EcsWorld _events;
        private readonly IEcsSystems _systems;
        private readonly EntityManager _entityManager;

        [Inject]
        public EcsStartup()
        {
            Debug.Log("[EcsStartup] Constructor");
            _entityManager = new EntityManager();
            _world = new EcsWorld();
            _events = new EcsWorld();
            _systems = new EcsSystems (_world);
        }
        
        public void Initialize()
        {
            Debug.Log("[EcsStartup] Initialize()");
            _systems.AddWorld(_events, EcsWorlds.EVENTS);
            _systems
                .Add(new DetectorRangeSystem())
                .Add(new AttackRangeSystem())
                .Add(new MovementSystem())
                .Add(new TransformViewSystem());
            
            
            /*.Add(new ExampleSystem())
            .Add(new FireRequestSystem())
            .Add(new SpawnRequestSystem())*/
            
#if UNITY_EDITOR
            _systems
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsWorlds.EVENTS));
#endif
            
            
            _entityManager?.Initialize(_world);
            _systems?.Inject(_entityManager);
            _systems?.Init();
        }

        public void Tick()
        {
            _systems?.Run();
        }

        public void Dispose()
        {
            _systems?.Destroy ();
            _world?.Destroy ();
        }
    }
}