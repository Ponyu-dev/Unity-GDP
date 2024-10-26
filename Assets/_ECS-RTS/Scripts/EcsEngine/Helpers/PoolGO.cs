using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public sealed class PoolGO
    {
        private readonly GameObject m_Prefab;
        private readonly Transform m_Container;
        private readonly Transform m_WorldTransform;
        private readonly bool m_AutoExpand;

        private readonly Queue<GameObject> m_Pool = new();
        private readonly HashSet<GameObject> m_Actives = new();

        public IReadOnlyCollection<GameObject> Actives => new HashSet<GameObject>(m_Actives);

        public PoolGO(GameObject prefab, Transform container, Transform worldTransform, bool autoExpand)
        {
            m_Prefab = prefab;
            m_Container = container;
            m_WorldTransform = worldTransform;
            m_AutoExpand = autoExpand;
        }

        public void CreatePool(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var createObject = CreateObject(m_Container);
                m_Pool.Enqueue(createObject);
            }
        }

        private GameObject CreateObject(Transform container) => Object.Instantiate(m_Prefab, container);

        public bool TryGet(out GameObject result)
        {
            if (m_Pool.TryDequeue(out var obj))
                obj.transform.SetParent(m_WorldTransform);
            else if (m_AutoExpand)
                obj = CreateObject(m_WorldTransform);

            result = obj;
            if (result == null) return false;
            
            m_Actives.Add(obj);

            return true;
        }

        public GameObject TryGetActive(int id)
        {
            return m_Actives.Count > 0 ? m_Actives?.ElementAt(index: id) : null;
        }

        public void InactiveObject(GameObject obj)
        {
            if (!m_Actives.Remove(obj)) return;
            
            obj.transform.SetParent(m_Container);
            this.m_Pool.Enqueue(obj);
        }
        
        public void ClearPool()
        {
            foreach (var activeObject in m_Actives)
            {
                Object.Destroy(activeObject.gameObject);
            }
            m_Actives.Clear();
            while (m_Pool.Count > 0)
            {
                var pooledObject = m_Pool.Dequeue();
                Object.Destroy(pooledObject.gameObject);
            }
        }
    }
}