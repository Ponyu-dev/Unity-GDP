using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IEnemyController<out T>
    {
        void Construct(Transform worldTransform, Vector3 spawnPosition, Vector3 attackPosition, GameObject target, BulletSystem bulletSystem);
        event Action<T> OnDestroyed;
    }
}