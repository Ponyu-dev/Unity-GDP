using System;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using VContainer;

namespace _EventBus.Scripts.Game.Presenters
{
    public interface ITimerPresenter
    {
        public UniTask StartCountdown();
    }
    
    public class TimerPresenter : ITimerPresenter
    {
        private readonly ITimerView _timerView;

        [Inject]
        public TimerPresenter(ITimerView timerView)
        {
            Debug.Log("[TimerPresenter] Constructor");
            _timerView = timerView;
        }
        
        public async UniTask StartCountdown()
        {
            _timerView.Show();
            
            for (var i = _timerView.TimerCount; i >= 0; i--)
            {
                _timerView.SetTimer(i > 0 ? i.ToString() : "START");
                await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.UnscaledDeltaTime);
            }
            
            _timerView.Hide();
        }
    }
}