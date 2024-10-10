using _EventBus.Scripts.Players.Abilities.Base;

namespace _EventBus.Scripts.Players.Abilities
{
    //В конце каждого хода героя игрока добавляет 1 ед здоровья рандомному союзнику.
    public class HealingGiftAbility : IAbility
    {
        public readonly int HealingAmount = 1;
    }
}