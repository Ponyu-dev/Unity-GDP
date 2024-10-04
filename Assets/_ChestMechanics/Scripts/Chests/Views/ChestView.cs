using TMPro;
using UnityEngine;

namespace _ChestMechanics.Chests.System
{
    public interface IChestView
    {
        public void SetTimer(string timer);
        public void StartAnimation(string animationName);
    }
    
    [RequireComponent(typeof(Animator))]
    public class ChestView : MonoBehaviour, IChestView
    {
        [SerializeField] private TextMeshProUGUI txtTimer;
        [SerializeField] private Animator animator;
        
        public void SetTimer(string timer)
        {
            txtTimer.text = timer;
        }

        public void StartAnimation(string animationName)
        {
            animator.Play(animationName);
        }
    }
}