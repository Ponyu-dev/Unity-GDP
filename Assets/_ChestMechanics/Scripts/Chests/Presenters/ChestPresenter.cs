using System;
using System.Collections.Generic;
using _ChestMechanics.Chests.Data;
using _ChestMechanics.Chests.System;
using _ChestMechanics.Session;
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

        private static readonly IReadOnlyList<string> Anims = new List<string>
        {
            "Idle",
            "Tease"
        };
        
        private readonly Random RandomAnim = new();

        private DateTime? _openTime;

        private readonly IServerTimeSession _serverTimeSession;
        
        private Chest _chest;
        private IChestView _chestView;

        [Inject]
        public ChestPresenter(IServerTimeSession serverTimeSession)
        {
            Debug.Log("ChestPresenter Constructor");
            _serverTimeSession = serverTimeSession;
            _serverTimeSession.OnCurrentSessionLoad += InitOpenTime;
        }

        public void Initialize(Chest chest, IChestView chestView)
        {
            Debug.Log($"ChestPresenter Initialize {chest.TypeChest}");
            _chest = chest;
            _chestView = chestView;
        }

        private void InitOpenTime()
        {
            if (!_serverTimeSession.IsActualTimeReceived()) return;
            if (_openTime != null) return;
            
            _openTime = _serverTimeSession.GetCurrentTime().AddSeconds(_chest.UnlockTime);
        }

        public void UpdateTimer()
        {
            if (_openTime == null) return;
            
            var timer = _openTime - _serverTimeSession.GetCurrentTime();
            if (timer?.TotalSeconds > 0)
            {
                var timerText = $"{timer?.Hours:D2}:{timer?.Minutes:D2}.{timer?.Seconds:D2}";
                _chestView.SetTimer(timerText);
                //TODO сделать другую логику
                //_chestView.StartAnimation(Anims[RandomAnim.Next(Anims.Count)]);
            }
            else
            {
                _chestView.SetTimer("Need Open");
                _chestView.StartAnimation(NameOpen);
            }
        }

        public void Dispose()
        {
            _serverTimeSession.OnCurrentSessionLoad -= InitOpenTime;
        }
    }
}