using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace _ChestMechanics.Scripts.Reward.View
{
    public interface IRewardView
    {
        public void SetCount(string count, bool startAnim);
    }
    
    public class RewardView : MonoBehaviour, IRewardView
    {
        private const string TriggerRewardAdd = "RewardAdd";
        
        [SerializeField] private TextMeshProUGUI txtCount;
        [SerializeField] private Animator animator;
        [SerializeField] private float animationDuration = 1.0f;

        public void SetCount(string count, bool startAnim)
        {
            if (txtCount == null) return;

            if (startAnim)
            {
                animator.SetTrigger(TriggerRewardAdd);
                AnimateNumber(int.Parse(txtCount.text), int.Parse(count), animationDuration).Forget();
            }
            else
            {
                txtCount.text = count;
            }
        }
        
        private async UniTaskVoid AnimateNumber(int startValue, int endValue, float duration)
        {
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                var progress = Mathf.Clamp01(elapsedTime / duration);
                
                var currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, progress));
                txtCount.text = currentValue.ToString();

                await UniTask.Yield();
            }
            
            txtCount.text = endValue.ToString();
        }
    }
}