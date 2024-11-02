using Homework_Upgrades.Conveyor.Scripts.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Homework_Upgrades.Conveyor.Scripts.Visual.Work
{
    public sealed class WorkWidget : MonoBehaviour
    {
        public ProgressBar ProgressBar => progressBar;
        public Button ButtonStart => btnStart;

        [SerializeField]
        private GameObject root;

        [SerializeField]
        private Button btnStart;
        
        [SerializeField]
        private ProgressBar progressBar;

        public void SetVisible(bool isVisible)
        {
            root.SetActive(isVisible);
        }
    }
}