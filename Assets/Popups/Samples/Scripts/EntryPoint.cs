using _PresentationModel.Popups.Samples;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Popups.Samples.Scripts
{
    public class EntryPoint : LifetimeScope
    {
        [SerializeField] private PopupCatalog catalog;
        [SerializeField] private Transform containerPopup;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<TestPopupApplyPresenter>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterInstance(catalog);

            builder.Register<PopupFactory>(Lifetime.Singleton)
                .WithParameter("container", containerPopup)
                .AsImplementedInterfaces()
                .AsSelf();
        }

        [Button]
        private void TestShowPopup()
        {
            var popupData = new TestPopupApplyData("Пример заголовка", "Это описание попапа.", "Подтвердить");
            var show = Container.Resolve<IPopupFactory>().CanShowPopup<TestPopupApplyView, TestPopupApplyPresenter>(popupData);
            Debug.Log($"[EntryPoint] TestShowPopup {show}");
        }
    }
}