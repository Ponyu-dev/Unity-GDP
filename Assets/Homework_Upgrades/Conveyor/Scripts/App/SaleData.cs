using System;
using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.App
{
    [Serializable]
    public sealed class SaleData
    {
        [SerializeField] public int sellingLumberAmount;
        [SerializeField] public int buyingCoinCurrency;
    }
}