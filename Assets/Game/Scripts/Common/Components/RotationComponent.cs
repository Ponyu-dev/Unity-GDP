using System;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Scripts.Common.Components
{
    [Serializable]
    public sealed class RotationComponent : IComponentInstaller
    {
        [SerializeField] private float angularSpeed = 5f;
        [SerializeField] private float3 initialDirection;
        
        public void Install(IEntity entity)
        {
            entity.AddLook(initialDirection);
            entity.AddAngularSpeed(angularSpeed);
            entity.AddBehaviour<RotationBehaviour>();;
        }
    }
}