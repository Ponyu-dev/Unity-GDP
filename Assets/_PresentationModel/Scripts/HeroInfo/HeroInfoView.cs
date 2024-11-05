using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _PresentationModel.Scripts.HeroInfo
{
    public sealed class HeroInfoView : MonoBehaviour
    {
        public event Action OnShowClicked;
        
        [SerializeField] private TextMeshProUGUI txtName;
        [SerializeField] private Image imgIcon;
        [SerializeField] private Button btnShowPopup;

        public void SetName(string heroName) => txtName.text = heroName;
        public void SetIcon(Sprite heroIcon) => imgIcon.sprite = heroIcon;

        private void OnEnable()
        {
            btnShowPopup.onClick.AddListener(OnShowClick);
        }

        private void OnShowClick()
        {
            OnShowClicked?.Invoke();
        }

        private void OnDisable()
        {
            btnShowPopup.onClick.RemoveListener(OnShowClick);
        }
    }
}