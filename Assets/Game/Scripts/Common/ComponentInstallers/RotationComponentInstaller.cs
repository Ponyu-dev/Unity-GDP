using System;
using Atomic.Elements;
using Atomic.Entities;
using Atomic.Extensions;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Common.ComponentInstallers
{
    [Serializable]
    public sealed class RotationComponentInstaller : IComponentInstaller, IAddExpression
    {
        [SerializeField] private Const<float> angularSpeed = new(5f);
        [SerializeField] private float3Reactive initialDirection;
        [SerializeField] private AndExpression canRotate;
        
        public void Install(IEntity entity)
        {
            entity.AddLook(initialDirection);
            entity.AddAngularSpeed(angularSpeed);
            entity.AddCanRotate(canRotate);
            entity.AddBehaviour<RotationBehaviour>();
        }
        
        public void Append(Func<bool> func)
        {
            canRotate.Append(func);   
        }
    }
}