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
        [Section, SerializeField]
        public ScriptableConveyor config;
        
        [Section, SerializeField, Space]
        public Core core;
        
        [Section, SerializeField]
        private Visual visual;
        
        [Section, SerializeField]
        private Canvas canvas;
    }

    [Serializable]
    public sealed class Core
    {
        [SerializeField, Space]
        public IntVariableLimited loadStorage = new();

        [SerializeField]
        public IntVariableLimited unloadStorage = new();

        [SerializeField, Space]
        public Timer workTimer = new();

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
            _conveyorViewAdapter.Construct(core.workTimer, conveyorView);
            _loadZoneViewAdapter.Construct(core.loadStorage, loadZoneView);
            _unloadZoneViewAdapter.Construct(core.unloadStorage, unloadZoneView);
        }
    }
    
    [Serializable]
    public sealed class Canvas
    {
        [SerializeField]
        private InfoWidget infoView;

        private readonly InfoWidgetAdapter _infoViewAdapter = new();

        [Construct]
        private void Construct(ScriptableConveyor config, Core core) //, ResourceInfoCatalog resourceCatalog
        {
            _infoViewAdapter.Construct(core.workTimer, core.workMechanics, infoView);

            //var inputType = config.inputResourceType;
            //var inputIcon = resourceCatalog.FindResource(inputType).icon;
            //infoView.SetInputIcon(inputIcon);

            //var outputType = config.outputResourceType;
            //var outputIcon = resourceCatalog.FindResource(outputType).icon;
            //infoView.SetOutputIcon(outputIcon);
        }
    }
}