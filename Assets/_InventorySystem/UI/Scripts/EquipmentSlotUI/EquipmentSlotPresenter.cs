// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: EquipmentSlotPresenter.cs
// ------------------------------------------------------------------------------

using System;
using _InventorySystem.Scripts.Inventory;
using _InventorySystem.Scripts.Inventory.System;
using _InventorySystem.Scripts.Item;
using _InventorySystem.Scripts.Item.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _InventorySystem.UI.Scripts
{
    public sealed class EquipmentSlotPresenter : IStartable, IDisposable
    {
        private readonly IEquipmentSystem _equipmentSystem;
        private readonly IEquipInventoryAction _equipInventoryAction;
        private readonly EquipmentSlotView _slotView;
        private readonly EquipmentSlot _equipmentSlot;

        [Inject]
        public EquipmentSlotPresenter(IEquipmentSystem equipmentSystem, IEquipInventoryAction equipInventoryAction, EquipmentSlotView slotView)
        {
            _equipmentSystem = equipmentSystem;
            
            _equipInventoryAction = equipInventoryAction;
            _equipInventoryAction.OnEquipChanged += Equip;
            _equipInventoryAction.OnUnEquipChanged += UnEquip;

            _equipmentSlot = slotView.Slot;
            _slotView = slotView;
            _slotView.OnClickUnEquip += OnUnEquip;
        }

        private void Equip(InventoryItem inventoryItem)
        {
            if (!inventoryItem.TryGetComponentSafe<IInventoryItemComponentEquippable>(
                    InventoryItemFlags.EQUIPPABLE,
                    out var componentEquippable))
            {
                Debug.LogWarning($"[EquipmentSlotPresenter] Equip Item {inventoryItem.Id} has not IInventoryItemComponentEquippable");
                return;
            }

            if (_equipmentSlot != componentEquippable.EquipmentSlot)
            {
                Debug.LogWarning($"[EquipmentSlotPresenter] Equip Item {inventoryItem.Id} {componentEquippable.EquipmentSlot} is not _slotView = {_equipmentSlot}");
                return;
            }
            
            _slotView.SetIcon(inventoryItem.Metadata.icon, true);
        }
        
        private void UnEquip(EquipmentSlot equipmentSlot)
        {
            if (equipmentSlot != _equipmentSlot)
            {
                Debug.LogWarning($"[EquipmentSlotPresenter] UnEquip({equipmentSlot}) is not _slotView = {_equipmentSlot}");
                return;
            }

            _slotView.SetIcon(default, false);
        }

        private void OnUnEquip()
        {
            _equipmentSystem.UnEquipItem(_slotView.Slot);
        }

        public void Dispose()
        {
            _equipInventoryAction.OnEquipChanged -= Equip;
            _equipInventoryAction.OnUnEquipChanged -= UnEquip;
            _slotView.OnClickUnEquip -= OnUnEquip;
        }

        public void Start()
        {
            
        }
    }
}