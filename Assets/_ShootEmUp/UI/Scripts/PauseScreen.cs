using ShootEmUp;
using UnityEngine;
using UnityEngine.UI;

namespace _ShootEmUp.UI.Scripts
{
    public sealed class PauseScreen : DefaultScreen,
        IGamePauseListener,
        IGameResumeListener
    {
        [SerializeField]
        private Button btnResume;
        
        public void OnPauseGame()
        {
            Show();
        }

        public void OnResumeGame()
        {
            Hide();
        }

        protected override void Show()
        {
            base.Show();
            btnResume.onClick.AddListener(OnClickBtnResume);
        }

        protected override void Hide()
        {
            btnResume.onClick.RemoveListener(OnClickBtnResume);
            base.Hide();
        }
        
        private void OnClickBtnResume()
        {
            m_GameManager.ResumeGame();
        }
    }
}