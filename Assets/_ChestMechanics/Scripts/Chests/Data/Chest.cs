using System;
using _ChestMechanics.Chests.Enums;
using UnityEngine;

namespace _ChestMechanics.Chests.Data
{
    [Serializable]
    public class Chest
    {
        [SerializeField] 
        private ChestType chestType;
        public ChestType TypeChest => chestType;

        [SerializeField]
        private float unlockTime;
        public float UnlockTime => unlockTime;
        
        /*
        [SerializeField]
        private UnlockType unlockType;
        public UnlockType TypeUnlock => unlockType;*/

        [SerializeField]
        private GameObject prefab;
        public GameObject Prefab => prefab;
    }
}