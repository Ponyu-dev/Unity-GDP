using System.Collections.Generic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class TreeServices : MonoBehaviour
    {
        public IReadOnlyList<TreeService> TreeServicesArray => this.treeServices;

        [SerializeField] private TreeService[] treeServices;

        public bool FindClosest(Vector3 position, out GameObject closestResource)
        {
            closestResource = default;
            var isFind = false;
            for (int i = 0, count = TreeServicesArray.Count; i < count; i++)
            {
                if (TreeServicesArray[i].FindClosest(position, out closestResource))
                {
                    isFind = true;
                    break;
                }
            }

            return isFind;
        }
    }
}