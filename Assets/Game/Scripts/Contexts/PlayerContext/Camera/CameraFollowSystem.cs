using System;
using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;

namespace Game.Scripts.Contexts.PlayerContext.Camera
{
    [Serializable]
    public sealed class CameraFollowSystem : IContextInit, IContextLateUpdate
    {
        private IValue<IEntity> _character;
        private IValue<float3> _characterPosition;
        private CameraData _cameraData;
        
        public void Init(IContext context)
        {
            _character = context.GetCharacter();
            _characterPosition = _character.Value.GetPosition();
            _cameraData = context.GetCameraData();
            UpdatePosition();
        }

        public void LateUpdate(IContext context, float deltaTime)
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            _cameraData.transform.position = _characterPosition.Value + _cameraData.offset;
        }
    }
}