using System.Collections.Generic;
using System.Linq;
using GameEngine.Data;
using SaveSystem.Base;
using UnityEngine;
using VContainer;
using VContainer.Unity;

//TODO не вынес в SaveSystem. Так как думаю потом SaveSystem допилить еще лучше и выложить в открытый доступ.
namespace GameEngine.Providers
{
    public class ResourcesDataProvider : IDataProvider<ISavableData>, IStartable
    {
        private readonly ResourceService _resourceService;

        [Inject]
        public ResourcesDataProvider(ResourceService resourceService)
        {
            Debug.Log("ResourcesDataProvider Constructor");
            _resourceService = resourceService;
        }

        public void Start()
        {
            Debug.Log("ResourcesDataProvider Start");
            _resourceService.SetResources(Object.FindObjectsOfType<Resource>());
        }

        public ISavableData GetDataForSaving()
        {
            Debug.Log("ResourcesDataProvider GetDataForSaving");
            return ResourcesData.Mapper(_resourceService.GetResources());
        }

        public void ApplyLoadedData(ISavableData data)
        {
            if (data is not ResourcesData resources) return;
            
            Debug.Log("ResourcesDataProvider ApplyLoadedData");
            ApplyResources(resources.resourcesData);
        }
        
        private void ApplyResources(List<ResourceData> resources)
        {
            foreach (var resourceData in resources)
            {
                var resource = _resourceService.GetResources().FirstOrDefault(r => r.ID == resourceData.ID);
                if (resource != null)
                {
                    resource.Amount = resourceData.Amount;
                }
                else
                {
                    Debug.LogWarning($"Resource with ID {resourceData.ID} not found.");
                }
            }
        }
    }
}