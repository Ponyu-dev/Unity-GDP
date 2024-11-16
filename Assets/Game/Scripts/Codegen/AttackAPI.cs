/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;
using Atomic.Extensions;
using Unity.Mathematics;
using Game.Scripts.Helpers;

namespace Atomic.Entities
{
    public static class AttackAPI
    {
        ///Keys
        public const int CanAttack = 15; // AndExpression
        public const int AttackAction = 16; // BaseEvent
        public const int AttackEvent = 17; // BaseEvent
        public const int AttackRange = 23; // Const<float>
        public const int AttackEntity = 24; // Const<IEntity>
        public const int IsRangeAttack = 25; // IReactiveVariable<bool>
        public const int AttackPeroid = 26; // Cycle
        public const int Damage = 28; // Const<int>
        public const int AttackCountdown = 30; // Countdown
        public const int ShootVFX = 31; // ParticleSystem
        public const int MaxAmmo = 33; // Const<int>
        public const int CurrentAmmo = 34; // IReactiveVariable<int>
        public const int FirePoint = 32; // Transform
        public const int ShootAction = 37; // BaseEvent<Transform>
        public const int BulletSpeed = 38; // Const<float>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AndExpression GetCanAttack(this IEntity obj) => obj.GetValue<AndExpression>(CanAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanAttack(this IEntity obj, out AndExpression value) => obj.TryGetValue(CanAttack, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanAttack(this IEntity obj, AndExpression value) => obj.AddValue(CanAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanAttack(this IEntity obj) => obj.HasValue(CanAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanAttack(this IEntity obj) => obj.DelValue(CanAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanAttack(this IEntity obj, AndExpression value) => obj.SetValue(CanAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseEvent GetAttackAction(this IEntity obj) => obj.GetValue<BaseEvent>(AttackAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackAction(this IEntity obj, out BaseEvent value) => obj.TryGetValue(AttackAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackAction(this IEntity obj, BaseEvent value) => obj.AddValue(AttackAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackAction(this IEntity obj) => obj.HasValue(AttackAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackAction(this IEntity obj) => obj.DelValue(AttackAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackAction(this IEntity obj, BaseEvent value) => obj.SetValue(AttackAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseEvent GetAttackEvent(this IEntity obj) => obj.GetValue<BaseEvent>(AttackEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackEvent(this IEntity obj, out BaseEvent value) => obj.TryGetValue(AttackEvent, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackEvent(this IEntity obj, BaseEvent value) => obj.AddValue(AttackEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackEvent(this IEntity obj) => obj.HasValue(AttackEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackEvent(this IEntity obj) => obj.DelValue(AttackEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackEvent(this IEntity obj, BaseEvent value) => obj.SetValue(AttackEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Const<float> GetAttackRange(this IEntity obj) => obj.GetValue<Const<float>>(AttackRange);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackRange(this IEntity obj, out Const<float> value) => obj.TryGetValue(AttackRange, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackRange(this IEntity obj, Const<float> value) => obj.AddValue(AttackRange, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackRange(this IEntity obj) => obj.HasValue(AttackRange);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackRange(this IEntity obj) => obj.DelValue(AttackRange);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackRange(this IEntity obj, Const<float> value) => obj.SetValue(AttackRange, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Const<IEntity> GetAttackEntity(this IEntity obj) => obj.GetValue<Const<IEntity>>(AttackEntity);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackEntity(this IEntity obj, out Const<IEntity> value) => obj.TryGetValue(AttackEntity, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackEntity(this IEntity obj, Const<IEntity> value) => obj.AddValue(AttackEntity, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackEntity(this IEntity obj) => obj.HasValue(AttackEntity);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackEntity(this IEntity obj) => obj.DelValue(AttackEntity);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackEntity(this IEntity obj, Const<IEntity> value) => obj.SetValue(AttackEntity, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReactiveVariable<bool> GetIsRangeAttack(this IEntity obj) => obj.GetValue<IReactiveVariable<bool>>(IsRangeAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsRangeAttack(this IEntity obj, out IReactiveVariable<bool> value) => obj.TryGetValue(IsRangeAttack, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsRangeAttack(this IEntity obj, IReactiveVariable<bool> value) => obj.AddValue(IsRangeAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsRangeAttack(this IEntity obj) => obj.HasValue(IsRangeAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsRangeAttack(this IEntity obj) => obj.DelValue(IsRangeAttack);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsRangeAttack(this IEntity obj, IReactiveVariable<bool> value) => obj.SetValue(IsRangeAttack, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Cycle GetAttackPeroid(this IEntity obj) => obj.GetValue<Cycle>(AttackPeroid);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackPeroid(this IEntity obj, out Cycle value) => obj.TryGetValue(AttackPeroid, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackPeroid(this IEntity obj, Cycle value) => obj.AddValue(AttackPeroid, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackPeroid(this IEntity obj) => obj.HasValue(AttackPeroid);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackPeroid(this IEntity obj) => obj.DelValue(AttackPeroid);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackPeroid(this IEntity obj, Cycle value) => obj.SetValue(AttackPeroid, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Const<int> GetDamage(this IEntity obj) => obj.GetValue<Const<int>>(Damage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetDamage(this IEntity obj, out Const<int> value) => obj.TryGetValue(Damage, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddDamage(this IEntity obj, Const<int> value) => obj.AddValue(Damage, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasDamage(this IEntity obj) => obj.HasValue(Damage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelDamage(this IEntity obj) => obj.DelValue(Damage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDamage(this IEntity obj, Const<int> value) => obj.SetValue(Damage, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Countdown GetAttackCountdown(this IEntity obj) => obj.GetValue<Countdown>(AttackCountdown);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttackCountdown(this IEntity obj, out Countdown value) => obj.TryGetValue(AttackCountdown, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAttackCountdown(this IEntity obj, Countdown value) => obj.AddValue(AttackCountdown, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttackCountdown(this IEntity obj) => obj.HasValue(AttackCountdown);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAttackCountdown(this IEntity obj) => obj.DelValue(AttackCountdown);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAttackCountdown(this IEntity obj, Countdown value) => obj.SetValue(AttackCountdown, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParticleSystem GetShootVFX(this IEntity obj) => obj.GetValue<ParticleSystem>(ShootVFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetShootVFX(this IEntity obj, out ParticleSystem value) => obj.TryGetValue(ShootVFX, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddShootVFX(this IEntity obj, ParticleSystem value) => obj.AddValue(ShootVFX, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasShootVFX(this IEntity obj) => obj.HasValue(ShootVFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelShootVFX(this IEntity obj) => obj.DelValue(ShootVFX);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetShootVFX(this IEntity obj, ParticleSystem value) => obj.SetValue(ShootVFX, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Const<int> GetMaxAmmo(this IEntity obj) => obj.GetValue<Const<int>>(MaxAmmo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMaxAmmo(this IEntity obj, out Const<int> value) => obj.TryGetValue(MaxAmmo, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddMaxAmmo(this IEntity obj, Const<int> value) => obj.AddValue(MaxAmmo, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMaxAmmo(this IEntity obj) => obj.HasValue(MaxAmmo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelMaxAmmo(this IEntity obj) => obj.DelValue(MaxAmmo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMaxAmmo(this IEntity obj, Const<int> value) => obj.SetValue(MaxAmmo, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReactiveVariable<int> GetCurrentAmmo(this IEntity obj) => obj.GetValue<IReactiveVariable<int>>(CurrentAmmo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCurrentAmmo(this IEntity obj, out IReactiveVariable<int> value) => obj.TryGetValue(CurrentAmmo, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCurrentAmmo(this IEntity obj, IReactiveVariable<int> value) => obj.AddValue(CurrentAmmo, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCurrentAmmo(this IEntity obj) => obj.HasValue(CurrentAmmo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCurrentAmmo(this IEntity obj) => obj.DelValue(CurrentAmmo);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCurrentAmmo(this IEntity obj, IReactiveVariable<int> value) => obj.SetValue(CurrentAmmo, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform GetFirePoint(this IEntity obj) => obj.GetValue<Transform>(FirePoint);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFirePoint(this IEntity obj, out Transform value) => obj.TryGetValue(FirePoint, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddFirePoint(this IEntity obj, Transform value) => obj.AddValue(FirePoint, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFirePoint(this IEntity obj) => obj.HasValue(FirePoint);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelFirePoint(this IEntity obj) => obj.DelValue(FirePoint);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetFirePoint(this IEntity obj, Transform value) => obj.SetValue(FirePoint, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseEvent<Transform> GetShootAction(this IEntity obj) => obj.GetValue<BaseEvent<Transform>>(ShootAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetShootAction(this IEntity obj, out BaseEvent<Transform> value) => obj.TryGetValue(ShootAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddShootAction(this IEntity obj, BaseEvent<Transform> value) => obj.AddValue(ShootAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasShootAction(this IEntity obj) => obj.HasValue(ShootAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelShootAction(this IEntity obj) => obj.DelValue(ShootAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetShootAction(this IEntity obj, BaseEvent<Transform> value) => obj.SetValue(ShootAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Const<float> GetBulletSpeed(this IEntity obj) => obj.GetValue<Const<float>>(BulletSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetBulletSpeed(this IEntity obj, out Const<float> value) => obj.TryGetValue(BulletSpeed, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddBulletSpeed(this IEntity obj, Const<float> value) => obj.AddValue(BulletSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasBulletSpeed(this IEntity obj) => obj.HasValue(BulletSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelBulletSpeed(this IEntity obj) => obj.DelValue(BulletSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBulletSpeed(this IEntity obj, Const<float> value) => obj.SetValue(BulletSpeed, value);
    }
}
