using System;
using Declarative;
using Elementary;
using Game.GamePlay.Conveyor;
using Homework_Upgrades.Conveyor.Scripts.System;
using Homework_Upgrades.Conveyor.Scripts.Visual;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.Entity
{
    public class ConveyorModel : DeclarativeModel
    {
        [Section, SerializeField] public ScriptableConveyor config;
        [Section, SerializeField, Space] public Core core;
        [Section, SerializeField] private Visual visual;
    }

    [Serializable]
    public sealed class Core
    {
        [SerializeField, Space] public IntVariableLimited loadStorage = new();

        [SerializeField] public IntVariableLimited unloadStorage = new();

        [SerializeField, Space] public Timer workTimer = new();

        public WorkMechanics workMechanics;

        [Button] public void StartWork() => workMechanics.StartWork();

        [Construct]
        private void ConstructStorages(ScriptableConveyor config)
        {
            loadStorage.MaxValue = config.inputCapacity;
            unloadStorage.MaxValue = config.outputCapacity;
        }

        [Construct]
        private void ConstructWork(ScriptableConveyor config)
        {
            workTimer.Duration = config.workTime;
            workMechanics = new WorkMechanics(
                loadStorage: loadStorage,
                unloadStorage: unloadStorage,
                workTimer: workTimer
            );
        }
    }
    
    [Serializable]
    public sealed class Visual
    {
        [SerializeField]
        private ConveyorVisual conveyorView;

        [SerializeField]
        private ZoneVisual loadZoneView;

        [SerializeField]
        private ZoneVisual unloadZoneView;

        private readonly ConveyorVisualAdapter _conveyorViewAdapter = new();

        private readonly ZoneVisualAdapter _loadZoneViewAdapter = new();

        private readonly ZoneVisualAdapter _unloadZoneViewAdapter = new();

        [Construct]
        private void Construct(Core core)
        {
            this._conveyorViewAdapter.Construct(core.workTimer, this.conveyorView);
            this._loadZoneViewAdapter.Construct(core.loadStorage, this.loadZoneView);
            this._unloadZoneViewAdapter.Construct(core.unloadStorage, this.unloadZoneView);
        }
    }
}