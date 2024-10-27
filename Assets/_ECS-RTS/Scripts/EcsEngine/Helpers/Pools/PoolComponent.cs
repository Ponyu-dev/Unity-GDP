using _ECS_RTS.Scripts.EcsEngine.Views;
using UnityEngine;
using VContainer;

namespace _ECS_RTS.Scripts.EcsEngine.Helpers.Pools
{
    public sealed class PoolComponent : PoolObject<GameObject> 
    {
        private readonly IObjectResolver _resolver;
        
        public PoolComponent(IObjectResolver resolver, GameObject prefab, Transform container, Transform worldTransform,
            bool autoExpand)
            : base(prefab, container, worldTransform, autoExpand)
        {
            _resolver = resolver;
        }

        public override GameObject CreateObject(Transform container = null, bool isEnqueue = false)
        {
            var obj = Object.Instantiate(Prefab, container == null ? Container : container);
            if (obj.TryGetComponent<CollisionComponentView>(out var componentView))
                _resolver.Inject(componentView);

            if (isEnqueue) Pool.Enqueue(obj);
            return obj;
        }

        public void ActiveObject(GameObject obj)
        {
            if (obj.TryGetComponent<CollisionComponentView>(out var componentView))
                componentView.OnCollisionEntered += InactiveObject;
            obj.transform.SetParent(WorldTransform);
        }

        public override void InactiveObject(GameObject obj)
        {
            base.InactiveObject(obj);
            if (obj.TryGetComponent<CollisionComponentView>(out var componentView))
                componentView.OnCollisionEntered -= InactiveObject;
            obj.transform.SetParent(Container);
        }
    }
}