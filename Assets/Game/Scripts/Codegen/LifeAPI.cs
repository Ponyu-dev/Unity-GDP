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
    public static class LifeAPI
    {
        ///Keys
        public const int HitPoints = 8; // IReactiveVariable<int>
        public const int IsDead = 11; // IReactiveVariable<bool>
        public const int TakeDamageAction = 12; // BaseEvent<int>
        public const int CanTakeDamage = 13; // AndExpression
        public const int DeadAction = 22; // BaseEvent<IEntity>
        public const int LifeTime = 36; // Cycle


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReactiveVariable<int> GetHitPoints(this IEntity obj) => obj.GetValue<IReactiveVariable<int>>(HitPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetHitPoints(this IEntity obj, out IReactiveVariable<int> value) => obj.TryGetValue(HitPoints, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddHitPoints(this IEntity obj, IReactiveVariable<int> value) => obj.AddValue(HitPoints, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasHitPoints(this IEntity obj) => obj.HasValue(HitPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelHitPoints(this IEntity obj) => obj.DelValue(HitPoints);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHitPoints(this IEntity obj, IReactiveVariable<int> value) => obj.SetValue(HitPoints, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReactiveVariable<bool> GetIsDead(this IEntity obj) => obj.GetValue<IReactiveVariable<bool>>(IsDead);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsDead(this IEntity obj, out IReactiveVariable<bool> value) => obj.TryGetValue(IsDead, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsDead(this IEntity obj, IReactiveVariable<bool> value) => obj.AddValue(IsDead, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsDead(this IEntity obj) => obj.HasValue(IsDead);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsDead(this IEntity obj) => obj.DelValue(IsDead);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsDead(this IEntity obj, IReactiveVariable<bool> value) => obj.SetValue(IsDead, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseEvent<int> GetTakeDamageAction(this IEntity obj) => obj.GetValue<BaseEvent<int>>(TakeDamageAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTakeDamageAction(this IEntity obj, out BaseEvent<int> value) => obj.TryGetValue(TakeDamageAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTakeDamageAction(this IEntity obj, BaseEvent<int> value) => obj.AddValue(TakeDamageAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTakeDamageAction(this IEntity obj) => obj.HasValue(TakeDamageAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTakeDamageAction(this IEntity obj) => obj.DelValue(TakeDamageAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTakeDamageAction(this IEntity obj, BaseEvent<int> value) => obj.SetValue(TakeDamageAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AndExpression GetCanTakeDamage(this IEntity obj) => obj.GetValue<AndExpression>(CanTakeDamage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanTakeDamage(this IEntity obj, out AndExpression value) => obj.TryGetValue(CanTakeDamage, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanTakeDamage(this IEntity obj, AndExpression value) => obj.AddValue(CanTakeDamage, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanTakeDamage(this IEntity obj) => obj.HasValue(CanTakeDamage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanTakeDamage(this IEntity obj) => obj.DelValue(CanTakeDamage);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanTakeDamage(this IEntity obj, AndExpression value) => obj.SetValue(CanTakeDamage, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseEvent<IEntity> GetDeadAction(this IEntity obj) => obj.GetValue<BaseEvent<IEntity>>(DeadAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetDeadAction(this IEntity obj, out BaseEvent<IEntity> value) => obj.TryGetValue(DeadAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddDeadAction(this IEntity obj, BaseEvent<IEntity> value) => obj.AddValue(DeadAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasDeadAction(this IEntity obj) => obj.HasValue(DeadAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelDeadAction(this IEntity obj) => obj.DelValue(DeadAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDeadAction(this IEntity obj, BaseEvent<IEntity> value) => obj.SetValue(DeadAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Cycle GetLifeTime(this IEntity obj) => obj.GetValue<Cycle>(LifeTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetLifeTime(this IEntity obj, out Cycle value) => obj.TryGetValue(LifeTime, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddLifeTime(this IEntity obj, Cycle value) => obj.AddValue(LifeTime, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasLifeTime(this IEntity obj) => obj.HasValue(LifeTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelLifeTime(this IEntity obj) => obj.DelValue(LifeTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLifeTime(this IEntity obj, Cycle value) => obj.SetValue(LifeTime, value);
    }
}
