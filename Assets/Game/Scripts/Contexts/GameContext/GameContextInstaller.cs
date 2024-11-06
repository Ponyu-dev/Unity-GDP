using System.Collections.Generic;
using Atomic.Contexts;
using Game.Scripts.Common.Team;
using UnityEngine;

namespace Game.Scripts.Contexts.GameContext
{
    public sealed class GameContextInstaller : SceneContextInstallerBase
    {
        [SerializeField] private Transform worldTransform;

        //[SerializeField] private GameCountdownInstaller gameCountdownInstaller;
        //[SerializeField] private CoinSystemInstaller coinSystemInstaller;

        public override void Install(IContext context)
        {
            context.AddWorldTransform(worldTransform);
            context.AddPlayerMap(new Dictionary<TeamType, IContext>());
            //context.AddSystem<GameOverController>();

            //context.Install(gameCountdownInstaller);
            //context.Install(coinSystemInstaller);
        }
    }
}