using System;
using System.Collections.Generic;

namespace _ECS_RTS.Scripts.EcsEngine.Helpers
{
    [Serializable]
    public class CustomTypePair<TType, TPair>
    {
        public TType Key;
        public TPair Value;
        
        public static Dictionary<TType, TPair> ConvertPrefabs(IEnumerable<CustomTypePair<TType, TPair>> list)
        {
            var prefabsDictionary = new Dictionary<TType, TPair>();
            foreach (var pair in list)
            {
                prefabsDictionary[pair.Key] = pair.Value;
            }

            return prefabsDictionary;
        }
    }
}