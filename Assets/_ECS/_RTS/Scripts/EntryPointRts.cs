using _ECS._RTS.Scripts;
using _ECS._RTS.Scripts.Installers;
using _ECS._RTS.Scripts.Installers.Presenters;
using _ECS._RTS.Scripts.Services;
using _ECS._RTS.Scripts.UI.Presenters;
using _ECS._RTS.Scripts.UI.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EntryPointRts : LifetimeScope
{
    [SerializeField] private BaseInstaller baseRed;
    [SerializeField] private BaseInstaller baseBlue;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<HealthService>(Lifetime.Singleton)
            .AsImplementedInterfaces()
            .AsSelf();
        
        builder.Register<BasePresenterInstaller>(Lifetime.Transient)
            .WithParameter(baseRed)
            .AsImplementedInterfaces()
            .AsSelf();
        
        builder.Register<BasePresenterInstaller>(Lifetime.Transient)
            .WithParameter(baseBlue)
            .AsImplementedInterfaces()
            .AsSelf();
        
        builder.Register<HealthPresenter>(Lifetime.Transient)
            .AsImplementedInterfaces()
            .AsSelf();
        
        builder.Register<HealthView>(Lifetime.Scoped)
            .AsImplementedInterfaces()
            .AsSelf();

        builder.Register<EcsStartup>(Lifetime.Singleton)
            .AsImplementedInterfaces().AsSelf();
    }
}
