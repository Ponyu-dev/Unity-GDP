using System;
using _PresentationModel.Scripts.LevelUp;
using Lessons.Architecture.PM;
using Popups;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _PresentationModel.Scripts.HeroInfo
{
    public sealed class HeroInfoPresenter : IStartable, IDisposable
    {
        private readonly Character _character;
        private readonly HeroInfoView _heroInfoView;
        private readonly IPopupFactory _popupFactory;

        [Inject]
        public HeroInfoPresenter(Character character, HeroInfoView heroInfoView, IPopupFactory popupFactory)
        {
            _heroInfoView = heroInfoView;
            _character = character;
            _popupFactory = popupFactory;
        }

        public void Start()
        {
            _character.PlayerLevel.OnLevelUp += OnLevelUp;
            _heroInfoView.OnShowClicked += ShowPopup;
            _heroInfoView.SetName(_character.UserInfo.Name);
            _heroInfoView.SetIcon(_character.UserInfo.Icon);
        }

        private void OnLevelUp()
        {
            var increasePerLevel = _character.IncreasePerLevel;
            var level = _character.PlayerLevel.CurrentLevel;
            Debug.Log($"OnLevelUp {level}");

            foreach (var stat in _character.CharacterInfo.GetStats())
            {
                var newValue = stat.Value + level * increasePerLevel;
                stat.ChangeValue(newValue);
            }
        }

        private void ShowPopup()
        {
            _popupFactory.OnHideFinished += OnHideFinish;
            _popupFactory.CanShowPopup<LevelUpView, LevelUpPresenter>(_character.ConvertToData());
        }

        private void OnHideFinish(bool apply)
        {
            _popupFactory.OnHideFinished -= OnHideFinish;
            
            if (apply)
                _character.PlayerLevel.LevelUp();
        }

        public void Dispose()
        {
            _heroInfoView.OnShowClicked -= ShowPopup;
            _character.PlayerLevel.OnLevelUp -= OnLevelUp;
        }
    }
}