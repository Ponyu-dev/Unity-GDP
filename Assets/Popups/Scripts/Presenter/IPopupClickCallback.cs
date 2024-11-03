namespace Popups
{
    public interface IPopupClickCallback
    {
        public void OnCloseClicked();
        public void OnCancelClicked();
        public void OnApplyClicked();
    }
}