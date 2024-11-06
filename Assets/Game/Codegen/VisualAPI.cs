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
    public static class VisualAPI
    {
        ///Keys
        public const int VisualTransform = 2; // Transform


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform GetVisualTransform(this IEntity obj) => obj.GetValue<Transform>(VisualTransform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetVisualTransform(this IEntity obj, out Transform value) => obj.TryGetValue(VisualTransform, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddVisualTransform(this IEntity obj, Transform value) => obj.AddValue(VisualTransform, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasVisualTransform(this IEntity obj) => obj.HasValue(VisualTransform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelVisualTransform(this IEntity obj) => obj.DelValue(VisualTransform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetVisualTransform(this IEntity obj, Transform value) => obj.SetValue(VisualTransform, value);
    }
}
