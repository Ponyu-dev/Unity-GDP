using System;
using Elementary;
using Game.GamePlay.Upgrades;
using Homework_Upgrades.Conveyor.Scripts.Entity.Configs;
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

        private UpdateTimerConfig _updateTimerConfig;

        public void Constructor(UpdateTimerConfig updateTimerConfig, IMoneyStorage moneyStorage)
        {
            base.Constructor(updateTimerConfig, moneyStorage);
            
            _updateTimerConfig = updateTimerConfig;

            _workTimer = new Timer(_updateTimerConfig.startTimerValue);
        }

        protected override void UpdateMaxLevel()
        {
            var newDuration = _workTimer.Duration - _updateTimerConfig.stepTimerValue;
            _workTimer.Duration = Mathf.Max(newDuration, _updateTimerConfig.minTimerValue);
        }
    }
}