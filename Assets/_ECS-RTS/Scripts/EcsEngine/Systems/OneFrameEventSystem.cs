using _ECS_RTS.Scripts.EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _ECS_RTS.Scripts.EcsEngine.Systems
{
    internal sealed class OneFrameEventSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OneFrame>> _filter = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;

        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _filter.Value)
            {
                _eventWorld.Value.DelEntity(@event);
            }
        }
    }
}