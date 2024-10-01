using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ChestMechanics.Session
{
    [Serializable]
    public class GameSessionConfigure
    {
        [SerializeField]
        private GameSessionView gameSessionView;
        
        public void Configure(IContainerBuilder builder)
        {
            builder.Register<GameSessionSave>(Lifetime.Singleton);
            builder.Register<GameSession>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            
            builder.RegisterComponent(gameSessionView);
        }
    }
}