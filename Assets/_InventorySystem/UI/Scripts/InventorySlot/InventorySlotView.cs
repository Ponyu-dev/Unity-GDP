// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: InventorySlotView.cs
// ------------------------------------------------------------------------------

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _InventorySystem.UI.Scripts.InventorySlot
{
    public sealed class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private Button btnSlot;
        [SerializeField] private Image imgItem;
        [SerializeField] private GameObject goStack;
        [SerializeField] private TextMeshProUGUI txtStackCount;

        public event Action OnClickSlot;

        private void Awake()
        {
            goStack.SetActive(false);
        }

        public void SetIconItem(Sprite icon)
        {
            imgItem.sprite = icon;
        }

        public void UpdateStack(string stackCount, bool isStackNotEmpty)
        {
            Debug.Log($"[Test] View UpdateStack {stackCount} {isStackNotEmpty}");
            goStack.SetActive(isStackNotEmpty);
            txtStackCount.text = stackCount;
        }

        private void OnEnable()
        {
            btnSlot.onClick.AddListener(ClickSlot);
        }

        private void ClickSlot()
        {
            OnClickSlot?.Invoke();
        }

        private void OnDisable()
        {
            btnSlot.onClick.RemoveListener(ClickSlot);
        }
    }
}