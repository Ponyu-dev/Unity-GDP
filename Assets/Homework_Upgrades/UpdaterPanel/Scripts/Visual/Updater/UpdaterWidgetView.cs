using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Homework_Upgrades.UpdaterPanel.Scripts.Visual
{
    public sealed class UpdaterWidgetView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtMaxUpdater;
        [SerializeField] private TextMeshProUGUI txtLevel;
        [SerializeField] private TextMeshProUGUI txtPrice;
        [SerializeField] private Button btnUpdater;
        
        public Button BtnUpdater => btnUpdater;

        public void SetMaxUpdater(string txt)
        {
            txtMaxUpdater.text = txt;
        }
        
        public void SetLevel(string txt)
        {
            txtLevel.text = txt;
        }
        
        public void SetPrice(string txt)
        {
            txtPrice.text = txt;
        }
    }
}