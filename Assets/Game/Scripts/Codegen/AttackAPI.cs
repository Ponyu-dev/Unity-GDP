/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;
using Atomic.Extensions;
using Unity.Mathematics;

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
    }
}
