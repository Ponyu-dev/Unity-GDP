using _EventBus.Scripts.Game.Factories;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Managers
{
    public class GameManager : IInitializable, IStartable
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly IHeroFactory _heroFactory;
        private readonly ITurnManager _turnManager;
        
        [Inject]
        public GameManager(
            IPlayerFactory playerFactory,
            IHeroFactory heroFactory,
            ITurnManager turnManager)
        {
            Debug.Log("[GameManager] Constructor");
            _playerFactory = playerFactory;
            _heroFactory = heroFactory;
            _turnManager = turnManager;

            _playerFactory.CreatePlayerHeroes();
        }

        public void Initialize()
        {
            Debug.Log("[GameManager] Initialize");
            _turnManager.Initialize();
        }

        public void Start()
        {
            Debug.Log("[GameManager] Start");

            //_turnManager.StartTurn();
        }
    }
}