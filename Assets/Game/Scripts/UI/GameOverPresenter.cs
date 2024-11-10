using System;
using Atomic.Elements;
using Atomic.Entities;
using Atomic.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    [Serializable]
    public sealed class GameOverPresenter : IViewInit, IViewDispose
    {
        [SerializeField] private SceneEntity player;
        [SerializeField] private Button btnRestart;
        [SerializeField] private GameObject gameOver;

        [ShowInInspector, ReadOnly] private IReactiveVariable<bool> _isDead;
        
        public void Init()
        {
            _isDead = player.GetIsDead();
            gameOver.SetActive(false);
            _isDead.Subscribe(OnPlayerDead);
            btnRestart.onClick.AddListener(OnClickRestart);
        }

        private void OnClickRestart()
        {
            _isDead.Invoke(false);
        }

        private void OnPlayerDead(bool isDead)
        {
            gameOver.SetActive(isDead);
        }

        public void Dispose()
        {
            btnRestart.onClick.RemoveListener(OnClickRestart);
            _isDead.Unsubscribe(OnPlayerDead);
        }
    }
}