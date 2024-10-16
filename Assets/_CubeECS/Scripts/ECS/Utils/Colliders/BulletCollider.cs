using System;
using _CubeECS.Scripts.ECS.Events;
using _CubeECS.Scripts.ECS.Utils;
using CubeECS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace _CubeECS.Scripts.ECS.Bullets
{
    public class BulletCollider : AbstractCollider
    {
        private EcsWorld _ecsWorld;

        public void SetEcsWorld(EcsWorld ecsWorld)
        {
            Debug.Log("[BulletCollider] SetEcsWorld");
            _ecsWorld = ecsWorld;
        }

        private void OnTriggerEnter(Collider collision)
        {
            var layerMask = LayerMask.LayerToName(collision.gameObject.layer);
            Debug.Log($"[BulletCollider] OnTriggerEnter {layerMask}");
            
            // Получаем доступ к системе через GameManager или другим способом
            var hitEventPool = _ecsWorld.GetPool<BulletHitEvent>();

            // Добавляем событие столкновения в ECS
            var eventEntity = _ecsWorld.NewEntity();
            ref var hitEvent = ref hitEventPool.Add(eventEntity);
            hitEvent.BulletEntity = EntityId;
            hitEvent.BulletGO = gameObject;
            
            // Получаем ECS-сущность юнита, в которого попала пуля
            if (collision.gameObject.TryGetComponent<EnemyCollider>(out var enemyCollider))
            {
                hitEvent.TargetEntity = enemyCollider.EntityId;
                hitEvent.TargetGO = enemyCollider.gameObject;
            }

            if (collision.gameObject.TryGetComponent<BulletCollider>(out var bulletCollider))
            {
                hitEvent.TargetEntity = bulletCollider.EntityId;
                hitEvent.TargetGO = bulletCollider.gameObject;
            }
        }
    }
}