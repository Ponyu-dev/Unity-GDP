using ShootEmUp;
using UnityEngine;
using UnityEngine.UI;

namespace _ShootEmUp.UI.Scripts
{
    public sealed class StartScreen : DefaultScreen, IInitializable, IGameTimerListener
    {
        [SerializeField]
        private Button btnStart;

        public void Initialize()
        {
            Show();
        }
        
        public void OnStartTimer()
        {
            Hide();
        }
        
        protected override void Show()
        {
            base.Show();
            btnStart.onClick.AddListener(OnClickBtnStart);
        }

        protected override void Hide()
        {
            btnStart.onClick.RemoveListener(OnClickBtnStart);
            base.Hide();
        }

        private void OnClickBtnStart()
        {
            m_GameManager.StartTimer();
        }
    }
}