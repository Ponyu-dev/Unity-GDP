using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public interface IMainMenuView
    {
        public event UnityAction OnStartPresetClicked;
        public event UnityAction OnStartRandomClicked;

        public void SetVictory(Color color, string victory);
        public void Show();
        public void Hide();
    }
    
    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        [SerializeField] private TextMeshProUGUI textVictory;
        [SerializeField] private Image imgBack;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Button btnStartPreset;
        [SerializeField] private Button btnStartRandom;
        
        public event UnityAction OnStartPresetClicked
        {
            add { this.btnStartPreset.onClick.AddListener(value); }
            remove { this.btnStartPreset.onClick.RemoveListener(value); }
        }
        
        public event UnityAction OnStartRandomClicked
        {
            add { this.btnStartRandom.onClick.AddListener(value); }
            remove { this.btnStartRandom.onClick.RemoveListener(value); }
        }

        public void SetVictory(Color color, string victory)
        {
            imgBack.color = color;
            textVictory.gameObject.SetActive(true);
            textVictory.text = victory;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            textVictory.gameObject.SetActive(false);
            gameObject.SetActive(false);
            imgBack.color = defaultColor;
        }
    }
}