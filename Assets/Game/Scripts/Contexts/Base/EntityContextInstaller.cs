using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Team;
using UnityEngine;

namespace Game.Scripts.Contexts.Base
{
    public abstract class EntityContextInstaller : SceneContextInstallerBase
    {
        [SerializeField] private SceneEntity entity;
        [SerializeField] private TeamType teamType;
        
        public override void Install(IContext context)
        {
            context.AddCharacter(new Const<IEntity>(entity));
            context.AddTeamType(teamType);
            context.GetPlayerMap().Add(teamType, context);
        }
    }
}