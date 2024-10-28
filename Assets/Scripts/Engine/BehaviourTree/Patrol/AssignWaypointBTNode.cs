using System;
using Atomic.AI;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class AssignWaypointBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetWaypoints(out Transform[] waypoints))
            {
                Debug.Log("[AssignWaypointBTNode] FAILURE");
                return BTResult.FAILURE;
            }
            
            int index = blackboard.GetWaypointIndex();
            GameObject targetWaypoint = waypoints[index].gameObject;
            
            blackboard.SetTarget(targetWaypoint);
            Debug.Log("[AssignWaypointBTNode] SUCCESS", targetWaypoint);
            return BTResult.SUCCESS;
        }
    }
}