using System;
using Homework_Upgrades.Conveyor.Scripts.Entity.Configs;
using Homework_Upgrades.MoneyStorage.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.Entity.Updaters
{
    public interface IConveyorUpdate
    {
        public bool GetUpdateMaxLevel(out int level);
    }
    
    [Serializable]
    public abstract class ConveyorUpdate : IConveyorUpdate
    {
        [ReadOnly, ShowInInspector] public int NextPrice => DefaultPrice * Level;
        [ReadOnly, ShowInInspector] protected int DefaultPrice;
        
        [ReadOnly, ShowInInspector] protected int Level;
        [ReadOnly, ShowInInspector] protected int MaxLevel;
        
        private IMoneyStorage _moneyStorage;
        private UpdateLevelConfig _updateConfig;

        protected void Constructor(UpdateLevelConfig updateConfig, IMoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
            _updateConfig = updateConfig;

            DefaultPrice = _updateConfig.defaultPrice;
            MaxLevel = _updateConfig.maxLevel;
            Level = 1;
        }
        
        private bool CanUpdateMaxLevel()
        {
            return Level < MaxLevel && _moneyStorage.CanSpendMoney(NextPrice);
        }

        protected abstract void UpdateMaxLevel();
        
        public bool GetUpdateMaxLevel(out int level)
        {
            level = Level;
            if (!CanUpdateMaxLevel()) return false;

            _moneyStorage.SpendMoney(NextPrice);
            Level += 1;
            level = Level;
            UpdateMaxLevel();

            return true;
        }
        
#if UNITY_EDITOR
        [Button]
        private void EditorUpdateMaxLevel()
        {
            var updateLevel = GetUpdateMaxLevel(out var newLevel);
            Debug.Log($"Level {newLevel} Update {updateLevel}");
        }
#endif
    }
}