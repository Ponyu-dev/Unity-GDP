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
        private IValue<bool> _canRotate;
        
        public void Init(IEntity entity)
        {
            _position = entity.GetPosition();
            _rotation = entity.GetRotation();
            _angularSpeed = entity.GetAngularSpeed();
            _lookDirection = entity.GetLook();
            _canRotate = entity.GetCanRotate();
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            if (!_canRotate.Value)
                return;
            
            var direction = math.normalize(_lookDirection.Value - _position.Value);
            var targetRotation = Quaternion.LookRotation(direction);
            
            _rotation.Value = Quaternion.Slerp(_rotation.Value, targetRotation, deltaTime * _angularSpeed.Value);
        }
    }
}