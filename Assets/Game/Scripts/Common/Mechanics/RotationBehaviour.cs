using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Scripts.Common.Mechanics
{
    public sealed class RotationBehaviour : IEntityInit, IEntityUpdate
    {
        private IVariable<quaternion> _rotation;
        private IValue<float> _angularSpeed;
        private IValue<float3> _position;
        private IValue<float3> _lookDirection;
        
        public void Init(IEntity entity)
        {
            _position = entity.GetPosition();
            _rotation = entity.GetRotation();
            _angularSpeed = entity.GetAngularSpeed();
            _lookDirection = entity.GetLook();
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            var direction = _lookDirection.Value - _position.Value;
            var targetRotation = Quaternion.LookRotation(direction);
            
            _rotation.Value = Quaternion.Slerp(_rotation.Value, targetRotation, deltaTime * _angularSpeed.Value);
        }
    }
}