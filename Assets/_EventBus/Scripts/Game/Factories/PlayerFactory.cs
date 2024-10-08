using System;
using System.Collections.Generic;
using _EventBus.Scripts.Game.Presenters;
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
    
    public class PlayerFactory : IPlayerFactory
    {
        private readonly Dictionary<PlayerType, List<IHeroPresenter>> _heroPresenters = new();

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
            List<IHeroPresenter> heroPresenters = new();

            for (int index = 0, count = playerConfig.heroTypes.Length; index < count; index++)
            {
                if (_heroPresenters.ContainsKey(playerType)) continue;

                var heroType = playerConfig.heroTypes[index];
                heroPresenters.Add(CreateHeroPresenter(playerType, _heroesConfig.GetHeroConfig(heroType), container));
            }

            _heroPresenters.Add(playerType, heroPresenters);
        }

        private IHeroPresenter CreateHeroPresenter(
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
            
            //Создание презентра
            var chestPresenter = _resolver.Resolve<IHeroPresenter>();
            _resolver.Inject(chestPresenter);
            chestPresenter.Initialize(heroConfig, heroView, heroEntity);

            return chestPresenter;
        }
    }
}