using Popups;
using System;
using Lessons.Architecture.PM;
using UnityEngine;
using VContainer;

namespace _PresentationModel.Scripts.LevelUp
{
    public sealed class LevelUpPresenter : PopupPresenter
    {
        private PlayerLevel _playerLevel;
        
        [Inject]
        public LevelUpPresenter()
        {
            Debug.Log("[LevelUpPresenter] Constructor");
        }
        
        public override void Init(Type type, PopupView popupView, PopupData popupData)
        {
            base.Init(type, popupView, popupData);
            
            if (popupView is not LevelUpView levelUpView) return;
            if (popupData is not Character data) return;

            var userInfo = data.GetUserInfoData();
            levelUpView.SetUserInfoData(userInfo.Name, userInfo.Description, userInfo.Icon);

            var levelData = data.GetPlayerLevelData();
            _playerLevel = data.PlayerLevel;
            levelUpView.SetPlayerLevelData($"Level: {levelData.CurrentLevel}", levelData.ProgressExperience, levelData.StringExperience);
        }

        public override void OnApplyClicked()
        {
            base.OnApplyClicked();
            _playerLevel.LevelUp();
        }
    }
}