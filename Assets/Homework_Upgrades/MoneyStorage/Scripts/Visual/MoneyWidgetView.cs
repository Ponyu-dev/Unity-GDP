using TMPro;
using UnityEngine;

namespace Game.GamePlay.Upgrades
{
    public sealed class MoneyWidgetView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtMoney;

        public void SetMoney(string countMoney)
        {
            txtMoney.text = countMoney;
        }
    }
}