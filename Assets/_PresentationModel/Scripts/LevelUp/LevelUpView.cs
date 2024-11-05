using _PresentationModel.Scripts.LevelUp.Views;
using Popups;
using UnityEngine;
using UnityEngine.UI;

namespace _PresentationModel.Scripts.LevelUp
{
    public sealed class LevelUpView : PopupView
    {
        [SerializeField] private Button btnClose;
        [SerializeField] private Button btnApply;
        [SerializeField] private UserInfoView userInfoView;
        [SerializeField] private PlayerLevelView playerLevelView;

        protected override void SetupButtons()
        {
            if (btnClose != null)
                btnClose.onClick.AddListener(OnCloseClicked);
            
            if (btnApply != null)
                btnApply.onClick.AddListener(OnApplyClicked);
        }

        protected override void RemoveButtonListeners()
        {
            if (btnClose != null)
                btnClose.onClick.RemoveListener(OnCloseClicked);
            
            if (btnApply != null)
                btnApply.onClick.RemoveListener(OnApplyClicked);
        }

        private void OnCloseClicked() => _popupClickCallback?.OnCloseClicked();
        private void OnApplyClicked() => _popupClickCallback?.OnApplyClicked();

        public void SetUserInfoData(string name, string description, Sprite icon)
        {
            userInfoView.SetName(name);        
            userInfoView.SetDescription(description);        
            userInfoView.SetIcon(icon);        
        }

        public void SetPlayerLevelData(string level, float progress, string text)
        {
            playerLevelView.SetCurrentLevel(level);
            playerLevelView.SetLevelProgress(progress, text);

            btnApply.interactable = progress >= 1;
        }
    }
}