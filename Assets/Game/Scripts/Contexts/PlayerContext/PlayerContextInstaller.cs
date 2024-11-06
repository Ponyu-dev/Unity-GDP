using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Contexts.PlayerContext.InputSystem;
using Game.Scripts.Contexts.PlayerContext.MovementSystem;
using Game.Scripts.Common.Team;
using UnityEngine;

namespace Game.Scripts.Contexts.PlayerContext
{
    public sealed class PlayerContextInstaller : SceneContextInstallerBase
    {
        [SerializeField] private SceneEntity character;
        [SerializeField] private InputMap inputMap;
        [SerializeField] private TeamType teamType;
        
        public override void Install(IContext context)
        {
            context.GetPlayerMap().Add(teamType, context);
            context.AddTeamType(teamType);

            context.AddCharacter(new Const<IEntity>(character));
            context.AddInputMap(inputMap);
            //context.AddCameraData(cameraData);
            
            context.AddSystem<CharacterMovementSystem>();
            //context.AddSystem<CameraFollowSystem>();
        }
    }
}