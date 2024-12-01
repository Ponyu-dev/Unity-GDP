// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-12-01
// <file>: IStatComponent.cs
// ------------------------------------------------------------------------------

namespace _InventorySystem.HeroStatsDebug.Scripts.Components
{
    public interface IStatComponent
    {
        void UpdateValue(int value);
        int GetValue();
    }
}