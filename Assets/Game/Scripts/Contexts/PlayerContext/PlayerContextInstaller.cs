using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Team;
using Game.Scripts.Contexts.PlayerContext.MovementSystem;
using Game.Scripts.Contexts.Base;
using Game.Scripts.Contexts.PlayerContext.Camera;
using UnityEngine;

namespace Game.Scripts.Contexts.PlayerContext
{
    public sealed class PlayerContextInstaller : SceneContextInstallerBase
    {
        [SerializeField] private SceneEntity entity;
        [SerializeField] private TeamType teamType;
        [SerializeField] private InputMap inputMap;
        [SerializeField] private CameraData cameraData;
        
        public override void Install(IContext context)
        {
            context.AddCharacter(new Const<IEntity>(entity));
            context.AddTeamType(teamType);
            context.GetPlayerMap().Add(teamType, context);
            
            context.AddInputMap(inputMap);
            context.AddCameraData(cameraData);
            
            context.AddSystem<PlayerMovementSystem>();
            context.AddSystem<PlayerRotateSystem>();
            context.AddSystem<CameraFollowSystem>();
        }
    }
}