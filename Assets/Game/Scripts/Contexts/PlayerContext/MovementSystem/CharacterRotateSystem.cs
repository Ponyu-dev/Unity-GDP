using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Scripts.Contexts.PlayerContext.MovementSystem
{
    public sealed class CharacterRotateSystem : IContextInit, IContextUpdate
    {
        private IValue<IEntity> _character;
        private IValue<float3> _position;
        private UnityEngine.Camera _cameraMain;
        
        public void Init(IContext context)
        {
            _character = context.GetCharacter();
            _position = _character.Value.GetPosition();
            _cameraMain = _character.Value.GetCameraMain();
        }

        public void Update(IContext context, float deltaTime)
        {
            var ray = _cameraMain.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, _position.Value);
            if (!plane.Raycast(ray, out var distance)) return;

            _character.Value.GetLook().Value = ray.GetPoint(distance);
        }
    }
}