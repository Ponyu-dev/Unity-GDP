using UnityEngine;

namespace _PresentationModel.Scripts.LevelUp.Data
{
    public struct UserInfoData
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite Icon { get; private set; }
        
        public UserInfoData(string name, string description, Sprite icon)
        {
            Name = name;
            Description = description;
            Icon = icon;
        }
    }
}