namespace Popups
{
    public interface IPopupAnimatorCallback
    {
        public void OnShowStarted();
        public void OnShowFinished();
        public void OnHideStarted();
        public void OnHideFinished();
    }
}