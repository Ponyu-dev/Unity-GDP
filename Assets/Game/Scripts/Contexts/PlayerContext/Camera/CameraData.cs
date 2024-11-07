using System;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Scripts.Contexts.PlayerContext
{
    [Serializable]
    public sealed class CameraData
    {
        public Transform transform;
        public float3 offset;
    }
}