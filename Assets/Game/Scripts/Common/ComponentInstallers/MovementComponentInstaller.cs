using System;
using Atomic.Elements;
using Atomic.Entities;
using Atomic.Extensions;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Common.ComponentInstallers
{
    [Serializable]
    public sealed class MovementComponentInstaller : IComponentInstaller, IAddExpression
    {
        [SerializeField] private Const<float> moveSpeed = new(3);
        [SerializeField] private float3Reactive initialDirection;
        [SerializeField] private AndExpression canMove;
        
        public void Install(IEntity entity)
        {
            entity.AddMoveSpeed(moveSpeed);
            entity.AddMoveDirection(initialDirection);
            entity.AddCanMove(canMove);
            entity.AddBehaviour<MovementBehaviour>();
        }

        public void Append(Func<bool> func)
        {
            canMove.Append(func);
        }
    }
}