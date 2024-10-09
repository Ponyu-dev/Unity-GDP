using System;
using System.Collections.Generic;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Presenters;
using _EventBus.Scripts.Players.Hero;
using _EventBus.Scripts.Players.Player;
using Cysharp.Threading.Tasks;
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
        private readonly Dictionary<HeroType, IHeroPresenter> _heroPresenters = new();

        private readonly IHeroFactory _heroFactory;
        private readonly EventBus _eventBus;

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
            EventBus eventBus,
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
            _eventBus = eventBus;
            _heroesConfig = heroesConfig;
            _prefabHeroView = prefabHeroView;
            _playersConfigRed = playersConfigRed;
            _containerPlayerRed = containerPlayerRed;
            _playersConfigBlue = playersConfigBlue;
            _containerPlayerBlue = containerPlayerBlue;
            _resolver = resolver;
            
            _eventBus.Subscribe<TurnStartedEvent>(OnTurnStarted);
            _eventBus.Subscribe<AttackedAnimEvent>(OnAttackedAnim);
            //_eventBus.Subscribe<AttackedEvent>(OnAttacked);
            _eventBus.Subscribe<DealDamageEvent>(OnDealDamage);
            _eventBus.Subscribe<DiedEvent>(OnDied);
            _eventBus.Subscribe<TurnEndedEvent>(OnTurnEnded);
        }
        
        private void OnTurnStarted(TurnStartedEvent evt)
        {
            var attackerHeroType = evt.CurrentHeroEntity.HeroType;
            Debug.Log($"[PlayerFactory] OnTurnStarted {attackerHeroType}");
            
            if (_heroPresenters.TryGetValue(attackerHeroType, out var attacker))
                attacker.SetActive(true);
        }

        private async void OnAttackedAnim(AttackedAnimEvent evt)
        {
            var attackerType = evt.Attacker.HeroType;
            var targetType = evt.Target.HeroType;

            if (!_heroPresenters.TryGetValue(attackerType, out var attacker) ||
                !_heroPresenters.TryGetValue(targetType, out var target)) 
                return;
            
            await attacker.AnimateAttack(attackerType, target.GetHeroView());
            _eventBus.RaiseEvent(new AttackedEvent(evt.Attacker, evt.Target));
            attacker.SetActive(false);
        }
        
        private void OnDealDamage(DealDamageEvent obj)
        {
            //Debug.Log($"[PlayerFactory] OnDealDamage {obj.Target.HeroType}");
        }
        
        private void OnDied(DiedEvent obj)
        {
            Debug.Log($"[PlayerFactory] OnDied {obj.Target}");
        }

        private void OnTurnEnded(TurnEndedEvent obj)
        {
            var heroType = obj.Current.HeroType;
            Debug.Log($"[PlayerFactory] OnTurnEnded {heroType}");
            if (_heroPresenters.TryGetValue(heroType, out var hero))
                hero.SetActive(false);
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
                if (_heroPresenters.ContainsKey(heroType)) continue;
                _heroPresenters.Add(heroType, CreateHeroPresenter(playerType, _heroesConfig.GetHeroConfig(heroType), container));
            }
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
            var heroPresenter = _resolver.Resolve<IHeroPresenter>();
            _resolver.Inject(heroPresenter);
            heroPresenter.Init(heroConfig, heroView, heroEntity);

            return heroPresenter;
        }

        public void Dispose()
        {
            _resolver?.Dispose();
            
            _eventBus.Unsubscribe<TurnStartedEvent>(OnTurnStarted);
            _eventBus.Unsubscribe<AttackedAnimEvent>(OnAttackedAnim);
            //_eventBus.Unsubscribe<AttackedEvent>(OnAttacked);
            _eventBus.Unsubscribe<DealDamageEvent>(OnDealDamage);
            _eventBus.Unsubscribe<DiedEvent>(OnDied);
            _eventBus.Unsubscribe<TurnEndedEvent>(OnTurnEnded);
        }
    }
}