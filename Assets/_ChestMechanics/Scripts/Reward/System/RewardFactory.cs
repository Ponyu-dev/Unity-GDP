using _ChestMechanics.Scripts.Reward.Data;
using _ChestMechanics.Scripts.Reward.Presenter;
using _ChestMechanics.Scripts.Reward.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ChestMechanics.Scripts.Reward.System
{
    public interface IRewardFactory
    {
        IRewardPresenter Create(RewardData reward);
    }
    
    public class RewardFactory : IInitializable, IRewardFactory
    {
        private readonly Transform _container;
        private readonly IObjectResolver _resolver;

        [Inject]
        public RewardFactory(
            Transform container,
            IObjectResolver resolver)
        {
            Debug.Log("RewardFactory Constructor");
            _container = container;
            _resolver = resolver;
        }

        public void Initialize()
        {
            Debug.Log("RewardFactory Initialize");
        }
        
        public IRewardPresenter Create(RewardData reward)
        {
            Debug.Log($"RewardFactory Create {reward.TypeReward}");
            var chestInstance = Object.Instantiate(reward.Prefab, _container);
            var chestView = chestInstance.GetComponent<RewardView>();

            // Инжектируем презентер в созданный сундук
            var chestPresenter = _resolver.Resolve<IRewardPresenter>();
            _resolver.Inject(chestPresenter);
            chestPresenter.Initialize(reward.SaveReward, chestView);

            Debug.Log($"RewardFactory Initialize {reward.TypeReward}");
            
            return chestPresenter;
        }
    }
}