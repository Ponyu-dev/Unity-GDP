using System;
using _EventBus.Scripts.Players.Components;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using VContainer;

namespace _EventBus.Scripts.Game.Presenters
{
    public interface IHeroPresenter
    {
        void Init(
            HeroView heroView, HitPointsComponent hitPointsComponent, 
            int defaultDamage, Sprite portrait);
        HeroView GetHeroView();
        public void SetActive(bool active);
        public UniTask AnimateAttack(HeroView target);
    }
    
    public class HeroPresenter : IHeroPresenter, IDisposable
    {
        private HeroView _heroView;
        public HeroView GetHeroView() => _heroView;
        
        private HitPointsComponent _hitPointsComponent;
        private int _defaultDamage;

        [Inject]
        public HeroPresenter()
        {
            Debug.Log("[HeroPresenter] Constructor");
        }
        
        public void Init(
            HeroView heroView,
            HitPointsComponent hitPointsComponent,
            int defaultDamage,
            Sprite portrait)
        {
            _heroView = heroView;
            _hitPointsComponent = hitPointsComponent;
            _defaultDamage = defaultDamage;
            
            _heroView.SetStats(GetStats(hitPointsComponent.Value));
            _heroView.SetIcon(portrait);
            
            _hitPointsComponent.OnValueChanged += OnHitPointsChanged;
        }

        private void OnHitPointsChanged(int health)
        {
            _heroView.SetStats(GetStats(health));
        }

        //атака/здоровье
        private string GetStats(int hitPoint)
        {
            return $"{_defaultDamage}/{hitPoint}";
        }

        public void SetActive(bool active)
        {
            _heroView.SetActive(active);
        }

        public UniTask AnimateAttack(HeroView target)
        {
            return _heroView.AnimateAttack(target);
        }

        public void Dispose()
        {
            _hitPointsComponent.OnValueChanged -= OnHitPointsChanged;
        }
    }
}