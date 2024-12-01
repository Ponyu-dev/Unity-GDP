// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-30
// <file>: EffectibleSystem.cs
// ------------------------------------------------------------------------------

using System;
using _InventorySystem.HeroStatsDebug.Scripts;
using _InventorySystem.Scripts.Item;
using _InventorySystem.Scripts.Item.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _InventorySystem.Scripts.Inventory.System
{
    public sealed class EffectibleSystem : IStartable, IDisposable
    {
        private readonly HeroStats _heroStats;
        private readonly IConsumableSystemAction _consumableSystemAction;
        private readonly IEquipmentSystemAction _equipmentSystemAction;
        
        [Inject]
        public EffectibleSystem(HeroStats heroStats, IConsumableSystemAction consumableSystemAction, IEquipmentSystemAction equipmentSystemAction)
        {
            _heroStats = heroStats;
            _consumableSystemAction = consumableSystemAction;
            _consumableSystemAction.OnConsumeAction += OnEffectActive;
            
            _equipmentSystemAction = equipmentSystemAction;
            _equipmentSystemAction.OnEquipItem += OnEffectActive;
            _equipmentSystemAction.OnUnEquipItem += OnEffectUnActive;
        }
           
        public void Start() { }

        private void OnEffectActive(InventoryItem item)
        {
            if (item is null)
                return;

            if (!item.FlagsExists(InventoryItemFlags.EFFECTIBLE))
            {
                Debug.LogWarning($"[EffectibleSystem] OnEffectActive {item.Id} has not EFFECTIBLE flag");
                return;
            }

            Debug.Log($"[EffectibleSystem] OnEffectActive {item.Id}");
            var effectableComponents = item.GetComponents<IInventoryItemComponentEffectible>();
            foreach (var component in effectableComponents)
            {
                component?.EffectActive(_heroStats);
            }
        }
        
        private void OnEffectUnActive(InventoryItem item)
        {
            if (item is null)
                return;

            if (!item.FlagsExists(InventoryItemFlags.EFFECTIBLE))
            {
                Debug.LogWarning($"[EffectibleSystem] OnEffectActive {item.Id} has not EFFECTIBLE flag");
                return;
            }

            Debug.Log($"[EffectibleSystem] OnEffectUnActive {item.Id}");
            var effectableComponents = item.GetComponents<IInventoryItemComponentEffectible>();
            foreach (var component in effectableComponents)
            {
                component?.EffectUnActive(_heroStats);
            } 
        }

        public void Dispose()
        {
            _consumableSystemAction.OnConsumeAction -= OnEffectActive;
            
            _equipmentSystemAction.OnEquipItem -= OnEffectActive;
            _equipmentSystemAction.OnUnEquipItem -= OnEffectUnActive;
        }
    }
}