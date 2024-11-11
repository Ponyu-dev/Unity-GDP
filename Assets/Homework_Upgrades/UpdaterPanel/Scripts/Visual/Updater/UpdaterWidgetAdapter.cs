using Declarative;
using Homework_Upgrades.Conveyor.Scripts.Entity.Updaters;
using UnityEngine;

namespace Homework_Upgrades.UpdaterPanel.Scripts.Visual
{
    public sealed class UpdaterWidgetAdapter : IEnableListener, IDisableListener
    {
        private UpdaterWidgetView _updaterView;
        private IConveyorUpdate _conveyorUpdate;
        
        public void Construct(IConveyorUpdate conveyorUpdate, UpdaterWidgetView updaterView)
        {
            _updaterView = updaterView;
            _conveyorUpdate = conveyorUpdate;
        }

        public void OnEnable()
        {
            Debug.Log("[UpdaterWidgetAdapter] OnEnable");
            _updaterView.BtnUpdater.onClick.AddListener(OnUpdateClick);
        }

        public void PanelShow()
        {
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            _updaterView.SetLevel(_conveyorUpdate.GetInfoLevel);
            _updaterView.SetMaxUpdater(_conveyorUpdate.GetInfoUpdater);
            _updaterView.SetPrice(_conveyorUpdate.NextPrice.ToString());
            _updaterView.BtnUpdater.interactable = _conveyorUpdate.CanUpdateMaxLevel();
        }

        private void OnUpdateClick()
        {
            if (_conveyorUpdate.GetUpdateMaxLevel(out var level))
            {
                UpdateInfo();
            }
        }
        
        public void OnDisable()
        {
            Debug.Log("[UpdaterWidgetAdapter] OnDisable");
            _updaterView.BtnUpdater.onClick.RemoveListener(OnUpdateClick);
        }
    }
}