using System;
using _PresentationModel.Scripts.LevelUp;
using Lessons.Architecture.PM;
using Popups;
using Popups.Samples.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace _PresentationModel.Scripts
{
    [Serializable]
    public class EntryPoint : PopupEntryPoint
    {
        [SerializeField] private Character character1;
        [SerializeField] private Character character2;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LevelUpPresenter>(Lifetime.Transient).AsImplementedInterfaces().AsSelf();

            base.Configure(builder);
        }

        [Button]
        private void PopupShowLevelUp1()
        {
            Container.Resolve<IPopupFactory>().CanShowPopup<LevelUpView, LevelUpPresenter>(character1);
        }

        [Button]
        private void PopupShowLevelUp2()
        {
            Container.Resolve<IPopupFactory>().CanShowPopup<LevelUpView, LevelUpPresenter>(character2);
        }
    }
}