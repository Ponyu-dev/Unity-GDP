using System;
using Elementary;
using Homework_Upgrades.Conveyor.Scripts.Entity.Configs;
using Homework_Upgrades.MoneyStorage.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.Entity.Updaters
{
    [Serializable]
    public class ConveyorProduceTimeComponent : ConveyorUpdate
    {
        [ReadOnly, ShowInInspector]
        private Timer _workTimer;
        public ITimer WorkTimer => _workTimer;

        private UpdateTimerConfig _config;
        public override string GetInfoUpdater => $"SpeedWork: {_workTimer.Duration} (-{_config.stepTimerValue})";

        public void Constructor(UpdateTimerConfig updateTimerConfig, IMoneyStorage moneyStorage)
        {
            base.Constructor(updateTimerConfig, moneyStorage);
            
            _config = updateTimerConfig;

            _workTimer = new Timer(_config.startTimerValue);
        }

        protected override void UpdateMaxLevel()
        {
            var newDuration = _workTimer.Duration - _config.stepTimerValue;
            _workTimer.Duration = Mathf.Max(newDuration, _config.minTimerValue);
        }
    }
}