using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace SampleGame
{
    public interface ISceneLoader
    {
        event Action SceneLoaded;
        void LoadNewScene(string newSceneAddress);
    }

    public class SceneLoader : ISceneLoader
    {
        // Holds the handle of the currently loaded scene, if any
        private AsyncOperationHandle<SceneInstance>? _currentSceneHandle;

        // Stores the address of the scene to be loaded
        private string _sceneToLoadAddress;

        // Event to notify when the scene is loaded
        public event Action SceneLoaded;

        /// <summary>
        /// Loads a new scene, and if a scene is currently loaded, it will unload it first.
        /// </summary>
        /// <param name="newSceneAddress">Address of the new scene to load</param>
        public async void LoadNewScene(string newSceneAddress)
        {
            _sceneToLoadAddress = newSceneAddress;

            // Unload the current scene if it's loaded
            if (_currentSceneHandle.HasValue)
            {
                await UnloadCurrentScene();
            }

            // Load the new scene
            await LoadSceneInternal();
        }

        /// <summary>
        /// Unloads the current scene asynchronously.
        /// </summary>
        private async UniTask UnloadCurrentScene()
        {
            if (_currentSceneHandle.HasValue)
            {
                // Unload the current scene
                await Addressables.UnloadSceneAsync(_currentSceneHandle.Value).Task;

                // Reset the current scene handle since it's now unloaded
                _currentSceneHandle = null;
                Debug.Log("Previous scene unloaded successfully.");
            }
        }

        /// <summary>
        /// Internal method for loading the new scene asynchronously.
        /// </summary>
        private async UniTask LoadSceneInternal()
        {
            // Load the scene using Addressables and wait for completion
            var handle =
                Addressables.LoadSceneAsync(_sceneToLoadAddress, UnityEngine.SceneManagement.LoadSceneMode.Single);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                // Store the handle of the currently loaded scene
                _currentSceneHandle = handle;
                Debug.Log($"Scene '{_sceneToLoadAddress}' loaded successfully.");

                // Trigger the SceneLoaded event if there are any subscribers
                SceneLoaded?.Invoke();
            }
            else
            {
                Debug.LogError($"Failed to load scene: {_sceneToLoadAddress}");
            }
        }
    }
}