using Popups;
using System;
using System.Collections.Generic;
using _PresentationModel.Scripts.LevelUp.Data;
using _PresentationModel.Scripts.LevelUp.Views;
using _PresentationModel.Scripts.UI;
using Lessons.Architecture.PM;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace _PresentationModel.Scripts.LevelUp
{
    public sealed class LevelUpPresenter : PopupPresenter
    {
        private readonly StatView _statView;
            
        [Inject]
        public LevelUpPresenter(StatView statView)
        {
            _statView = statView;
            Debug.Log("[LevelUpPresenter] Constructor");
        }
        
        public override void Init(Type type, PopupView popupView, IPopupData popupData)
        {
            base.Init(type, popupView, popupData);
            
            if (popupView is not LevelUpView levelUpView) return;
            if (popupData is not CharacterData data) return;

            levelUpView.SetEnableBtnApply(data.Level.CanLevelUp());
            SetUserInfoData(data.UserInfo, levelUpView.userInfoView);
            SetPlayerLevelData(data.Level, levelUpView.playerLevelView);
            SetStats(data.Stats, levelUpView.statsContainer);
        }
        
        private void SetUserInfoData(UserInfo data, UserInfoView userInfoView)
        {
            userInfoView.SetName(data.Name);        
            userInfoView.SetDescription(data.Description);        
            userInfoView.SetIcon(data.Icon);        
        }

        private void SetPlayerLevelData(PlayerLevelData data, PlayerLevelView playerLevelView)
        {
            playerLevelView.SetCurrentLevel($"Level: {data.CurrentLevel}");
            playerLevelView.SetLevelProgress(data.ProgressExperience, data.StringExperience);
        }

        private void SetStats(IReadOnlyList<CharacterStat> statsData, Transform container)
        {
            Debug.Log($"[LevelUpPresenter] SetStats {statsData.Count}");
            foreach (var stat in statsData)
            {
                _statView.SetState(stat.ToString());
                Object.Instantiate(_statView, container);
            }
        }
    }
}