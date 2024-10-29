using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace SampleGame
{
    public sealed class UIAssetsService : IInitializable
    {
        public delegate void AssetsLoadedHandler();
        public static event AssetsLoadedHandler OnAssetsLoaded;
        
        private readonly DiContainer _container;
        private readonly AssetReference _menuScreenReference;
        private readonly AssetReference _pauseScreenReference;

        public UIAssetsService(
            DiContainer container,
            [Inject(Id = "MenuScreen")] AssetReference menuScreenReference,
            [Inject(Id = "PauseScreen")] AssetReference pauseScreenReference)
        {
            _container = container;
            _menuScreenReference = menuScreenReference;
            _pauseScreenReference = pauseScreenReference;
        }

        public void Initialize()
        {
            _menuScreenReference.LoadAssetAsync<GameObject>().Completed += OnMenuScreenLoaded;
            _pauseScreenReference.LoadAssetAsync<GameObject>().Completed += OnPauseScreenLoaded;
        }

        private void OnMenuScreenLoaded(AsyncOperationHandle<GameObject> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                _container.BindInstance(obj.Result)
                    .WithId($"{nameof(MenuScreen)}_GO")
                    .AsCached()
                    .NonLazy();
                CheckIfAllAssetsLoaded();
                Debug.Log("MenuScreen loaded successfully.");
            }
            else
            {
                Debug.LogError("Failed to load MenuScreen.");
            }
        }

        private void OnPauseScreenLoaded(AsyncOperationHandle<GameObject> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                _container.BindInstance(obj.Result)
                    .WithId($"{nameof(PauseScreen)}_GO")
                    .AsCached()
                    .NonLazy();
                CheckIfAllAssetsLoaded();
                Debug.Log("PauseScreen loaded successfully.");
            }
            else
            {
                Debug.LogError("Failed to load PauseScreen.");
            }
        }
        
        private void CheckIfAllAssetsLoaded()
        {
            var hasBindingIdMenuScreen = _container.HasBindingId<GameObject>($"{nameof(MenuScreen)}_GO");
            var hasBindingIdPauseScreen = _container.HasBindingId<GameObject>($"{nameof(PauseScreen)}_GO");
            if (hasBindingIdMenuScreen && hasBindingIdPauseScreen)
                OnAssetsLoaded?.Invoke();
        }
    }
}