using Atomic.Contexts;
using Game.Scripts.Contexts.PlayerContext.MovementSystem;
using Game.Scripts.Contexts.Base;
using Game.Scripts.Contexts.PlayerContext.Camera;
using UnityEngine;

namespace Game.Scripts.Contexts.PlayerContext
{
    public sealed class PlayerContextInstaller : EntityContextInstaller
    {
        [SerializeField] private InputMap inputMap;
        [SerializeField] private CameraData cameraData;
        
        public override void Install(IContext context)
        {
            base.Install(context);
            context.AddInputMap(inputMap);
            context.AddCameraData(cameraData);
            
            context.AddSystem<PlayerMovementSystem>();
            context.AddSystem<PlayerRotateSystem>();
            context.AddSystem<CameraFollowSystem>();
        }
    }
}