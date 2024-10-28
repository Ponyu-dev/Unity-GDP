using System;
using Atomic.AI;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class MoveResourcesToBarnBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            var character = blackboard.GetCharacter();
            var characterStorage = character.GetComponent<ResourceStorageComponent>();

            var barn = blackboard.GetBarn();
            var barnStorage = barn.GetComponent<ResourceStorageComponent>();

            return MoveResources(characterStorage, barnStorage);
        }

        private static BTResult MoveResources(ResourceStorageComponent fromStorage, ResourceStorageComponent toStorage)
        {
            if (toStorage.FreeSlots == 0)
            {
                Debug.Log("[MoveResourcesToBarnBTNode] RUNNING", toStorage.gameObject);
                return BTResult.FAILURE;
            }

            var resourcesToAdd = Math.Min(toStorage.FreeSlots, fromStorage.Current);
            fromStorage.RemoveResources(resourcesToAdd);
            toStorage.AddResources(resourcesToAdd);
            
            Debug.Log("[MoveResourcesToBarnBTNode] SUCCESS", toStorage.gameObject);
            
            return BTResult.SUCCESS;
        }
    }
}