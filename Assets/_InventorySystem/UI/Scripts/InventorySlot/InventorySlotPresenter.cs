// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: InventorySlotPresenter.cs
// ------------------------------------------------------------------------------

using System;
using _InventorySystem.Scripts.Inventory.System;
using _InventorySystem.Scripts.Item;
using _InventorySystem.Scripts.Item.Components;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace _InventorySystem.UI.Scripts.InventorySlot
{
    public interface IInventorySlotPresenter : IDisposable
    {
        Guid InstanceId { get; }
        void Init(InventoryItem item, InventorySlotView slotView);
        void UpdateStack(InventoryItem item);
    }
    
    public sealed class InventorySlotPresenter : IInventorySlotPresenter
    {
        public Guid InstanceId => _item.InstanceId;

        private readonly IConsumableSystem _consumableSystem;
        private readonly IEquipmentSystem _equipmentSystem;
        private InventorySlotView _slotView;
        private InventoryItem _item;

        [Inject]
        public InventorySlotPresenter(IConsumableSystem consumableSystem, IEquipmentSystem equipmentSystem)
        {
            _consumableSystem = consumableSystem;
            _equipmentSystem = equipmentSystem;
        }
        
        public void Init(InventoryItem item, InventorySlotView slotView)
        {
            _item = item;
            _slotView = slotView;

            _slotView.OnClickSlot += OnClickSlot;
            _slotView.SetIconItem(item.Metadata.icon);
            UpdateStack(item);
        }

        public void UpdateStack(InventoryItem item)
        {
            if (!item.TryGetComponentSafe<IInventoryItemComponentStackable>(
                    InventoryItemFlags.STACKABLE,
                    out var componentStackable))
                return;
            
            _slotView.UpdateStack(componentStackable.Count.ToString(), componentStackable.IsNotEmpty());
        }

        private void OnClickSlot()
        {
            if (_item.HasComponent<IInventoryItemComponentConsumable>())
                _consumableSystem.ConsumeItem(_item);
            
            if (_item.HasComponent<IInventoryItemComponentEquippable>())
                _equipmentSystem.EquipItem(_item);
        }

        public void Dispose()
        {
            _slotView.OnClickSlot -= OnClickSlot;
            Object.Destroy(_slotView.gameObject);
        }
    }
}