using System.Collections.Generic;
using System.Linq;
using GameEngine.Data;
using SaveSystem.Base;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameEngine.Providers
{
    public class ResourcesDataProvider : IDataProvider<ResourcesData>, IStartable
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

        public ResourcesData GetDataForSaving()
        {
            Debug.Log("ResourcesDataProvider GetDataForSaving");
            return ResourcesData.Mapper(_resourceService.GetResources());
        }

        public void ApplyLoadedData(ResourcesData data)
        {
            Debug.Log("ResourcesDataProvider ApplyLoadedData");
            ApplyResources(data.resourcesData);
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