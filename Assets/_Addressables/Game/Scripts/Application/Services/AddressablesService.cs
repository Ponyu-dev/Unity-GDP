using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SampleGame
{
    public interface IAddressablesService
    {
        Task<GameObject> SpawnAsync(string address, Transform parent = null);
        void Clear();
    }
    
    public sealed class AddressablesService : IAddressablesService
    {
        private readonly List<AsyncOperationHandle> _spawnedObjects = new List<AsyncOperationHandle>();

        public async Task<GameObject> SpawnAsync(string address, Transform parent = null)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(address);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var instance = Object.Instantiate(handle.Result, parent);
                _spawnedObjects.Add(handle);
                return instance;
            }

            Debug.LogError($"Failed to load object at address: {address}");
            return null;
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