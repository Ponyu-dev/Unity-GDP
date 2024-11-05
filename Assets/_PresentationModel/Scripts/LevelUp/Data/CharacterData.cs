using System.Collections.Generic;
using Lessons.Architecture.PM;
using Popups;

namespace _PresentationModel.Scripts.LevelUp.Data
{
    public sealed class CharacterData : IPopupData
    {
        public CharacterData(UserInfo userInfo, PlayerLevelData level, IReadOnlyList<CharacterStat> statsData)
        {
            UserInfo = userInfo;
            Level = level;
            Stats = statsData;
        }

        public UserInfo UserInfo { get; private set; }
        public PlayerLevelData Level { get; private set; }
        public IReadOnlyList<CharacterStat> Stats { get; private set; }
    }
}