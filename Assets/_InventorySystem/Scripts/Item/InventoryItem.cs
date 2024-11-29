// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-27
// <file>: InventoryItem.cs
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using _InventorySystem.Scripts.Item.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _InventorySystem.Scripts.Item
{
    [Serializable]
    public sealed class InventoryItem
    {
        public string Id => id;
        public string InstanceId => _instanceId;
        public InventoryItemFlags Flags => flags;
        public InventoryItemMetadata Metadata => metadata;

        [SerializeField] private string id;
        [ReadOnly, ShowInInspector] private readonly string _instanceId = Guid.NewGuid().ToString();
        [SerializeField] private InventoryItemFlags flags;
        [SerializeField] private InventoryItemMetadata metadata;
        [SerializeReference] private List<IInventoryItemComponent> components;
        
        public InventoryItem()
        {
            id = string.Empty;
            flags = InventoryItemFlags.NONE;
            metadata = new InventoryItemMetadata();
            components = new List<IInventoryItemComponent>();
        }
        
        public InventoryItem(
            string id,
            string instanceId,
            InventoryItemFlags flags,
            InventoryItemMetadata metadata,
            List<IInventoryItemComponent> components)
        {
            this.id = id;
            this._instanceId = instanceId;
            this.flags = flags;
            this.metadata = metadata;
            this.components = components;
        }
        
        public bool TryGetComponent<T>(out T result)
        {
            for (int i = 0, count = components.Count; i < count; i++)
            {
                var component = components[i];
                if (component is not T tComponent)
                    continue;
                
                result = tComponent;
                return true;
            }

            result = default;
            return false;
        }

        public InventoryItem Clone()
        {
            return new InventoryItem(
                Id,
                InstanceId,
                Flags,
                Metadata,
                CloneComponents()
            );
        }

        private List<IInventoryItemComponent> CloneComponents()
        {
            var result = new List<IInventoryItemComponent>();

            for (int i = 0, count = components.Count; i < count; i++)
            {
                result.Add(components[i].Clone());
            }

            return result;
        }
    }
}