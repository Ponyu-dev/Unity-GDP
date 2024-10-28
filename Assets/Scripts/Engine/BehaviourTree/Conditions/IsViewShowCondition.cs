using Atomic.AI;
using UnityEngine;

namespace Game.Engine
{
    public class IsViewShowCondition : IBlackboardCondition
    {
        [SerializeField, BlackboardKey] private int _view;

        public bool Invoke(IBlackboard blackboard)
        {
            var view = blackboard.GetObject<GameObject>(_view);
            
            return view.activeSelf;
        }
    }
}