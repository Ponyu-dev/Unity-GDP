using System;
using Declarative;
using Elementary;
using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.Visual.Zone
{
    [Serializable]
    public sealed class ZoneWidgetAdapter : 
        IAwakeListener,
        IEnableListener,
        IDisableListener
    {
        private IVariableLimited<int> _storage;
        private ZoneWidget _view;

        public void Construct(IVariableLimited<int> storage, ZoneWidget view)
        {
            _storage = storage;
            _view = view;
        }
        
        public void Awake()
        {
            Debug.Log("[ZoneWidgetAdapter] Awake");
            OnChanged(_storage.Current, _storage.MaxValue);
        }

        private void OnChanged(int currentValue, int maxValue)
        {
            var text = $"{currentValue} / {maxValue}";
            Debug.Log($"[ZoneWidgetAdapter] OnChanged {text}");
            _view.Text.text = text;
        }

        public void OnEnable()
        {
            Debug.Log("[ZoneWidgetAdapter] OnEnable");
            _storage.OnValueChanged += OnValueChanged;
            _storage.OnMaxValueChanged += OnMaxValueChanged;
        }
        
        private void OnValueChanged(int current) => OnChanged(current, _storage.MaxValue);

        private void OnMaxValueChanged(int maxValue) => OnChanged(_storage.Current, maxValue);

        public void OnDisable()
        {
            Debug.Log("[ZoneWidgetAdapter] OnDisable");
            _storage.OnValueChanged -= OnValueChanged;
            _storage.OnMaxValueChanged -= OnMaxValueChanged;
        }
    }
}