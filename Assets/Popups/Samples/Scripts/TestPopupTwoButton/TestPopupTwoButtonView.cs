using Popups;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _PresentationModel.Popups.Samples
{
    public class TestPopupTwoButtonView : PopupView
    {
        [SerializeField, Required]
        private TextMeshProUGUI textTitle;
        public void SetTextTitle(string text) => textTitle.text = text;
        
        [SerializeField, Required]
        private TextMeshProUGUI textDescription;
        public void SetTextDescription(string text) => textDescription.text = text;
        
        [SerializeField, Required]
        private Button buttonClose;

        [SerializeField, Required]
        private Button buttonApply;
        [SerializeField, Required]
        private TextMeshProUGUI textButtonApply;
        public void SetTextButtonApply(string text) => textButtonApply.text = text;

        [SerializeField, Required]
        private Button buttonCancel;
        [SerializeField, Required]
        private TextMeshProUGUI textButtonCancel;
        public void SetTextButtonCancel(string text) => textButtonCancel.text = text;
        
        protected override void SetupButtons()
        {
            if (buttonClose != null)
                buttonClose.onClick.AddListener(OnCloseClicked);

            if (buttonApply != null)
                buttonApply.onClick.AddListener(OnApplyClicked);

            if (buttonCancel != null)
                buttonCancel.onClick.AddListener(OnCancelClicked);
        }

        protected override void RemoveButtonListeners()
        {
            if (buttonClose != null)
                buttonClose.onClick.RemoveListener(OnCloseClicked);

            if (buttonApply != null)
                buttonApply.onClick.RemoveListener(OnApplyClicked);

            if (buttonCancel != null)
                buttonCancel.onClick.RemoveListener(OnCancelClicked);
        }

        private void OnCloseClicked() => _popupClickCallback?.OnCloseClicked();
        private void OnApplyClicked() => _popupClickCallback?.OnApplyClicked();
        private void OnCancelClicked() => _popupClickCallback?.OnCancelClicked();
    }
}