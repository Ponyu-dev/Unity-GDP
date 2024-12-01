// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: EntryPoints.cs
// ------------------------------------------------------------------------------

using _InventorySystem.HeroStatsDebug.Scripts;
using _InventorySystem.Scripts.Inventory;
using _InventorySystem.Scripts.Inventory.System;
using _InventorySystem.Scripts.Item;
using _InventorySystem.UI.Scripts.InventorySlot;
using Sirenix.OdinInspector;
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
        [SerializeField] private HeroStats heroStats;

        [BoxGroup("EquipmentSlotView"), SerializeField] private EquipmentSlotView slotViewHead;
        [BoxGroup("EquipmentSlotView"), SerializeField] private EquipmentSlotView slotViewBody;
        [BoxGroup("EquipmentSlotView"), SerializeField] private EquipmentSlotView slotViewLegs;
        [BoxGroup("EquipmentSlotView"), SerializeField] private EquipmentSlotView slotViewLeftHand;
        [BoxGroup("EquipmentSlotView"), SerializeField] private EquipmentSlotView slotViewRightHand;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(inventoryItemCatalog);

            builder.RegisterInstance<HeroStats>(heroStats)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Register<InventorySlotPresenter>(Lifetime.Transient)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<BaseInventory>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder.Register<EquipInventory>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            
            builder.Register<EquipmentSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            builder.Register<ConsumableSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            builder.Register<EffectibleSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Register<GridInventoryPresenter>(Lifetime.Singleton)
                .WithParameter(inventorySlotView)
                .WithParameter(gridInventoryTransform)
                .AsImplementedInterfaces();

            builder.Register<EquipmentSlotPresenter>(Lifetime.Scoped)
                .WithParameter(slotViewHead)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<EquipmentSlotPresenter>(Lifetime.Scoped)
                .WithParameter(slotViewBody)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<EquipmentSlotPresenter>(Lifetime.Scoped)
                .WithParameter(slotViewLegs)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<EquipmentSlotPresenter>(Lifetime.Scoped)
                .WithParameter(slotViewLeftHand)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<EquipmentSlotPresenter>(Lifetime.Scoped)
                .WithParameter(slotViewRightHand)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}