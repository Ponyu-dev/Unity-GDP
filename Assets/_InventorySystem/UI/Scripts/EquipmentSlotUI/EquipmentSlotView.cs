// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: EquipmentSlotView.cs
// ------------------------------------------------------------------------------

using _InventorySystem.Scripts.Item.Components;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _InventorySystem.UI.Scripts
{
    public sealed class EquipmentSlotView : MonoBehaviour
    {
        [SerializeField] private EquipmentSlot slot;
        [SerializeField] private Image iconItemSlot;
        [SerializeField] private TextMeshProUGUI txtSlot;
        [SerializeField] private Button btnUnEquip;

        public EquipmentSlot Slot => slot;
        
        public event UnityAction OnClickUnEquip
        {
            add => btnUnEquip.onClick.AddListener(value);
            remove => btnUnEquip.onClick.RemoveListener(value);
        }
        
        public void SetIcon(Sprite icon, bool active)
        {
            iconItemSlot.sprite = icon;
            iconItemSlot.gameObject.SetActive(active);
            txtSlot.gameObject.SetActive(!active);
            SetButtonInteractable(active);
        }

        private void SetButtonInteractable(bool value)
        {
            btnUnEquip.interactable = value;
        }
    }
}