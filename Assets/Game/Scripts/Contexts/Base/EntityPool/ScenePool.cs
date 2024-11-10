using System.Collections.Generic;
using System.Linq;
using Atomic.Entities;
using UnityEngine;

namespace Game.Scripts.Contexts.Base.EntityPool
{
    public sealed class ScenePool : IEntityPool
    {
        private readonly SceneEntity _prefab;

        private readonly Transform _worldContainer;
        private readonly Transform _poolContainer;

        private readonly Queue<SceneEntity> _queue = new();
        private readonly HashSet<IEntity> _active = new();

        public IReadOnlyList<IEntity> GetActives() => _active.ToList();
        public int CountActives() => _active.Count;

        public ScenePool(
            SceneEntity prefab,
            Transform poolContainer,
            Transform worldContainer,
            int initialCount = 0
        )
        {
            _prefab = prefab;
            _poolContainer = poolContainer;
            _worldContainer = worldContainer;

            for (var i = 0; i < initialCount; i++)
            {
                var entity = SceneEntity.Instantiate(_prefab, _poolContainer);
                _queue.Enqueue(entity);
            }
        }

        public IEntity Rent()
        {
            if (_queue.TryDequeue(out var entity))
                entity.transform.SetParent(_worldContainer);
            else
                entity = SceneEntity.Instantiate(_prefab, _worldContainer);
            
            _active.Add(entity);
            
            return entity;
        }

        public void Return(IEntity entity)
        {
            var sceneEntity = SceneEntity.Cast(entity);
            sceneEntity.transform.SetParent(_poolContainer);
            _active.Remove(sceneEntity);
            _queue.Enqueue(sceneEntity);
        }
    }
}