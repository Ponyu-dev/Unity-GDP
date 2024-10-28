using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Addressables.Game.Scripts.Gameplay.Services
{
    public interface IAddressablesService
    {
        Task<GameObject> SpawnAsync(string address, Transform parent = null);
        void Despawn(GameObject obj);
    }
    
    public sealed class AddressablesService : IAddressablesService
    {
        private readonly List<GameObject> _spawnedObjects = new List<GameObject>();

        public async Task<GameObject> SpawnAsync(string address, Transform parent = null)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(address);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var instance = Object.Instantiate(handle.Result, parent);
                _spawnedObjects.Add(instance);
                return instance;
            }

            Debug.LogError($"Failed to load object at address: {address}");
            return null;
        }

        public void Despawn(GameObject obj)
        {
            if (_spawnedObjects.Contains(obj))
            {
                _spawnedObjects.Remove(obj);
                Addressables.ReleaseInstance(obj);
            }
            else
            {
                Debug.LogWarning("Object not managed by AddressablesService.");
            }
        }
        
        public void Clear()
        {
            foreach (var obj in _spawnedObjects)
            {
                Addressables.ReleaseInstance(obj);
            }
            _spawnedObjects.Clear();
        }
    }
}