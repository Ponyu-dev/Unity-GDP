using System;
using Elementary;
using Game.GamePlay.Upgrades;
using Homework_Upgrades.Conveyor.Scripts.Entity.Configs;
using Sirenix.OdinInspector;

namespace Homework_Upgrades.Conveyor.Scripts.Entity.Updaters
{
    public interface IConveyorStorage
    {
        
    }
    
    [Serializable]
    public class ConveyorStorageComponent : ConveyorUpdate
    {
        [ReadOnly, ShowInInspector]
        private IntVariableLimited _storage;
        public IntVariableLimited Storage => _storage;

        private UpdateStorageConfig _config;

        public void Constructor(UpdateStorageConfig loadStorageConfig, IMoneyStorage moneyStorage)
        {
            base.Constructor(loadStorageConfig, moneyStorage);
            
            _config = loadStorageConfig;
            
            _storage = new IntVariableLimited
            {
                Current = 0,
                MaxValue = _config.stepMaxValue
            };
        }

        protected override void UpdateMaxLevel()
        {
            _storage.MaxValue += _config.stepMaxValue;
        }

//#if UNITY_EDITOR
        [Button]
        private void SetCurrentValue(int value)
        {
            _storage.Current += value;
        }
//#endif
    }
}