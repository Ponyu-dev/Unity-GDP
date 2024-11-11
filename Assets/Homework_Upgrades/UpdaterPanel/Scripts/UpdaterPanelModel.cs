using System;
using Declarative;
using Homework_Upgrades.Conveyor.Scripts.Entity;
using Homework_Upgrades.UpdaterPanel.Scripts.Visual;
using UnityEngine;

namespace Homework_Upgrades.UpdaterPanel.Scripts
{
    public sealed class UpdaterPanelModel : DeclarativeModel
    {
        [Section, SerializeField, Space]
        public Core core;
        
        [Section, SerializeField, Space]
        public Canvas canvas;
    }

    [Serializable]
    public sealed class Core
    {
        [SerializeField, Space]
        public ConveyorModel conveyorModel;
    }

    [Serializable]
    public sealed class Canvas
    {
        [SerializeField] private UpdaterPanelView updaterPanelView;
        private UpdaterPanelWidget _updaterPanelWidget = new();
        
        [SerializeField] private UpdaterWidgetView updaterLoadZone;
        private UpdaterWidgetAdapter _updaterLoadZoneAdapter = new(); 
            
        [SerializeField] private UpdaterWidgetView updaterUnLoadZone;
        private UpdaterWidgetAdapter _updaterUnLoadZoneAdapter = new();
        
        [SerializeField] private UpdaterWidgetView updaterTimer;
        private UpdaterWidgetAdapter _updaterTimerAdapter = new();
        
        [Construct]
        private void Construct(Core core)
        {
            _updaterLoadZoneAdapter.Construct(core.conveyorModel.core.conveyorLoadStorageComponent, updaterLoadZone);
            _updaterUnLoadZoneAdapter.Construct(core.conveyorModel.core.conveyorUnloadStorageComponent, updaterUnLoadZone);
            _updaterTimerAdapter.Construct(core.conveyorModel.core.conveyorProduceTimeComponent, updaterTimer);
            
            _updaterPanelWidget.Construct(updaterPanelView, new []
            {
                _updaterLoadZoneAdapter,
                _updaterUnLoadZoneAdapter,
                _updaterTimerAdapter
            });
        }
    }
}