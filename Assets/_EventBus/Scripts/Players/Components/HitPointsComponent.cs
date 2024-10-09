using System;
using _EventBus.Scripts.Players.Utils;
using UnityEngine;

namespace _EventBus.Scripts.Players.Components
{
    public sealed class HitPointsComponent
    {
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
        
        public bool IsHitPointsLow()
        {
            // Проверка, если HitPoints меньше 20% от MaxHitPoints
            return _hitPoints < (0.2 * _maxHitPoints);
        }

        private readonly AtomicVariable<int> _hitPoints = new();
        private readonly AtomicVariable<int> _maxHitPoints = new();

        public HitPointsComponent(int maxHitPoints)
        {
            _hitPoints.Value = maxHitPoints;
            _maxHitPoints.Value = maxHitPoints;
        }
    }
}