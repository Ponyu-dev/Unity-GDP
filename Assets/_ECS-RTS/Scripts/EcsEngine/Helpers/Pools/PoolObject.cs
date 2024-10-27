using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Helpers.Pools
{
    public abstract class PoolObject<T> where T : Object
    {
        protected readonly T Prefab;
        protected readonly Transform Container;
        protected readonly Transform WorldTransform;
        protected readonly bool AutoExpand;

        protected Queue<T> Pool = new();
        protected readonly HashSet<T> Actives = new();

        public int ActivesCount() => Actives.Count;

        protected PoolObject(T prefab, Transform container, Transform worldTransform, bool autoExpand)
        {
            Prefab = prefab;
            Container = container;
            WorldTransform = worldTransform;
            AutoExpand = autoExpand;
        }

        public abstract T CreateObject(Transform container = null, bool isEnqueue = false);

        public bool TryGet(out T result)
        {
            if (!Pool.TryDequeue(out var obj) && AutoExpand)
                obj = CreateObject(WorldTransform);

            result = obj;
            if (result == null) return false;
            
            Actives.Add(obj);

            return true;
        }

        public T TryGetActive(int id)
        {
            return Actives.Count > 0 ? Actives?.ElementAt(index: id) : null;
        }

        public virtual void InactiveObject(T obj)
        {
            if (!Actives.Remove(obj)) return;
            Pool.Enqueue(obj);
        }
        
        public virtual void ClearPool()
        {
            foreach (var activeObject in Actives)
            {
                Object.Destroy(activeObject);
            }
            Actives.Clear();
            while (Pool.Count > 0)
            {
                var pooledObject = Pool.Dequeue();
                Object.Destroy(pooledObject);
            }
        }
    }
}