using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class TeamData
    {
        [SerializeField] private bool isPlayer;
        public bool IsPlayer => this.isPlayer;
    }
}