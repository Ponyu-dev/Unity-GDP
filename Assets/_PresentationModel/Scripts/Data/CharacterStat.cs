using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public sealed class CharacterStat
    {
        [ShowInInspector]
        public string Name { get; private set; }

        [ShowInInspector]
        public int Value { get; private set; }
        
        [Button]
        public void ChangeValue(int value)
        {
            this.Value = value;
            Debug.Log($"ChangeValue {Name} new {value}");
        }

        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
    }
}