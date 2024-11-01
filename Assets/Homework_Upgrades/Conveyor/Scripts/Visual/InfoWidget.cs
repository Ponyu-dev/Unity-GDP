using UnityEngine;
using UnityEngine.UI;
using ProgressBar = Homework_Upgrades.Conveyor.Scripts.Helpers.ProgressBar;

namespace Homework_Upgrades.Conveyor.Scripts.Visual
{
    public sealed class InfoWidget : MonoBehaviour
    {
        public ProgressBar ProgressBar => progressBar;
        public Button ButtonStart => btnStart;

        [SerializeField]
        private GameObject root;

        [SerializeField]
        private Button btnStart;
        
        /*[SerializeField]
        private Image inputImage;

        [SerializeField]
        private Image outputImage;*/
        
        [SerializeField]
        private ProgressBar progressBar;

        public void SetVisible(bool isVisible)
        {
            root.SetActive(isVisible);
        }

        /*public void SetInputIcon(Sprite icon)
        {
            inputImage.sprite = icon;
        }

        public void SetOutputIcon(Sprite icon)
        {
            outputImage.sprite = icon;
        }*/
    }
}