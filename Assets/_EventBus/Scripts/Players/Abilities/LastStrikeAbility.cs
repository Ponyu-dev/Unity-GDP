using _EventBus.Scripts.Players.Abilities.Base;

namespace _EventBus.Scripts.Players.Abilities
{
    //TODO надо как то через конфиг выставлять какой доп урон добавить сюда.
    //TODO ??? может быть прям тут надо делать все что связано с абилкой ??? 
    // В конце своего хода наносит рандомному противнику 3 ед урона.
    public class LastStrikeAbility : IAbilityTurnEnd
    {
        public readonly int Damage = 3;
    }
}