using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Popups.Samples.Scripts
{
    public abstract class PopupEntryPoint : LifetimeScope
    {
        [SerializeField, Required] private PopupCatalog catalog;
        [SerializeField, Required] private Transform containerPopup;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(catalog);

            builder.Register<PopupFactory>(Lifetime.Singleton)
                .WithParameter("container", containerPopup)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}