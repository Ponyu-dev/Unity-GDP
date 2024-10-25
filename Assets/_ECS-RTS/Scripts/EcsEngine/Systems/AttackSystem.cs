using _ECS_RTS.Scripts.EcsEngine.Components;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _ECS_RTS.Scripts.EcsEngine.Systems
{
    internal sealed class AttackSystem : TimedSystem
    {
        private readonly EcsFilterInject<Inc<AttackTag, AttackTargetEntity>, Exc<Inactive>> _filterAttack;
        private readonly EcsFilterInject<Inc<Position, EntityTag>, Exc<Inactive>> _filterEnemy;
        
        private readonly EcsPoolInject<AttackEvent> _eventPool;
        
        public AttackSystem() : base(1f) { }

        protected override void Execute(IEcsSystems systems)
        {
            //var attackTargetEntityPool = _filterAttack.Pools.Inc2;
            foreach (var entity in _filterAttack.Value)
            {
                //var attackEnemyId = attackTargetEntityPool.Get(entity).Value;
                //Debug.Log($"[AttackSystem] Run {entity} attack {attackEnemyId}");
                _eventPool.Value.Add(entity) = new AttackEvent();
            }
        }
    }
}