using System;
using _ShootEmUp.UI.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ShootEmUp.Scripts.VContainer
{
    [Serializable]
    public class UILifetimeScope
    {
        public void Configure(IContainerBuilder builder)
        {
            Debug.Log("[UILifetimeScope] Configure");
            builder.RegisterComponentInHierarchy<Screen>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<StartScreen>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<PlayingScreen>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<PauseScreen>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<FinishScreen>().AsImplementedInterfaces();
        }
    }
}
