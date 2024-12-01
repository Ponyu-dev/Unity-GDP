// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-30
// <file>: InventoryItemComponentEffectible.cs
// ------------------------------------------------------------------------------

using _InventorySystem.HeroStatsDebug.Scripts;

namespace _InventorySystem.Scripts.Item.Components
{
    public interface IInventoryItemComponentEffectible : IInventoryItemComponent
    {
        void EffectActive(HeroStats heroStats) { }
        void EffectUnActive(HeroStats heroStats) { }
    }
    
    public abstract class InventoryItemComponentEffectible : IInventoryItemComponentEffectible
    {
        public virtual void EffectActive(HeroStats heroStats) { }
        public virtual void EffectUnActive(HeroStats heroStats) { }
        public abstract IInventoryItemComponent Clone();
    }
}