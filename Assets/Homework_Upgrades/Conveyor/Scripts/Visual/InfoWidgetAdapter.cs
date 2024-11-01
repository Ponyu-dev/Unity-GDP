using System;
using Declarative;
using Elementary;
using Homework_Upgrades.Conveyor.Scripts.System;
using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.Visual
{
    [Serializable]
    public sealed class InfoWidgetAdapter : 
        IAwakeListener,
        IEnableListener,
        IDisableListener
    {
        private IWorkMechanics _workMechanics;
        private ITimer _workTimer;
        private InfoWidget _view;

        public void Construct(ITimer workTimer, IWorkMechanics workMechanics, InfoWidget view)
        {
            _workTimer = workTimer;
            _workMechanics = workMechanics;
            _view = view;
        }

        void IAwakeListener.Awake()
        {
            _view.SetVisible(true);
            _view.ProgressBar.SetVisible(_workTimer.IsPlaying);
            _view.ButtonStart.gameObject.SetActive(!_workTimer.IsPlaying);
        }

        void IEnableListener.OnEnable()
        {
            Debug.Log("[InfoWidgetAdapter] OnEnable");
            _view.ButtonStart.onClick.AddListener(OnClickStart);
            _workTimer.OnStarted += OnWorkStarted;
            _workTimer.OnTimeChanged += OnWorkProgressChanged;
            _workTimer.OnFinished += OnWorkFinished;
        }

        private void OnClickStart()
        {
            Debug.Log("[InfoWidgetAdapter] OnClickStart");
            _workMechanics.StartWork();
        }

        void IDisableListener.OnDisable()
        {
            Debug.Log("[InfoWidgetAdapter] OnDisable");
            _view.ButtonStart.onClick.RemoveListener(OnClickStart);
            _workTimer.OnStarted -= OnWorkStarted;
            _workTimer.OnTimeChanged -= OnWorkProgressChanged;
            _workTimer.OnFinished -= OnWorkFinished;
        }

        private void OnWorkStarted()
        {
            _view.ButtonStart.gameObject.SetActive(false);
            _view.ProgressBar.SetVisible(true);
        }

        private void OnWorkProgressChanged()
        {
            _view.ProgressBar.SetProgress(_workTimer.Progress);
        }

        private void OnWorkFinished()
        {
            _view.ButtonStart.gameObject.SetActive(true);
            _view.ProgressBar.SetVisible(false);
        }
    }
}