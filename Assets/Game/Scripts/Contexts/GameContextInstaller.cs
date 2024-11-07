using System.Collections.Generic;
using Atomic.Contexts;
using Game.Scripts.Common.Team;
using Game.Scripts.Contexts.GameContext.Base.GameCountdown;
using UnityEngine;

namespace Game.Scripts.Contexts.GameContext
{
    public sealed class GameContextInstaller : SceneContextInstallerBase
    {
        [SerializeField] private Transform worldTransform;
        [SerializeField] private GameCountdownInstaller countdownInstaller;

        public override void Install(IContext context)
        {
            context.AddWorldTransform(worldTransform);
            context.AddPlayerMap(new Dictionary<TeamType, IContext>());

            context.Install(countdownInstaller);
        }
    }
}