using _PresentationModel.Popups.Samples;
using Sirenix.OdinInspector;
using VContainer;

namespace Popups.Samples.Scripts
{
    public class ExamplePopupEntryPoint : PopupEntryPoint
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<TestPopupApplyPresenter>(Lifetime.Transient).AsImplementedInterfaces().AsSelf();
            builder.Register<TestPopupTwoButtonPresenter>(Lifetime.Transient).AsImplementedInterfaces().AsSelf();
            
            base.Configure(builder);
        }

        [Button]
        private void ApplyShowPopup()
        {
            var popupData = new TestPopupApplyData("Apply Title", "Apply Description.", "Apply");
            var show = Container.Resolve<IPopupFactory>().CanShowPopup<TestPopupApplyView, TestPopupApplyPresenter>(popupData);
        }
        
        [Button]
        private void TwoButtonShowPopup()
        {
            var popupData = new TestPopupTwoButtonData("TwoButton Title", "TwoButton Description", "Apply", "Cancel");
            var show = Container.Resolve<IPopupFactory>().CanShowPopup<TestPopupTwoButtonView, TestPopupTwoButtonPresenter>(popupData);
        }
    }
}