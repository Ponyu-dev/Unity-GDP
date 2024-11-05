using _PresentationModel.Scripts.LevelUp.Views;
using Popups;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace _PresentationModel.Scripts.LevelUp
{
    public sealed class LevelUpView : PopupView
    {
        [SerializeField, Required] private Button btnClose;
        [SerializeField, Required] private Button btnApply;
        [SerializeField, Required] public UserInfoView userInfoView;
        [SerializeField, Required] public PlayerLevelView playerLevelView;
        [SerializeField, Required] public Transform statsContainer;

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

        public void SetEnableBtnApply(bool value) => btnApply.interactable = value;
    }
}