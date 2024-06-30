using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IEnemyController<out T>
    {
        void Construct(Transform worldTransform, Vector3 spawnPosition, Vector3 attackPosition, Vector3 targetPosition, HitPointsComponent targetHitPointsComponent, BulletSystem bulletSystem);
        event Action<T> OnDestroyed;
    }
}