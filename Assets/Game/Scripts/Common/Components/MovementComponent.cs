using System;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Scripts.Common.Components
{
    [Serializable]
    public sealed class MovementComponent : IComponentInstaller
    {
        [SerializeField] private float moveSpeed = 3;
        [SerializeField] private float3 initialDirection;
        
        public void Install(IEntity entity)
        {
            entity.AddMoveSpeed(moveSpeed);
            entity.AddMoveDirection(initialDirection);
            entity.AddBehaviour<MovementBehaviour>();
        }
    }
}