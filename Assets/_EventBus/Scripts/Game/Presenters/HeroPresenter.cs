using _EventBus.Scripts.Players.Hero;
using UI;
using UnityEngine;
using VContainer;

namespace _EventBus.Scripts.Game.Presenters
{
    public interface IHeroPresenter
    {
        void Initialize(HeroConfig heroConfig, HeroView heroView, IHeroEntity heroEntity);
    }
    
    public class HeroPresenter : IHeroPresenter
    {
        private readonly EventBus _eventBus;
        private HeroConfig _heroConfig;
        private HeroView _heroView;
        private IHeroEntity _heroEntity;

        [Inject]
        public HeroPresenter(EventBus eventBus)
        {
            _eventBus = eventBus;
            Debug.Log("[HeroPresenter] Constructor");
        }
        
        public void Initialize(
            HeroConfig heroConfig,
            HeroView heroView,
            IHeroEntity heroEntity)
        {
            _heroConfig = heroConfig;
            _heroView = heroView;
            _heroEntity = heroEntity;
            
            Debug.Log($"[HeroPresenter] Initialize heroConfig.type = {_heroConfig.type}");
            Debug.Log($"[HeroPresenter] Initialize heroEntity.PlayerType = {_heroEntity.PlayerType}");

            InitView();
        }

        private void InitView()
        {
            _heroView.SetIcon(_heroConfig.portrait);
            _heroView.SetStats($"{_heroConfig.damage}/{_heroConfig.health}");//атака/здоровье
        }
    }
}