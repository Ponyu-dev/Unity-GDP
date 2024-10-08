using System;
using Cysharp.Threading.Tasks;
using ShootEmUp;
using TMPro;
using UnityEngine;

namespace _ShootEmUp.UI.Scripts
{
    public sealed class TimerScreen : DefaultScreen, ITimerGameListener, IStartGameListener
    {
        [SerializeField] 
        private int countTimer = 3;
        
        [SerializeField]
        private TextMeshProUGUI m_TextTimer; 

        public void OnStartTimer()
        {
            Show();
        }

        protected override void Show()
        {
            base.Show();
            StartCountdown().Forget();
        }

        private async UniTaskVoid StartCountdown()
        {
            for (var i = countTimer; i >= 0; i--)
            {
                m_TextTimer.text = i.ToString();
                await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.UnscaledDeltaTime);
            }
            
            m_GameManager.StartGame();
        }

        
        public void OnStartGame()
        {
            Hide();
        }
        
        protected override void Hide()
        {
            base.Hide();
            m_TextTimer.text = "0";
        }
    }
}