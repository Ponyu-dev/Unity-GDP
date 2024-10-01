using System.Collections.Generic;
using UnityEngine;

namespace _ChestMechanics.Chests.Data
{
    [CreateAssetMenu(menuName = "Chests/Config", fileName = "ChestsConfig", order = 0)]
    public class ChestsConfig : ScriptableObject
    {
        [SerializeField]
        private List<Chest> Chests;
    }
}