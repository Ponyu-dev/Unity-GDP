using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Helpers.Pools
{
    public sealed class PoolComponent : PoolObject<GameObject> 
    {
        public PoolComponent(GameObject prefab, Transform container, Transform worldTransform, bool autoExpand) 
            : base(prefab, container, worldTransform, autoExpand) { }

        public override GameObject CreateObject(Transform container = null, bool isEnqueue = false)
        {
            var obj = Object.Instantiate(Prefab, container == null ? Container : container);
            if (isEnqueue) Pool.Enqueue(obj);
            return obj;
        }

        public override void InactiveObject(GameObject obj)
        {
            base.InactiveObject(obj);
            obj.transform.SetParent(Container);
        }
    }
}