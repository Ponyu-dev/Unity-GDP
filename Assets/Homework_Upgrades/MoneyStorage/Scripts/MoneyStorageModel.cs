using System;
using Declarative;
using Game.GamePlay.Upgrades;
using UnityEngine;

namespace Homework_Upgrades.MoneyStorage.Scripts
{
    public sealed class MoneyStorageModel : DeclarativeModel
    {
        [Section, SerializeField, Space]
        public Core core;
        
        [Section, SerializeField, Space]
        public Canvas canvas;
    }

    [Serializable]
    public sealed class Core
    {
        [Section, SerializeField]
        public MoneyStorage moneyStorage = new();
    }

    [Serializable]
    public sealed class Canvas
    {
        [SerializeField] public MoneyWidgetView moneyWidgetView;
        private readonly MoneyWidgetAdapter _moneyWidgetAdapter = new();
        
        [Construct]
        private void Construct(Core core)
        {
            _moneyWidgetAdapter.Constructor(core.moneyStorage, moneyWidgetView);
        }
    }
}