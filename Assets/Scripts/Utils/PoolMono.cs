using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public sealed class PoolMono<T> where T : MonoBehaviour
    {
        private readonly T m_Prefab;
        private readonly Transform m_Container;
        private readonly Transform m_WorldTransform;

        private readonly Queue<T> m_Pool = new();
        private readonly HashSet<T> m_Actives = new();

        public PoolMono(T prefab, int count, Transform container, Transform worldTransform)
        {
            m_Prefab = prefab;
            m_Container = container;
            m_WorldTransform = worldTransform;
            
            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var createObject = CreateObject(m_Container);
                m_Pool.Enqueue(createObject);
            }
        }

        private T CreateObject(Transform container) => Object.Instantiate(m_Prefab, container);

        public T Get()
        {
            if (m_Pool.TryDequeue(out var obj))
                obj.transform.SetParent(m_WorldTransform);
            else
                obj = CreateObject(m_WorldTransform);

            m_Actives.Add(obj);

            return obj;
        }

        public void InactiveObject(T obj)
        {
            if (!m_Actives.Remove(obj)) return;
            
            obj.transform.SetParent(m_Container);
            this.m_Pool.Enqueue(obj);
        }
    }
}