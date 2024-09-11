using ShootEmUp;
using UnityEngine;
using UnityEngine.UI;

namespace _ShootEmUp.UI.Scripts
{
    public sealed class PlayingScreen : DefaultScreen, 
        IStartGameListener, 
        IPauseGameListener, 
        IResumeGameListener,
        IFinishGameListener
    {
        [SerializeField]
        private Button btnPause;
        
        public void OnStartGame()
        {
            Show();
        }

        public void OnPauseGame()
        {
            Hide();
        }

        public void OnResumeGame()
        {
            Show();
        }

        public void OnFinishGame()
        {
            Hide();
        }

        protected override void Show()
        {
            base.Show();
            btnPause.onClick.AddListener(OnClickBtnPause);
        }

        protected override void Hide()
        {
            btnPause.onClick.RemoveListener(OnClickBtnPause);
            base.Hide();
        }

        private void OnClickBtnPause()
        {
            m_GameManager.PauseGame();
        }
    }
}