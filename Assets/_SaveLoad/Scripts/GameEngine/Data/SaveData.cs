using System;
using System.Collections.Generic;

namespace GameEngine.Data
{
    [Serializable]
    public class SaveData
    {
        public List<ResourceData> Resources;
        public List<UnitData> Units;
    }
}