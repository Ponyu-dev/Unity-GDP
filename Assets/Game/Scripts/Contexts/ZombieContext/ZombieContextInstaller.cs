using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Team;
using Game.Scripts.Contexts.ZombieContext.MovementSystem;
using UnityEngine;

namespace Game.Scripts.Contexts.ZombieContext
{
    public sealed class ZombieContextInstaller : SceneContextInstallerBase
    {
        [SerializeField] private SceneEntity zombie;
        [SerializeField] private SceneEntity player;
        [SerializeField] private TeamType teamType;
        
        public override void Install(IContext context)
        {
            context.GetPlayerMap().Add(teamType, context);
            context.AddTeamType(teamType);
            
            context.AddCharacter(new Const<IEntity>(zombie));
            context.AddAttackPlayer(new Const<IEntity>(player));

            context.AddSystem<ZombieMovementSystem>();        
        }
    }
}