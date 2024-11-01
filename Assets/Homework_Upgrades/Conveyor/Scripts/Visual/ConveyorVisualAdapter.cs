using Declarative;
using Elementary;

namespace Homework_Upgrades.Conveyor.Scripts.Visual
{
    public sealed class ConveyorVisualAdapter :
        IEnableListener,
        IDisableListener
    {
        private ITimer _workTimer;
        private ConveyorVisual _conveyor;

        public void Construct(ITimer workTimer, ConveyorVisual conveyor)
        {
            _workTimer = workTimer;
            _conveyor = conveyor;
        }

        void IEnableListener.OnEnable()
        {
            _workTimer.OnStarted += OnStartWork;
            _workTimer.OnFinished += OnFinishWork;
        }

        void IDisableListener.OnDisable()
        {
            _workTimer.OnStarted -= OnStartWork;
            _workTimer.OnFinished -= OnFinishWork;
        }

        private void OnStartWork()
        {
            _conveyor.Play();
        }

        private void OnFinishWork()
        {
            _conveyor.Stop();
        }
    }
}