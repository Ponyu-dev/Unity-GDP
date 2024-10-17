using _ECS._RTS.Scripts.Components;
using UniRx;

namespace _ECS._RTS.Scripts.Reactivies
{
    public class ReactiveHealth
    {
        public ReactiveProperty<int> Current { get; }
        public ReactiveProperty<int> Max { get; }

        public ReactiveHealth(int initialCurrent, int initialMax)
        {
            Current = new ReactiveProperty<int>(initialCurrent);
            Max = new ReactiveProperty<int>(initialMax);
        }
        
        public void SyncFromEcs(Health ecsHealth)
        {
            Current.Value = ecsHealth.Current;
            Max.Value = ecsHealth.Max;
        }
        
        public void SyncToEcs(ref Health ecsHealth)
        {
            ecsHealth.Current = Current.Value;
            ecsHealth.Max = Max.Value;
        }
    }
}