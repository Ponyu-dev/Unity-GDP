using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Data
{
    [Serializable]
    public class ResourcesData
    {
        public List<ResourceData> resourcesData;
        
        public static ResourcesData Mapper(IEnumerable<Resource> resources)
        {
            return new ResourcesData
            {
                resourcesData = resources.Select(r => new ResourceData
                {
                    ID = r.ID,
                    Amount = r.Amount
                }).ToList()
            };
        }
    }
    
    [Serializable]
    public class ResourceData
    {
        public string ID;
        public int Amount;
    }
}