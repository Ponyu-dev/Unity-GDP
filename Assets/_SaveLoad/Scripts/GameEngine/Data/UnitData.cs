using System;

namespace GameEngine.Data
{
    [Serializable]
    public class UnitData
    {
        public string Name;
        public string Type;
        public int HitPoints;
        public Vector3Data Position;
        public Vector3Data Rotation;
    }
}