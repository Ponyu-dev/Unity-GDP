using System;
using System.Collections.Generic;
using System.Linq;
using SaveSystem.Base;

namespace GameEngine.Data
{
    [Serializable]
    public class ListUnitData : ISavableData
    {
        public List<UnitData> Units;

        public static ListUnitData Mapper(IEnumerable<Unit> units)
        {
            return new ListUnitData
            {
                Units = units.Select(u => new UnitData
                {
                    Name = u.name,
                    Type = u.Type,
                    HitPoints = u.HitPoints,
                    Position = new Vector3Data(u.Position),
                    Rotation = new Vector3Data(u.Rotation)
                }).ToList()
            };
        }
    }
    
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