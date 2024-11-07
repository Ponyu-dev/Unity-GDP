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
    public static class ShootAPI
    {
        ///Keys
        public const int CanFire = 15; // AndExpression
        public const int ShootAction = 16; // BaseEvent
        public const int ShootEvent = 17; // BaseEvent


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AndExpression GetCanFire(this IEntity obj) => obj.GetValue<AndExpression>(CanFire);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanFire(this IEntity obj, out AndExpression value) => obj.TryGetValue(CanFire, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanFire(this IEntity obj, AndExpression value) => obj.AddValue(CanFire, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanFire(this IEntity obj) => obj.HasValue(CanFire);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanFire(this IEntity obj) => obj.DelValue(CanFire);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanFire(this IEntity obj, AndExpression value) => obj.SetValue(CanFire, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseEvent GetShootAction(this IEntity obj) => obj.GetValue<BaseEvent>(ShootAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetShootAction(this IEntity obj, out BaseEvent value) => obj.TryGetValue(ShootAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddShootAction(this IEntity obj, BaseEvent value) => obj.AddValue(ShootAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasShootAction(this IEntity obj) => obj.HasValue(ShootAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelShootAction(this IEntity obj) => obj.DelValue(ShootAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetShootAction(this IEntity obj, BaseEvent value) => obj.SetValue(ShootAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseEvent GetShootEvent(this IEntity obj) => obj.GetValue<BaseEvent>(ShootEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetShootEvent(this IEntity obj, out BaseEvent value) => obj.TryGetValue(ShootEvent, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddShootEvent(this IEntity obj, BaseEvent value) => obj.AddValue(ShootEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasShootEvent(this IEntity obj) => obj.HasValue(ShootEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelShootEvent(this IEntity obj) => obj.DelValue(ShootEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetShootEvent(this IEntity obj, BaseEvent value) => obj.SetValue(ShootEvent, value);
    }
}
