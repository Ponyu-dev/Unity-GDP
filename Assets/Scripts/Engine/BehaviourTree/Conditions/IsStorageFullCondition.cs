using Atomic.AI;
using UnityEngine;

namespace Game.Engine
{
    public class IsStorageFullCondition : IBlackboardCondition
    {
        [SerializeField, BlackboardKey] private int _storageObject;

        public bool Invoke(IBlackboard blackboard)
        {
            var objectWithStorage = blackboard.GetObject<GameObject>(_storageObject);
            var storage = objectWithStorage.GetComponent<ResourceStorageComponent>();
            
            return storage.IsFull();
        }
    }
}