using Popups;
using VContainer;

namespace _PresentationModel.Popups.Samples
{
    public class TestPopupPresenter : PopupPresenter
    {
        [Inject]
        public TestPopupPresenter(
            IPopup popup,
            IPopupAnimator popupAnimator) : base(popup, popupAnimator)
        {
            
        }
    }
}