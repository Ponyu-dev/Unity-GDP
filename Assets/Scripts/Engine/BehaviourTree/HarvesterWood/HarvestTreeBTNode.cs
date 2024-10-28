using Atomic.AI;

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
                return BTResult.FAILURE;

            if (!resourceStorageComponent.IsNotFull() || !tree.activeInHierarchy)
                return BTResult.SUCCESS;
            
            harvestComponent.StartHarvest();
            return BTResult.RUNNING;
        }
    }
}