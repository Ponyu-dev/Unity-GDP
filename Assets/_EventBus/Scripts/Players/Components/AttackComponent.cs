namespace _EventBus.Scripts.Players.Components
{
    public sealed class AttackComponent
    {
        public int Value { get; private set; }

        public AttackComponent(int value)
        {
            Value = value;
        }
    }
}