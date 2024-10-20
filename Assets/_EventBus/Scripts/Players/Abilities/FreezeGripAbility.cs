using System;
using _EventBus.Scripts.Players.Abilities.Base;

namespace _EventBus.Scripts.Players.Abilities
{
    //Когда атакует противника, то замораживает его. Заморозка: замороженный герой пропускает свой ход 1 раз.
    [Serializable]
    public class FreezeGripAbility : IAbilityAttack
    {
        public readonly int CountTurnFreeze = 2;
    }
}