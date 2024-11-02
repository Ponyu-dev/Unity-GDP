using Declarative;
using Elementary;

namespace Homework_Upgrades.Conveyor.Scripts.Visual.Zone
{
    public sealed class ZoneVisualAdapter :
        IAwakeListener,
        IEnableListener,
        IDisableListener
    {
        private IVariable<int> _storage;
        private ZoneVisual _visualZone;

        public void Construct(IVariable<int> storage, ZoneVisual visualZone)
        {
            _storage = storage;
            _visualZone = visualZone;
        }

        void IAwakeListener.Awake()
        {
            _visualZone.SetupItems(_storage.Current);
        }

        void IEnableListener.OnEnable()
        {
            _storage.OnValueChanged += OnItemsChanged;
        }

        void IDisableListener.OnDisable()
        {
            _storage.OnValueChanged -= OnItemsChanged;
        }

        private void OnItemsChanged(int count)
        {
            _visualZone.SetupItems(count);
        }
    }
}