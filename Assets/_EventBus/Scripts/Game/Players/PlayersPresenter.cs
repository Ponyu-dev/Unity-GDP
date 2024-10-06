using System.Collections.Generic;
using _EventBus.Scripts.Entities.Hero;
using _EventBus.Scripts.Entities.Player;
using _EventBus.Scripts.Game.Presenters;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Players
{
    public class PlayersPresenter : IStartable
    {
        private Dictionary<PlayerType, List<IHeroPresenter>> _heroPresenters = new();
        
        private readonly HeroView _prefabHeroView;

        private readonly PlayerConfig _playersRedConfig;
        private readonly Transform _containerPlayerRed;

        private readonly PlayerConfig _playersBlueConfig;
        private readonly Transform _containerPlayerBlue;

        private readonly IObjectResolver _resolver;

        [Inject]
        public PlayersPresenter(
            HeroView prefabHeroView,
            PlayerConfig playersRedConfig,
            Transform containerPlayerRed,
            PlayerConfig playersBlueConfig,
            Transform containerPlayerBlue,
            IObjectResolver resolver)
        {
            Debug.Log("PlayersPresenter Constructor");
            _prefabHeroView = prefabHeroView;
            _playersRedConfig = playersRedConfig;
            _containerPlayerRed = containerPlayerRed;
            _playersBlueConfig = playersBlueConfig;
            _containerPlayerBlue = containerPlayerBlue;
            _resolver = resolver;
        }

        public void Start()
        {
            CreatePlayer(_playersRedConfig.player, _containerPlayerRed);
            CreatePlayer(_playersBlueConfig.player, _containerPlayerBlue);
        }
        
        private void CreatePlayer(Player player, Transform container)
        {
            List<IHeroPresenter> heroPresenters = new();

            for (int index = 0, count = player.heroConfigs.Length; index < count; index++)
            {
                if (_heroPresenters.ContainsKey(player.playerType)) continue;
                
                var heroConfig = player.heroConfigs[index];
                heroPresenters.Add(CreateHeroPresenter(heroConfig, container));
            }

            _heroPresenters.Add(player.playerType, heroPresenters);
        }
        
        private IHeroPresenter CreateHeroPresenter(HeroConfig heroConfig, Transform container)
        {
            Debug.Log($"PlayersPresenter Create {heroConfig.hero.heroType}");
            
            var heroInstance = Object.Instantiate(_prefabHeroView, container);
            var heroView = heroInstance.GetComponent<HeroView>();
            var chestPresenter = _resolver.Resolve<IHeroPresenter>();
            _resolver.Inject(chestPresenter);
            chestPresenter.Initialize(heroConfig, heroView);
            
            return chestPresenter;
        }
    }
}