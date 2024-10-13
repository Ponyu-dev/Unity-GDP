namespace _EventBus.Scripts.Players.Components
{
    public class FreezeDebuffComponent
    {
        public int CountTurn { get; private set; }

        public FreezeDebuffComponent(int countTurn)
        {
            CountTurn = countTurn;
        }
        
        public bool ProcessTurn()
        {
            CountTurn -= 1;
            return CountTurn <= 0;
        }
    }
}