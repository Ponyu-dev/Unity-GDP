using _PresentationModel.Scripts.UI;
using TMPro;
using UnityEngine;

namespace _PresentationModel.Scripts.LevelUp.Views
{
    public class PlayerLevelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtCurrentLevel;
        [SerializeField] private ProgressBar playerLevelView;

        public void SetCurrentLevel(string level) => txtCurrentLevel.text = level;
        public void SetLevelProgress(float progress, string text) => playerLevelView.SetProgress(progress, text);
    }
}