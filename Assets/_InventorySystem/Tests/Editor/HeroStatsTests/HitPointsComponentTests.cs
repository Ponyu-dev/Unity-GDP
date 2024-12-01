// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-12-01
// <file>: HitPointsComponentTests.cs
// ------------------------------------------------------------------------------

using _InventorySystem.HeroStatsDebug.Scripts;
using _InventorySystem.HeroStatsDebug.Scripts.Components;
using NUnit.Framework;

namespace _InventorySystem.Tests.Editor.HeroStatsTests
{
    [TestFixture]
    public class HitPointsComponentTests
    {
        [Test]
        public void Constructor_ShouldClampHitPointsWithinMaxHitPoints()
        {
            var hpComponent = new HitPointsComponent(150, 100);

            Assert.AreEqual(100, hpComponent.MaxHitPoints, "MaxHitPoints should be initialized correctly.");
            Assert.AreEqual(100, hpComponent.CurrentHitPoints, "CurrentHitPoints should be clamped to MaxHitPoints.");
        }

        [Test]
        public void UpdateMaxHp_ShouldIncreaseMaxHpAndNotAffectHitPoints()
        {
            var hpComponent = new HitPointsComponent(50, 100);

            hpComponent.UpdateMaxHp(50);

            Assert.AreEqual(150, hpComponent.MaxHitPoints, "MaxHitPoints should increase by the given value.");
            Assert.AreEqual(50, hpComponent.CurrentHitPoints, "CurrentHitPoints should remain unchanged.");
        }

        [Test]
        public void UpdateMaxHp_ShouldDecreaseMaxHpAndClampHitPoints()
        {
            var hpComponent = new HitPointsComponent(80, 100);

            hpComponent.UpdateMaxHp(-50);

            Assert.AreEqual(50, hpComponent.MaxHitPoints, "MaxHitPoints should decrease by the given value.");
            Assert.AreEqual(50, hpComponent.CurrentHitPoints,
                "CurrentHitPoints should be clamped to the new MaxHitPoints.");
        }

        [Test]
        public void UpdateMaxHp_ShouldNotAllowNegativeMaxHp()
        {
            var hpComponent = new HitPointsComponent(50, 100);

            hpComponent.UpdateMaxHp(-150);

            Assert.AreEqual(0, hpComponent.MaxHitPoints, "MaxHitPoints should not go below 0.");
            Assert.AreEqual(0, hpComponent.CurrentHitPoints, "CurrentHitPoints should be clamped to 0.");
        }

        [Test]
        public void UpdateCurrentHp_ShouldIncreaseHitPointsWithinMaxHp()
        {
            var hpComponent = new HitPointsComponent(50, 100);

            hpComponent.UpdateCurrentHp(30);

            Assert.AreEqual(80, hpComponent.CurrentHitPoints, "CurrentHitPoints should increase by the given value.");
        }

        [Test]
        public void UpdateCurrentHp_ShouldNotExceedMaxHp()
        {
            var hpComponent = new HitPointsComponent(90, 100);

            hpComponent.UpdateCurrentHp(20);

            Assert.AreEqual(100, hpComponent.CurrentHitPoints, "CurrentHitPoints should not exceed MaxHitPoints.");
        }

        [Test]
        public void UpdateCurrentHp_ShouldDecreaseHitPointsButNotBelowZero()
        {
            var hpComponent = new HitPointsComponent(20, 100);

            hpComponent.UpdateCurrentHp(-30);

            Assert.AreEqual(0, hpComponent.CurrentHitPoints, "CurrentHitPoints should not go below 0.");
        }

        [Test]
        public void UpdateMaxHp_ShouldHandleZeroValues()
        {
            var hpComponent = new HitPointsComponent(50, 100);

            hpComponent.UpdateMaxHp(0);

            Assert.AreEqual(100, hpComponent.MaxHitPoints, "MaxHitPoints should remain unchanged.");
            Assert.AreEqual(50, hpComponent.CurrentHitPoints, "CurrentHitPoints should remain unchanged.");
        }

        [Test]
        public void UpdateCurrentHp_ShouldHandleZeroValues()
        {
            var hpComponent = new HitPointsComponent(50, 100);

            hpComponent.UpdateCurrentHp(0);

            Assert.AreEqual(50, hpComponent.CurrentHitPoints, "CurrentHitPoints should remain unchanged.");
        }
    }
}