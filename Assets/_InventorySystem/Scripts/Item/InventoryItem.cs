// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-27
// <file>: InventoryItem.cs
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using _InventorySystem.Scripts.Item.Components;
using Sirenix.Serialization;
using UnityEngine;

namespace _InventorySystem.Scripts.Item
{
    [Serializable]
    public sealed class InventoryItem
    {
        public string Id => id;
        public InventoryItemFlags Flags => flags;
        public InventoryItemMetadata Metadata => metadata;

        [SerializeField] private string id;
        [SerializeField] private InventoryItemFlags flags;
        [SerializeField] private InventoryItemMetadata metadata;
        [SerializeReference] private List<IInventoryItemComponent> components;
        
        public InventoryItem()
        {
            id = string.Empty;
            components = new List<IInventoryItemComponent>();
        }
        
        public InventoryItem(
            string id,
            InventoryItemFlags flags,
            InventoryItemMetadata metadata,
            List<IInventoryItemComponent> components
        )
        {
            this.id = id;
            this.flags = flags;
            this.metadata = metadata;
            this.components = components;
        }

        public T GetComponent<T>()
        {
            for (int i = 0, count = components.Count; i < count; i++)
            {
                var component = components[i];
                if (component is T result)
                {
                    return result;
                }
            }

            throw new Exception($"Component {typeof(T).Name} is not found!");
        }

        public T[] GetComponents<T>()
        {
            var result = new List<T>();
            for (int i = 0, count = components.Count; i < count; i++)
            {
                var component = components[i];
                if (component is T tComponent)
                {
                    result.Add(tComponent);
                }
            }

            return result.ToArray();
        }

        public IReadOnlyList<IInventoryItemComponent> GetAllComponents()
        {
            return components;
        }

        public bool HasComponent<T>()
        {
            for (int i = 0, count = components.Count; i < count; i++)
            {
                if (components[i] is not T)
                    continue;
                return true;
            }
            
            return false;
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
                id,
                flags,
                metadata,
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