using Atomic.Elements;
using Declarative;
using UnityEngine;

public class ConveyorModel : DeclarativeModel
{
    public AtomicVariable<int> LoadStorageCapacity;
    public AtomicVariable<int> UnloadStorageCapacity;
    public AtomicVariable<float> ProduceTime;
}
    
