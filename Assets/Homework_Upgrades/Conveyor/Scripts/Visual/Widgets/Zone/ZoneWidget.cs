using TMPro;
using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.Visual.Zone
{
    public class ZoneWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        public TextMeshProUGUI Text => text;
    }
}