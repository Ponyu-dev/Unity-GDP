// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: EntryPoints.cs
// ------------------------------------------------------------------------------

using _InventorySystem.Scripts.Inventory;
using _InventorySystem.Scripts.Inventory.System;
using _InventorySystem.Scripts.Item;
using _InventorySystem.UI.Scripts.InventorySlot;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _InventorySystem.UI.Scripts
{
    public sealed class EntryPoints : LifetimeScope
    {
        [SerializeField] private InventoryItemCatalog inventoryItemCatalog;
        [SerializeField] private InventorySlotView inventorySlotView;
        [SerializeField] private Transform gridInventoryTransform;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(inventoryItemCatalog);

            builder.Register<InventorySlotPresenter>(Lifetime.Transient)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<BaseInventory>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder.Register<EquipInventory>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            
            builder.Register<EquipmentSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder.Register<ConsumableSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces();

            builder.Register<GridInventoryPresenter>(Lifetime.Singleton)
                .WithParameter(inventorySlotView)
                .WithParameter(gridInventoryTransform)
                .AsImplementedInterfaces();
        }
    }
}