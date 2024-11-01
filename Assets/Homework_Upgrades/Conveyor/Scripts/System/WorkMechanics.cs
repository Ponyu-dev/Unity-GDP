using Elementary;
using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.System
{
    public interface IWorkMechanics
    {
        void StartWork();
    }

    public sealed class WorkMechanics : IWorkMechanics
    {
        private readonly IVariableLimited<int> _loadStorage;
        private readonly IVariableLimited<int> _unloadStorage;
        private readonly ITimer _workTimer;

        public WorkMechanics(
            IVariableLimited<int> loadStorage,
            IVariableLimited<int> unloadStorage,
            ITimer workTimer)
        {
            _loadStorage = loadStorage;
            _unloadStorage = unloadStorage;
            _workTimer = workTimer;
        }
        
        private bool CanStartWork()
        {
            if (_workTimer.IsPlaying)
                return false;

            if (_loadStorage.Current == 0) 
                return false;

            if (_unloadStorage.IsLimit)
                return false;

            return true;
        }

        public void StartWork()
        {
            if (!CanStartWork()) return;
            
            Debug.Log("[WorkMechanics] StartWork");
            
            _workTimer.OnFinished += OnWorkFinished;
            _workTimer.OnCanceled += OnWorkFinished;
            
            _loadStorage.Current--;
            _workTimer.ResetTime();
            _workTimer.Play();
        }

        private void OnWorkFinished()
        {
            _workTimer.OnFinished -= OnWorkFinished;
            _unloadStorage.Current++;
        }
    }
}