using System;
using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Game.Managers;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Presenters
{
    public class MainMenuPresenter : IDisposable, IStartable
    {
        private readonly IMainMenuView _mainMenuView;
        private readonly IPlayerFactory _playerFactory;
        private readonly ITimerPresenter _timerPresenter;
        private readonly ITurnManager _turnManager;

        [Inject]
        public MainMenuPresenter(
            IMainMenuView mainMenuView,
            IPlayerFactory playerFactory, 
            ITimerPresenter timerPresenter,
            ITurnManager turnManager)
        {
            Debug.Log("[MainMenuPresenter] Constructor");
            _mainMenuView = mainMenuView;
            _playerFactory = playerFactory;
            _timerPresenter = timerPresenter;
            _turnManager = turnManager;

            _mainMenuView.OnStartPresetClicked += OnStartPresetClicked;
            _mainMenuView.OnStartRandomClicked += OnStartRandomClicked;
        }

        public void Start()
        {
            Debug.Log("[MainMenuPresenter] Start");
            _mainMenuView.Show();
        }

        private void OnStartRandomClicked()
        {
            _playerFactory.CreatePlayerHeroesRandom();
            StartGame().Forget();
        }

        private void OnStartPresetClicked()
        {
            _playerFactory.CreatePlayerHeroes();
            StartGame().Forget();
        }

        private async UniTaskVoid StartGame()
        {
            _mainMenuView.Hide();
            await _timerPresenter.StartCountdown();
            _turnManager.Initialize();
            _turnManager.StartTurn();
        }
        
        public void Dispose()
        {
            _mainMenuView.OnStartPresetClicked -= OnStartPresetClicked;
            _mainMenuView.OnStartRandomClicked -= OnStartRandomClicked;
        }
    }
}