using Atomic.AI;
using UnityEngine;

namespace Game.Engine.HarvesterWood
{
    public sealed class HarvestTreeBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            var harvestComponent = blackboard.GetCharacter().GetComponent<HarvestComponent>();
            var resourceStorageComponent = blackboard.GetCharacter().GetComponent<ResourceStorageComponent>();

            var tree = blackboard.GetTargetTree();

            if (!tree.activeInHierarchy && resourceStorageComponent.IsNotFull())
            {
                Debug.Log("[HarvestTreeBTNode] FAILURE", tree.gameObject);
                blackboard.DelTargetTree();
                return BTResult.FAILURE;
            }

            if (!resourceStorageComponent.IsNotFull() || !tree.activeInHierarchy)
            {
                Debug.Log("[HarvestTreeBTNode] SUCCESS", tree.gameObject);
                return BTResult.SUCCESS;
            }

            Debug.Log("[HarvestTreeBTNode] RUNNING", tree.gameObject);
            harvestComponent.StartHarvest();
            return BTResult.RUNNING;
        }
    }
}