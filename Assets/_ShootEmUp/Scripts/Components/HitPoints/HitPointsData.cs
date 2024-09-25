using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class HitPointsData
    {
        [SerializeField]
        private int hitPoints;

        public int HitPoints() => hitPoints;
    }
}