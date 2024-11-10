using System;
using Atomic.Elements;
using Atomic.Entities;
using Atomic.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    [Serializable]
    public sealed class AmmoPresenter : IViewInit, IViewEnable, IViewDisable
    {
        [SerializeField] private TextMeshProUGUI txtAmmo; 
        [SerializeField] private SceneEntity player;

        [ShowInInspector, ReadOnly] private Const<int> _maxAmmo; 
        [ShowInInspector, ReadOnly] private IReactiveValue<int> _currentAmmo; 
        
        public void Init()
        {
            _maxAmmo = player.GetMaxAmmo();
            _currentAmmo = player.GetCurrentAmmo();
            SetTextAmmo($"{_currentAmmo.Value} / {_maxAmmo.Value}");
        }

        private void SetTextAmmo(string ammo)
        {
            txtAmmo.text = ammo;
        }

        public void Enable()
        {
            _currentAmmo.Subscribe(OnValueChanged);
        }

        private void OnValueChanged(int current)
        {
            SetTextAmmo($"{current} / {_maxAmmo.Value}");
        }

        public void Disable()
        {
            _currentAmmo.Unsubscribe(OnValueChanged);
        }
    }
}