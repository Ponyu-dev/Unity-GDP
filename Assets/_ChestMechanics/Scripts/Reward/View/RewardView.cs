using TMPro;
using UnityEngine;

namespace _ChestMechanics.Scripts.Reward.View
{
    public interface IRewardView
    {
        public void SetCount(string count);
    }
    
    public class RewardView : MonoBehaviour, IRewardView
    {
        [SerializeField] private TextMeshProUGUI txtCount;

        public void SetCount(string count)
        {
            if (txtCount == null) return;

            txtCount.text = count;
        }
    }
}