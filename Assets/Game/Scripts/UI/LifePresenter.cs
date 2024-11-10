using System;
using Atomic.Elements;
using Atomic.Entities;
using Atomic.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    [Serializable]
    public sealed class LifePresenter : IViewInit, IViewEnable, IViewDisable
    {
        [SerializeField] private Image imgLife;
        [SerializeField] private SceneEntity player;

        [ShowInInspector, ReadOnly] private int _maxHp;
        [ShowInInspector, ReadOnly] private IValue<int> _currentHp;
        
        public void Init()
        {
            _currentHp = player.GetHitPoints();
            _maxHp = _currentHp.Value;
        }

        public void Enable()
        {
            player.GetHitPoints().Subscribe(OnChangeHitPoints);
        }

        private void OnChangeHitPoints(int damage)
        {
            imgLife.fillAmount = 1f - (float)_currentHp.Value / _maxHp;
        }

        public void Disable()
        {
            player.GetHitPoints().Unsubscribe(OnChangeHitPoints);
        }
    }
}