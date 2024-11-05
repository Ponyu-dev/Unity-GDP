using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _PresentationModel.Scripts.UI
{
    public sealed class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textPreogress;

        [SerializeField]
        private Color completeTextColor;

        [SerializeField]
        private Color processingTextColor;

        [Space]
        [SerializeField]
        private Image fill;

        [SerializeField]
        private Color completeFillColor;

        [SerializeField]
        private Color progressFillColor;
        
        public void SetProgress(float progress, string text)
        {
            this.fill.fillAmount = progress;
            this.textPreogress.text = text;

            if (progress >= 1)
            {
                this.textPreogress.color = this.completeTextColor;
                this.fill.color = this.completeFillColor;
            }
            else
            {
                this.textPreogress.color = this.processingTextColor;
                this.fill.color = this.progressFillColor;
            }
        }
    }
}