using System.Collections.Generic;
using Declarative;
using Homework_Upgrades.UpdaterPanel.Scripts.Visual;

namespace Homework_Upgrades.UpdaterPanel.Scripts
{
    public sealed class UpdaterPanelWidget : IEnableListener, IDisableListener
    {
        private UpdaterPanelView _updaterPanelView;
        private IReadOnlyList<UpdaterWidgetAdapter> _updateWidgets;
        
        public void Construct(UpdaterPanelView updaterPanelView, IReadOnlyList<UpdaterWidgetAdapter> updateWidgets)
        {
            _updaterPanelView = updaterPanelView;
            _updateWidgets = updateWidgets;
        }
        
        void IEnableListener.OnEnable()
        {
            _updaterPanelView.btnShowPanel.onClick.AddListener(OnShow);
            _updaterPanelView.btnClosePanel.onClick.AddListener(OnHide);
        }

        private void OnShow()
        {
            foreach (var widget in _updateWidgets)
            {
                widget.PanelShow();
            }
            _updaterPanelView.updaterPanel.SetActive(true);
        }

        private void OnHide()
        {
            _updaterPanelView.updaterPanel.SetActive(false);
        }

        void IDisableListener.OnDisable()
        {
            _updaterPanelView.btnShowPanel.onClick.RemoveListener(OnShow);
            _updaterPanelView.btnClosePanel.onClick.RemoveListener(OnHide);
        }

    }
}