using System;
using Declarative;
using Game.GamePlay.Upgrades;
using Homework_Upgrades.Conveyor.Scripts.App;
using Homework_Upgrades.Conveyor.Scripts.Entity.Configs;
using Homework_Upgrades.Conveyor.Scripts.Entity.Updaters;
using Homework_Upgrades.Conveyor.Scripts.System;
using Homework_Upgrades.Conveyor.Scripts.Visual.Conveyor;
using Homework_Upgrades.Conveyor.Scripts.Visual.Widgets.Sale;
using Homework_Upgrades.Conveyor.Scripts.Visual.Work;
using Homework_Upgrades.Conveyor.Scripts.Visual.Zone;
using Homework_Upgrades.MoneyStorage.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Homework_Upgrades.Conveyor.Scripts.Entity
{
    public class ConveyorModel : DeclarativeModel
    {
        [Section, SerializeField]
        public ConveyorConfig config;
        
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
        public MoneyStorageModel moneyStorageModel;
        
        [SerializeField, Space]
        public ConveyorStorageComponent conveyorLoadStorageComponent = new();

        [SerializeField, Space]
        public ConveyorStorageComponent conveyorUnloadStorageComponent = new();

        [SerializeField, Space]
        public ConveyorProduceTimeComponent conveyorProduceTimeComponent = new();

        public WorkMechanics workMechanics;

        public SaleData saleData;

        [Construct]
        private void ConstructStorages(ConveyorConfig config)
        {
            saleData = config.saleData;
            conveyorLoadStorageComponent.Constructor(config.loadStorageConfig, moneyStorageModel.core.moneyStorage);
            conveyorUnloadStorageComponent.Constructor(config.unLoadStorageConfig, moneyStorageModel.core.moneyStorage);
            conveyorProduceTimeComponent.Constructor(config.updateTimerConfig, moneyStorageModel.core.moneyStorage);
        }

        [Construct]
        private void ConstructWork(ConveyorConfig config)
        {
            saleData = config.saleData;
            workMechanics = new WorkMechanics(
                loadStorage: conveyorLoadStorageComponent.Storage,
                unloadStorage: conveyorUnloadStorageComponent.Storage,
                workTimer: conveyorProduceTimeComponent.WorkTimer
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
            _conveyorViewAdapter.Construct(core.conveyorProduceTimeComponent.WorkTimer, conveyorView);
            _loadZoneViewAdapter.Construct(core.conveyorLoadStorageComponent.Storage, loadZoneView);
            _unloadZoneViewAdapter.Construct(core.conveyorUnloadStorageComponent.Storage, unloadZoneView);
        }
    }
    
    [Serializable]
    public sealed class Canvas
    {
        [SerializeField] public WorkWidget workWidget;
        private readonly WorkWidgetAdapter _workWidgetAdapter = new();
        
        [SerializeField] public ZoneWidget zoneLoadWidget;
        private readonly ZoneWidgetAdapter _zoneLoadWidgetAdapter = new();
        
        [SerializeField] public ZoneWidget zoneUnLoadWidget;
        private readonly ZoneWidgetAdapter _zoneUnLoadWidgetAdapter = new();
        
        [SerializeField] public SaleWidget saleWidget;
        private readonly SaleWidgetAdapter _saleWidgetAdapter = new();

        [Construct]
        private void Construct(ConveyorConfig config, Core core)
        {
            _workWidgetAdapter.Construct(core.workMechanics, core.conveyorProduceTimeComponent.WorkTimer, workWidget);
            _zoneLoadWidgetAdapter.Construct(core.conveyorLoadStorageComponent.Storage, zoneLoadWidget);
            _zoneUnLoadWidgetAdapter.Construct(core.conveyorUnloadStorageComponent.Storage, zoneUnLoadWidget);
            _saleWidgetAdapter.Construct(core, saleWidget);
        }
    }
}