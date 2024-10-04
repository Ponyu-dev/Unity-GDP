using System;
using _ChestMechanics.Chests.Data;
using _ChestMechanics.Chests.Enums;
using _ChestMechanics.Chests.System;
using _ChestMechanics.Session;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using Random = System.Random;

namespace _ChestMechanics.Chests.Presenters
{
    public interface IChestPresenter
    {
        public void Initialize(Chest chest, IChestView chestView);
        public void UpdateTimer();
    }
    
    public class ChestPresenter : IChestPresenter, IDisposable
    {
        private const string NameOpen = "Open";
        private const string NameTease = "Tease";
        private const string NameIdle = "ChestIdle";
        
        private readonly Random _random = new();

        private DateTime? _openTime;
        private ChestOpenType _chestOpenType = ChestOpenType.Idle;

        private readonly IServerTimeSession _serverTimeSession;
        
        private Chest _chest;
        private IChestView _chestView;

        [Inject]
        public ChestPresenter(IServerTimeSession serverTimeSession)
        {
            Debug.Log("ChestPresenter Constructor");
            _serverTimeSession = serverTimeSession;
            _serverTimeSession.OnCurrentSessionLoad += OnCurrentSessionLoad;
        }

        private void OnCurrentSessionLoad()
        {
            InitOpenTime().Forget();
        }

        public void Initialize(Chest chest, IChestView chestView)
        {
            Debug.Log($"ChestPresenter Initialize {chest.TypeChest}");
            _chest = chest;
            _chestView = chestView;

            _chestView.OnChestOpen += OnChestOpen;
        }

        private void OnChestOpen()
        {
            if (_chestOpenType != ChestOpenType.CanOpen) return;
            
            Debug.Log($"{_chest.TypeChest} OPEN");
            Debug.Log("InitOpenTime");
            ChestOpened();
        }

        private void  ChestOpened()
        {
            _chestView.SetTimer("You Opened");
            _chestView.StartAnimation(NameOpen);
            _chestOpenType = ChestOpenType.Opened;
            InitOpenTime().Forget();
        }

        private async UniTaskVoid InitOpenTime()
        {
            if (!_serverTimeSession.IsActualTimeReceived()) return;

            await UniTask.Delay(_random.Next(1000, 2000));
            _openTime = _serverTimeSession.GetCurrentTime().AddSeconds(_chest.UnlockTime);
            _chestOpenType = ChestOpenType.Idle;
            _chestView.StartAnimation(NameIdle);
        }
        
        public void UpdateTimer()
        {
            if (_chestOpenType != ChestOpenType.Idle) return;
            if (_openTime == null) return;
            
            var timer = _openTime - _serverTimeSession.GetCurrentTime();
            if (timer?.TotalSeconds > 0)
            {
                var timerText = $"{timer?.Hours:D2}:{timer?.Minutes:D2}.{timer?.Seconds:D2}";
                _chestView.SetTimer(timerText);
            }
            else
            {
                _chestOpenType = ChestOpenType.CanOpen;
                _chestView.SetTimer("Need Open");
                _chestView.StartAnimation(NameTease);
            }
        }

        public void Dispose()
        {
            _serverTimeSession.OnCurrentSessionLoad -= OnCurrentSessionLoad;
        }
    }
}