using _ECS._RTS.Scripts.Components;
using _ECS._RTS.Scripts.Components.Range;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _ECS._RTS.Scripts.Systems.Range
{
    public abstract class BaseRangeSystem<TComponent, TRange> : IEcsRunSystem 
        where TComponent : struct
        where TRange : struct, IRange
    {
        protected readonly EcsFilterInject<Inc<Position, TComponent, TRange, Layer>> _filter;

        private Collider[] GetEnemyInRange(Vector3 position, float range, int layer)
        {
            // Получаем все коллайдеры в радиусе, которые находятся на слое врагов
            return Physics.OverlapSphere(position, range, layer);
        }

        public void Run(IEcsSystems systems)
        {
            var positionPool = _filter.Pools.Inc1;
            var rangePool = _filter.Pools.Inc3;
            var layerPool = _filter.Pools.Inc4;

            foreach (var entity in _filter.Value)
            {
                var position = positionPool.Get(entity);
                var range = rangePool.Get(entity);
                var layer = layerPool.Get(entity);

                var colliders = GetEnemyInRange(position.Value, range.Value, layer.Value);
                HandleColliders(entity, colliders);
            }
        }

        // Метод для обработки врагов в радиусе, реализуется в наследниках
        protected abstract void HandleColliders(int entity, Collider[] colliders);
    }
}