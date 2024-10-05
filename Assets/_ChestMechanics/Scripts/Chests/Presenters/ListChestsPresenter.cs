using System.Collections.Generic;
using _ChestMechanics.Chests.Data;
using _ChestMechanics.Chests.System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ChestMechanics.Chests.Presenters
{
    public class ListChestsPresenter : IInitializable, ITickable
    {
        private readonly ChestsConfig _chestsConfig;
        private readonly IChestFactory _chestFactory;

        private readonly List<IChestPresenter> _chestPresenters;

        [Inject]
        public ListChestsPresenter(
            ChestsConfig chestsConfig,
            IChestFactory chestFactory)
        {
            Debug.Log("ListChestsPresenter Constructor");
            _chestsConfig = chestsConfig;
            _chestFactory = chestFactory;
            _chestPresenters = new List<IChestPresenter>();
        }

        public void Initialize()
        {
            Debug.Log("ListChestsPresenter Initialize");
            SpawnChests(_chestsConfig.GetChest());
        }
        
        private void SpawnChests(IEnumerable<Chest> chests)
        {
            foreach (var chest in chests)
            {
                _chestPresenters.Add(_chestFactory.Create(chest));
            }
        }
        
        public void Tick()
        {
            Debug.Log("ListChestsPresenter Tick");
            if (_chestPresenters.Count <= 0) return;

            foreach (var chest in _chestPresenters)
            {
                chest.UpdateTimer();
            }
        }
    }
}