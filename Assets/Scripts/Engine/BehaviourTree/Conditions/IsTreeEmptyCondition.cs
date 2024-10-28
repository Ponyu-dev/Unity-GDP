using Atomic.AI;

namespace Game.Engine
{
    public class IsTreeEmptyCondition : IBlackboardCondition
    {
        public bool Invoke(IBlackboard blackboard)
        {
            return blackboard.TryGetTreeServices(out var treeServices) &&
                   !treeServices.FindClosest(blackboard.GetCharacter().transform.position, out var tree);
        }
    }
}