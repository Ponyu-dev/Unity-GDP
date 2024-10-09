using System;
using _EventBus.Scripts.Game.Presenters;
using _EventBus.Scripts.Players.Components;
using _EventBus.Scripts.Players.Hero;
using _EventBus.Scripts.Players.Player;
using UI;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace _EventBus.Scripts.Game.Factories
{
    public interface IPlayerFactory
    {
        public void CreatePlayerHeroes();
        //public void CreatePlayerHeroesRandom();
    }
    
    public class PlayerFactory : IPlayerFactory, IDisposable
    {
        private readonly IHeroFactory _heroFactory;

        private readonly HeroesConfig _heroesConfig;
        private readonly HeroView _prefabHeroView;

        private readonly PlayerConfig _playersConfigRed;
        private readonly Transform _containerPlayerRed;

        private readonly PlayerConfig _playersConfigBlue;
        private readonly Transform _containerPlayerBlue;

        private readonly IObjectResolver _resolver;
        
        [Inject]
        public PlayerFactory(
            IHeroFactory heroFactory,
            HeroesConfig heroesConfig,
            HeroView prefabHeroView,
            PlayerConfig playersConfigRed,
            Transform containerPlayerRed,
            PlayerConfig playersConfigBlue,
            Transform containerPlayerBlue,
            IObjectResolver resolver)
        {
            Debug.Log("[PlayerFactory] Constructor");
            _heroFactory = heroFactory;
            _heroesConfig = heroesConfig;
            _prefabHeroView = prefabHeroView;
            _playersConfigRed = playersConfigRed;
            _containerPlayerRed = containerPlayerRed;
            _playersConfigBlue = playersConfigBlue;
            _containerPlayerBlue = containerPlayerBlue;
            _resolver = resolver;
        }

        public void CreatePlayerHeroes()
        {
            Debug.Log("[PlayerFactory] CreatePlayerHeroes");
            try
            {
                CreatePlayer(_playersConfigRed, _containerPlayerRed);
                CreatePlayer(_playersConfigBlue, _containerPlayerBlue);
            }
            catch (Exception e)
            {
                Debug.Log("[PlayerFactory] CreatePlayerHeroes Exception");
                Debug.LogException(e);
            }
        }

        private void CreatePlayer(PlayerConfig playerConfig, Transform container)
        {
            var playerType = playerConfig.playerType;
            Debug.Log($"[PlayerFactory] CreatePlayer {playerType}");

            for (int index = 0, count = playerConfig.heroTypes.Length; index < count; index++)
            {
                var heroType = playerConfig.heroTypes[index];
                CreateHeroPresenter(playerType, _heroesConfig.GetHeroConfig(heroType), container);
            }
        }

        private void CreateHeroPresenter(
            PlayerType playerType,
            HeroConfig heroConfig,
            Transform container)
        {
            Debug.Log($"[PlayerFactory] CreateHeroPresenter {heroConfig.type}");

            //Создание View
            var heroInstance = Object.Instantiate(_prefabHeroView, container);
            var heroView = heroInstance.GetComponent<HeroView>();
            
            //Создание Entity
            var heroEntity = _heroFactory.CreateEntity(playerType, heroConfig);
            var hitPointsComponent = heroEntity.GetComponent<HitPointsComponent>();
            heroEntity.AddComponent(new DestroyComponent(heroView.gameObject));
            
            //Создание презентра
            var heroPresenter = _resolver.Resolve<IHeroPresenter>();
            _resolver.Inject(heroPresenter);
            heroPresenter.Init(heroView, hitPointsComponent, heroConfig.damage, heroConfig.portrait);
            
            heroEntity.AddComponent(heroPresenter);
        }

        public void Dispose()
        {
            _resolver?.Dispose();
        }
    }
}