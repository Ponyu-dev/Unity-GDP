using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Homework_Upgrades.Conveyor.Scripts.Visual.Widgets.Sale
{
    public sealed class SaleWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtLumber;
        [SerializeField] private TextMeshProUGUI txtCoin;
        [SerializeField] private Button btnSale;
        
        public Button BtnSale => btnSale;

        public void SetTxtLumber(string txt)
        {
            txtLumber.text = txt;
        }
        
        public void SetTxtCoin(string txt)
        {
            txtCoin.text = txt;
        }
    }
}