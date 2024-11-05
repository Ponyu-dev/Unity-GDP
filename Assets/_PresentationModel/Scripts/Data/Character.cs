using _PresentationModel.Scripts.LevelUp.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    [CreateAssetMenu(menuName = "Characters/New CharacterData", fileName = "CharacterData")]
    public sealed class Character : ScriptableObject
    {
        [ShowInInspector] public int IncreasePerLevel { get; private set; }
        [ShowInInspector] public UserInfo UserInfo { get; private set; }
        [ShowInInspector] public PlayerLevel PlayerLevel  { get; private set; }
        [ShowInInspector] public CharacterInfo CharacterInfo  { get; private set; }
        
        public CharacterData ConvertToData()
        {
            var level = new PlayerLevelData(PlayerLevel.CurrentLevel, PlayerLevel.CurrentExperience, PlayerLevel.RequiredExperience);
            
            return new CharacterData(UserInfo, level, CharacterInfo.GetStats());
        }
    }
}