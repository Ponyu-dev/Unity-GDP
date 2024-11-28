// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-27
// <file>: InventoryItemMetadata.cs
// ------------------------------------------------------------------------------

using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _InventorySystem.Scripts.Item
{
    [Serializable]
    public sealed class InventoryItemMetadata
    {
        [SerializeField] public string title;
        [SerializeField, TextArea] public string decription;
        [SerializeField, PreviewField] public Sprite icon;
    }
}