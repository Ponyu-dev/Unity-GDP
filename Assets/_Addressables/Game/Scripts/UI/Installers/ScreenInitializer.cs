using System;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class ScreenInitializer<T> : IInitializable, IDisposable
    {
        private readonly bool _defaultActive;
        private readonly Transform _parent;
        private readonly DiContainer _container;
        private GameObject _screenPrefab;

        public ScreenInitializer(Transform parent, bool defaultActive, DiContainer container)
        {
            _container = container;
            _parent = parent;
            _defaultActive = defaultActive;
            UIAssetsService.OnAssetsLoaded += OnAssetsLoaded;
        }

        public void Initialize()
        {
            OnAssetsLoaded();
        }

        private void OnAssetsLoaded()
        {
            if (!_container.HasBindingId<GameObject>($"{typeof(T).Name}_GO"))
                return;
                
            _screenPrefab = _container.ResolveId<GameObject>($"{typeof(T).Name}_GO");
                
            if (_screenPrefab == null) return;
            _container.InstantiatePrefab(_screenPrefab, _parent).SetActive(_defaultActive);
        }

        public void Dispose()
        {
            UIAssetsService.OnAssetsLoaded -= OnAssetsLoaded;
        }
    }
}