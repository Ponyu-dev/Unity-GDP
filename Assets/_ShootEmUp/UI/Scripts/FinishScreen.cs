using ShootEmUp;
using UnityEngine;
using UnityEngine.UI;

namespace _ShootEmUp.UI.Scripts
{
    public sealed class FinishScreen : DefaultScreen,
        IFinishGameListener,
        ITimerGameListener
    {
        [SerializeField]
        private Button btnRestart;

        public void OnStartTimer()
        {
            Hide();
        }

        public void OnFinishGame()
        {
            Show();
        }

        protected override void Show()
        {
            btnRestart.onClick.AddListener(OnClick);
            base.Show();
        }

        protected override void Hide()
        {
            btnRestart.onClick.RemoveListener(OnClick);
            base.Hide();
        }

        private void OnClick()
        {
            m_GameManager.RestartGame();
        }
    }
}