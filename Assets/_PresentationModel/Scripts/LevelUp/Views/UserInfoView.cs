using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _PresentationModel.Scripts.LevelUp.Views
{
    public sealed class UserInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtName;
        [SerializeField] private TextMeshProUGUI txtDescription;
        [SerializeField] private Image imgIcon;

        public void SetName(string txtName) => this.txtName.text = txtName;
        public void SetDescription(string description) => txtDescription.text = description;
        public void SetIcon(Sprite icon) => imgIcon.sprite = icon;
    }
}