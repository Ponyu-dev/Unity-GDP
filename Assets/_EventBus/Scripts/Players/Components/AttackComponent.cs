namespace _EventBus.Scripts.Players.Components
{
    public sealed class AttackComponent
    {
        public int AttackValue { get; private set; }

        public AttackComponent(int attackValue)
        {
            AttackValue = attackValue;
        }
    }
}