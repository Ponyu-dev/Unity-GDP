using _ChestMechanics.Scripts.Reward.Data;
using _ChestMechanics.Scripts.Reward.View;
using VContainer;

namespace _ChestMechanics.Scripts.Reward.Presenter
{
    public interface IRewardPresenter
    {
        public void Initialize(RewardSave reward, IRewardView rewardView);
        public void UpdateRewardCount(string count, bool startAnim);
    }
    
    public class RewardPresenter : IRewardPresenter
    {
        private RewardSave _rewardSave;
        private IRewardView _rewardView;

        [Inject]
        public RewardPresenter() { }
        
        public void Initialize(RewardSave rewardSave, IRewardView rewardView)
        {
            _rewardView = rewardView;
            _rewardSave = rewardSave;

            UpdateRewardCount(_rewardSave.Count.ToString(), false);
        }

        public void UpdateRewardCount(string count, bool startAnim)
        {
            _rewardView.SetCount(count, startAnim);
        }
    }
}