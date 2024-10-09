using System;
using _EventBus.Scripts.Players.Utils;
using UnityEngine;

namespace _EventBus.Scripts.Players.Components
{
    public sealed class HitPointsComponent
    {
        /*public int Value
        {
            get => _hitPoints;
            set => _hitPoints = Mathf.Clamp(value, 0, _maxHitPoints);
        }

        private int _hitPoints;
        private readonly int _maxHitPoints;

        public HitPointsComponent(int hitPoints)
        {
            _hitPoints = hitPoints;
            _maxHitPoints = hitPoints;
        }

        public bool IsDied() => Value <= 0;*/
        
        public event Action<int> OnValueChanged
        {
            add => _hitPoints.ValueChanged += value;
            remove => _hitPoints.ValueChanged -= value;
        }

        public int Value
        {
            get => _hitPoints;
            set => _hitPoints.Value = Mathf.Clamp(value, 0, _maxHitPoints);
        }

        public int MaxHitPoints => _maxHitPoints;

        private readonly AtomicVariable<int> _hitPoints = new();
        private readonly AtomicVariable<int> _maxHitPoints = new();

        public HitPointsComponent(int maxHitPoints)
        {
            _hitPoints.Value = maxHitPoints;
            _maxHitPoints.Value = maxHitPoints;
        }
    }
}