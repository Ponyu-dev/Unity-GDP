using System.Collections.Generic;
using UnityEngine;

namespace GameEngine
{
    [CreateAssetMenu(fileName = "UnitPrefabs", menuName = "GameEngine/UnitPrefabs")]
    public class UnitPrefabs : ScriptableObject
    {
        [SerializeField]
        private List<Unit> unitPrefabEntries;

        public Unit GetPrefabByType(string type)
        {
            var entry = unitPrefabEntries.Find(e => e.Type == type);
            return entry;
        }
    }
}