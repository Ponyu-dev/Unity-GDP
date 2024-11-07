using System;
using System.Collections.Generic;
using Atomic.Elements;
using Game.Scripts.Contexts.Base.EntityPool;
using UnityEngine;

namespace Game.Scripts.Contexts.ZombieContext
{
    [Serializable]
    public sealed class ZombieSystemData
    {
        public IEntityPool pool;
        public IReadOnlyList<Bounds> spawnAreas;
        public Cycle spawnCycle;
    }
}