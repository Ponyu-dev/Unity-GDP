using System;
using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Game.Managers;
using _EventBus.Scripts.Players.Player;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Presenters
{
    public class MainMenuPresenter : IDisposable, IStartable
    {
        private readonly Color _winRed = new(Color.red.r, Color.red.g, Color.red.b, 0.5f);
        private readonly Color _winBlue = new(Color.blue.r, Color.blue.g, Color.blue.b, 0.5f);
        private readonly Color _draw = new(Color.gray.r, Color.gray.g, Color.gray.b, 0.5f);
        
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

            _turnManager.OnGameFinish += OnGameFinish;
            _mainMenuView.OnStartPresetClicked += OnStartPresetClicked;
            _mainMenuView.OnStartRandomClicked += OnStartRandomClicked;
        }

        private Color GetWinColor(PlayerType? playerType)
        {
            if (playerType == null)
                return _draw;
            return playerType == PlayerType.Red ? _winRed : _winBlue;

        }
        
        private void OnGameFinish(PlayerType? playerTypeWin)
        {
            var victory = string.IsNullOrEmpty(playerTypeWin.ToString()) 
                ? "Draw"
                : $"!!! VICTORY {playerTypeWin.ToString()} !!!";
            
            
            _mainMenuView.SetVictory(GetWinColor(playerTypeWin), victory);
            _mainMenuView.Show();
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
            _turnManager.OnGameFinish -= OnGameFinish;
            _mainMenuView.OnStartPresetClicked -= OnStartPresetClicked;
            _mainMenuView.OnStartRandomClicked -= OnStartRandomClicked;
        }
    }
}