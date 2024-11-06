using System.Collections.Generic;
using Atomic.Entities;
using UnityEngine;

namespace Game.Scripts.Contexts.GameContext.EntityPool
{
    public sealed class SceneEntityPool : IEntityPool
    {
        private readonly SceneEntity _prefab;

        private readonly Transform _worldContainer;
        private readonly Transform _poolContainer;

        private readonly Queue<SceneEntity> _queue = new();

        public SceneEntityPool(
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
            {
                entity.transform.SetParent(_worldContainer);
                return entity;
            }

            return SceneEntity.Instantiate(_prefab, _worldContainer);
        }

        public void Return(IEntity entity)
        {
            var sceneEntity = SceneEntity.Cast(entity);
            sceneEntity.transform.SetParent(_poolContainer);
            _queue.Enqueue(sceneEntity);
        }
    }
}