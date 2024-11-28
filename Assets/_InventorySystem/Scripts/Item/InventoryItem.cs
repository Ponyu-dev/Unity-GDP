// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-27
// <file>: InventoryItem.cs
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
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
        [SerializeField, Space] private object[] components;
        
        public InventoryItem()
        {
            id = string.Empty;
            components = Array.Empty<object>();
        }

        public InventoryItem(string id, InventoryItemFlags flags, InventoryItemMetadata metadata, params object[] components)
        {
            this.id = id;
            this.flags = flags;
            this.metadata = metadata;
            this.components = components;
        }

        public T GetComponent<T>()
        {
            for (int i = 0, count = components.Length; i < count; i++)
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
            for (int i = 0, count = components.Length; i < count; i++)
            {
                var component = components[i];
                if (component is T tComponent)
                {
                    result.Add(tComponent);
                }
            }

            return result.ToArray();
        }

        public object[] GetAllComponents()
        {
            return components;
        }

        public bool TryGetComponent<T>(out T result)
        {
            for (int i = 0, count = components.Length; i < count; i++)
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

        private object[] CloneComponents()
        {
            var count = components.Length;
            var result = new object[count];

            for (var i = 0; i < count; i++)
            {
                var component = components[i];
                if (component is ICloneable cloneable)
                {
                    component = cloneable.Clone();
                }

                result[i] = component;
            }

            return result;
        }
    }
}