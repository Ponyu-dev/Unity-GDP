using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Scripts.Common.Mechanics
{
    public sealed class TransformBehaviour : IEntityInit, IEntityUpdate
    {
        private Transform _transform;
        private IReactiveValue<float3> _position;
        private IReactiveValue<quaternion> _rotation;
        
        public void Init(IEntity entity)
        {
            _transform = entity.GetRootTransform();
            _position = entity.GetPosition();
            _rotation = entity.GetRotation();
            
            _transform.SetPositionAndRotation(_position.Value, _rotation.Value);
        }
        
        public void OnUpdate(IEntity entity, float deltaTime)
        {
            _transform.SetPositionAndRotation(_position.Value, _rotation.Value);
        }
    }
}