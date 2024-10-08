namespace _EventBus.Scripts.Players.Components
{
    public struct HitPointsComponent
    {
        public int Value { get; private set; }

        public HitPointsComponent(int maxHealth)
        {
            Value = maxHealth;
        }

        public void ChangeHitPoints(int strength)
        {
            Value -= strength;
        }

        public bool IsDied() => Value <= 0;
    }
}