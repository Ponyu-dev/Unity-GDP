using UnityEngine;

namespace _CubeECS.Scripts.ECS.Utils
{
    public abstract class AbstractCollider : MonoBehaviour
    {
        public int EntityId { get; private set; }

        public void SetEntityId(int entityId)
        {
            EntityId = entityId;
        }
    }
}