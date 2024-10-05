using _ChestMechanics.Chests.Data;
using _ChestMechanics.Chests.Presenters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ChestMechanics.Chests.System
{
    public interface IChestFactory
    {
        IChestPresenter Create(Chest chest);
    }
    
    public class ChestFactory : IInitializable, IChestFactory
    {
        private readonly Transform _containerChests;
        private readonly IObjectResolver _resolver;

        [Inject]
        public ChestFactory(
            Transform containerChests,
            IObjectResolver resolver)
        {
            Debug.Log("ChestFactory Constructor");
            _containerChests = containerChests;
            _resolver = resolver;
        }

        public void Initialize()
        {
            Debug.Log("ChestFactory Initialize");
        }

        public IChestPresenter Create(Chest chest)
        {
            Debug.Log($"ChestFactory Create {chest.TypeChest}");
            var chestInstance = Object.Instantiate(chest.Prefab, _containerChests);
            var chestView = chestInstance.GetComponent<ChestView>();

            // Инжектируем презентер в созданный сундук
            var chestPresenter = _resolver.Resolve<IChestPresenter>();
            _resolver.Inject(chestPresenter);
            chestPresenter.Initialize(chest, chestView);

            Debug.Log($"ChestFactory Initialize {chest.TypeChest}");
            
            return chestPresenter;
        }
    }
}