using System;
using Atomic.AI;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class SetActiveDialogBTNode : IBlackboardAction
    {
        [SerializeField, BlackboardKey] private int gameObjectKey;
        [SerializeField] private bool isActive;
        
        public void Invoke(IBlackboard blackboard)
        {
            var gameObject = blackboard.GetObject<GameObject>(gameObjectKey);
            gameObject.SetActive(isActive);
        }
    }
}