using Popups;
using TMPro;
using UnityEngine;

namespace _PresentationModel.Popups.Samples
{
    public class TestPopup : Popup
    {
        [SerializeField] private TextMeshProUGUI textTitle;
        
        public override void SetData(PopupData data)
        {
            if (data == null) return;
            
            var dataPres = data as TestPopupData;
            if (dataPres == null) return;
            
            textTitle.text = dataPres.Title;
        }
    }
}