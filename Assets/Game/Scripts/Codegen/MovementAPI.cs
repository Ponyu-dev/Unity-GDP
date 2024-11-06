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
    public static class MovementAPI
    {
        ///Keys
        public const int Position = 3; // float3Reactive
        public const int MoveDirection = 4; // float3Reactive
        public const int MoveSpeed = 5; // Const<float>
        public const int CanMove = 6; // AndExpression


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3Reactive GetPosition(this IEntity obj) => obj.GetValue<float3Reactive>(Position);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetPosition(this IEntity obj, out float3Reactive value) => obj.TryGetValue(Position, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddPosition(this IEntity obj, float3Reactive value) => obj.AddValue(Position, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasPosition(this IEntity obj) => obj.HasValue(Position);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelPosition(this IEntity obj) => obj.DelValue(Position);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this IEntity obj, float3Reactive value) => obj.SetValue(Position, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3Reactive GetMoveDirection(this IEntity obj) => obj.GetValue<float3Reactive>(MoveDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMoveDirection(this IEntity obj, out float3Reactive value) => obj.TryGetValue(MoveDirection, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddMoveDirection(this IEntity obj, float3Reactive value) => obj.AddValue(MoveDirection, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMoveDirection(this IEntity obj) => obj.HasValue(MoveDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelMoveDirection(this IEntity obj) => obj.DelValue(MoveDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMoveDirection(this IEntity obj, float3Reactive value) => obj.SetValue(MoveDirection, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Const<float> GetMoveSpeed(this IEntity obj) => obj.GetValue<Const<float>>(MoveSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMoveSpeed(this IEntity obj, out Const<float> value) => obj.TryGetValue(MoveSpeed, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddMoveSpeed(this IEntity obj, Const<float> value) => obj.AddValue(MoveSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMoveSpeed(this IEntity obj) => obj.HasValue(MoveSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelMoveSpeed(this IEntity obj) => obj.DelValue(MoveSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMoveSpeed(this IEntity obj, Const<float> value) => obj.SetValue(MoveSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AndExpression GetCanMove(this IEntity obj) => obj.GetValue<AndExpression>(CanMove);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCanMove(this IEntity obj, out AndExpression value) => obj.TryGetValue(CanMove, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCanMove(this IEntity obj, AndExpression value) => obj.AddValue(CanMove, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCanMove(this IEntity obj) => obj.HasValue(CanMove);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCanMove(this IEntity obj) => obj.DelValue(CanMove);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCanMove(this IEntity obj, AndExpression value) => obj.SetValue(CanMove, value);
    }
}
