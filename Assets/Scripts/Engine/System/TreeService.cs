using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Tree = Game.Content.Tree;

namespace Game.Engine
{
    ///Содержит информацию о всех ресурсах на карте
    public sealed class TreeService : EventReceiver
    {
        private readonly Queue<GameObject> _pool = new();
        private readonly HashSet<GameObject> _actives = new();

        private void Awake()
        {
            var trees = GetComponentsInChildren<Transform>(true)
                .Where(child => child.CompareTag(GameObjectTags.Tree))
                .Select(child => child.gameObject)
                .ToArray();

            foreach (var tree in trees)
            {
                _pool.Enqueue(tree);
                tree.SetActive(false);
            }
            
            ActivateHalf();
        }

        private void ActivateHalf()
        {
            var halfCount = _pool.Count / 2;
            for (var i = 0; i < halfCount; i++)
            {
                var obj = _pool.Dequeue();
                Activate(obj);
            }
        }

        public override void OnEventTriggered()
        {
            if (_pool.Count <= 0) return;
            var nextObject = _pool.Dequeue();
            Activate(nextObject);
        }

        private void Activate(GameObject obj)
        {
            if (_actives.Contains(obj)) return;
            
            obj.SetActive(true);
            
            if (obj.TryGetComponent<Tree>(out var tree))
            {
                tree.OnStateInActived += Deactivate;
                tree.InitDefault();
            }
            
            _actives.Add(obj);
        }

        private void Deactivate(GameObject obj)
        {
            if (obj.TryGetComponent<Tree>(out var tree))
                tree.OnStateInActived -= Deactivate;
            
            obj.SetActive(false);
            
            if (_actives.Contains(obj))
                _actives.Remove(obj);
            
            _pool.Enqueue(obj);
        }

        public bool FindClosest(Vector3 position, out GameObject closestResource)
        {
            var minDistance = float.MaxValue;
            closestResource = null;

            foreach (var resource in _actives)
            {
                if (!resource.activeSelf)
                    continue;

                var resourcePosition = resource.transform.position;
                var distanceVector = resourcePosition - position;
                distanceVector.y = 0;

                var resourceDistance = distanceVector.sqrMagnitude;
                if (!(resourceDistance < minDistance)) continue;
                minDistance = resourceDistance;
                closestResource = resource;
            }

            return closestResource != null;
        }
    }
}