namespace _EventBus.Scripts.Players.Components
{
    public struct AttackComponent
    {
        public int AttackValue { get; private set; }

        public AttackComponent(int attackValue)
        {
            AttackValue = attackValue;
        }
    }
}