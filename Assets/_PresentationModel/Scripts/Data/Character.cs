using System;
using _PresentationModel.Scripts.LevelUp.Data;
using Popups;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public sealed class Character : PopupData
    {
        [SerializeField] public UserInfo userInfo;
        [SerializeField] public PlayerLevel playerLevel;
        [SerializeField] public CharacterInfo characterInfo;
        [SerializeField] public CharacterStat characterStat;

        public PlayerLevel PlayerLevel => playerLevel;
        
        public UserInfoData GetUserInfoData() => new UserInfoData(userInfo.Name, userInfo.Description, userInfo.Icon);
        public PlayerLevelData GetPlayerLevelData() => new PlayerLevelData(playerLevel.CurrentLevel, playerLevel.CurrentExperience, playerLevel.RequiredExperience);
    }
}