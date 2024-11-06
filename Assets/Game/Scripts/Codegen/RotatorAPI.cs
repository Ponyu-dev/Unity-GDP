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
    public static class RotatorAPI
    {
        ///Keys
        public const int Rotation = 7; // quaternionReactive
        public const int AngularSpeed = 8; // Const<float>
        public const int Look = 9; // float3Reactive


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternionReactive GetRotation(this IEntity obj) => obj.GetValue<quaternionReactive>(Rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetRotation(this IEntity obj, out quaternionReactive value) => obj.TryGetValue(Rotation, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddRotation(this IEntity obj, quaternionReactive value) => obj.AddValue(Rotation, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasRotation(this IEntity obj) => obj.HasValue(Rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelRotation(this IEntity obj) => obj.DelValue(Rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRotation(this IEntity obj, quaternionReactive value) => obj.SetValue(Rotation, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Const<float> GetAngularSpeed(this IEntity obj) => obj.GetValue<Const<float>>(AngularSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAngularSpeed(this IEntity obj, out Const<float> value) => obj.TryGetValue(AngularSpeed, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAngularSpeed(this IEntity obj, Const<float> value) => obj.AddValue(AngularSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAngularSpeed(this IEntity obj) => obj.HasValue(AngularSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAngularSpeed(this IEntity obj) => obj.DelValue(AngularSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAngularSpeed(this IEntity obj, Const<float> value) => obj.SetValue(AngularSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3Reactive GetLook(this IEntity obj) => obj.GetValue<float3Reactive>(Look);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetLook(this IEntity obj, out float3Reactive value) => obj.TryGetValue(Look, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddLook(this IEntity obj, float3Reactive value) => obj.AddValue(Look, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasLook(this IEntity obj) => obj.HasValue(Look);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelLook(this IEntity obj) => obj.DelValue(Look);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLook(this IEntity obj, float3Reactive value) => obj.SetValue(Look, value);
    }
}
