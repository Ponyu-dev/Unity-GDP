using Atomic.AI;
using UnityEngine;

namespace Game.Engine.HarvesterWood
{
    public sealed class FindClosestTreeBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetTreeServices(out var treeServices))
                return BTResult.FAILURE;
            
            var character = blackboard.GetCharacter();

            if (!treeServices.FindClosest(character.transform.position, out var target))
            {
                blackboard.DelTargetTree();
                return BTResult.FAILURE;
            }
            
            if (target == null)
                return BTResult.FAILURE;
            
            blackboard.SetTargetTree(target);
            
            return BTResult.SUCCESS;
        }
    }
}