using System;
using _EventBus.Scripts.Players.Components;
using _EventBus.Scripts.Players.Hero;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using VContainer;

namespace _EventBus.Scripts.Game.Presenters
{
    public interface IHeroPresenter
    {
        void Init(HeroConfig heroConfig, HeroView heroView, IHeroEntity heroEntity);
        HeroView GetHeroView();
        HeroType GetHeroType();
        public void SetActive(bool active);
        public UniTask AnimateAttack(HeroType heroType, HeroView target);
    }
    
    public class HeroPresenter : IHeroPresenter, IDisposable
    {
        private HeroConfig _heroConfig;
        public HeroType GetHeroType() => _heroConfig.type;
        
        private HeroView _heroView;
        public HeroView GetHeroView() => _heroView;

        private IHeroEntity _heroEntity;

        [Inject]
        public HeroPresenter()
        {
            Debug.Log("[HeroPresenter] Constructor");
        }
        
        public void Init(
            HeroConfig heroConfig,
            HeroView heroView, 
            IHeroEntity heroEntity)
        {
            _heroConfig = heroConfig;
            _heroView = heroView;
            _heroEntity = heroEntity;
            Debug.Log($"[HeroPresenter] Initialize heroConfig.type = {_heroConfig.type}");
            
            _heroEntity.GetComponent<HitPointsComponent>().OnValueChanged += OnHitPointsChanged;
            
            InitView();
        }

        private void OnHitPointsChanged(int health)
        {
            _heroView.SetStats(GetStats(health));
        }

        //атака/здоровье
        private string GetStats(int hitPoint)
        {
            return $"{_heroConfig.damage}/{hitPoint}";
        }

        private void InitView()
        {
            _heroView.SetIcon(_heroConfig.portrait);
            _heroView.SetStats(GetStats(_heroConfig.health));
        }

        public void SetActive(bool active)
        {
            _heroView.SetActive(active);
        }

        public UniTask AnimateAttack(HeroType heroType, HeroView target)
        {
            return heroType == GetHeroType() ? _heroView.AnimateAttack(target) : default;
        }

        public void Dispose()
        {
            _heroEntity.GetComponent<HitPointsComponent>().OnValueChanged -= OnHitPointsChanged;
        }
    }
}