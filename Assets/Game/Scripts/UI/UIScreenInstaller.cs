using System.Collections.Generic;
using Atomic.UI;
using Atomic.UI.Installer;
using UnityEngine;

namespace Game.Scripts.UI
{
    public sealed class UIScreenInstaller : SceneViewControllerInstaller
    {
        [SerializeField] private GameOverPresenter gameOverPresenter;
        [SerializeField] private LifePresenter lifePresenter;
        [SerializeField] private AmmoPresenter ammoPresenter;
        
        protected override IEnumerable<IViewController> GetControllers()
        {
            yield return gameOverPresenter;
            yield return lifePresenter;
            yield return ammoPresenter;
        }
    }
}