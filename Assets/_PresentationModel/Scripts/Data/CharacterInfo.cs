using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public struct CharacterInfo
    {
        [ShowInInspector]
        public List<CharacterStat> Stats { get; private set; }

        public CharacterStat GetStat(string name)
        {
            foreach (var stat in this.Stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public CharacterStat[] GetStats()
        {
            return this.Stats.ToArray();
        }
    }
}