using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SampleGame
{
    public sealed class PauseScreen : MonoBehaviour
    {
        [SerializeField]
        private Button resumeButton;

        [SerializeField]
        private Button exitButton;

        private MenuLoader menuLoader;
        private PauseButton pauseButton;

        [Inject]
        public void Construct(MenuLoader menuLoader, GameLoader gameLoader, PauseButton pauseButton)
        {
            this.menuLoader = menuLoader;
            this.pauseButton = pauseButton;
            this.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            this.pauseButton.OnClicked -= Show;
            this.resumeButton.onClick.AddListener(this.Hide);
            this.exitButton.onClick.AddListener(this.menuLoader.LoadMenu);
        }

        private void OnDisable()
        {
            this.pauseButton.OnClicked += Show;
            this.resumeButton.onClick.RemoveListener(this.Hide);
            this.exitButton.onClick.RemoveListener(this.menuLoader.LoadMenu);
        }

        public void Show()
        {
            Time.timeScale = 0; //KISS
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            Time.timeScale = 1; //KISS
            this.gameObject.SetActive(false);
        }

        public void OnDestroy()
        {
            this.pauseButton.OnClicked -= Show;
        }
    }
}