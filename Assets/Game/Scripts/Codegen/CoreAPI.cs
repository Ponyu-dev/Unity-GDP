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
    public static class CoreAPI
    {
        ///Keys
        public const int RootTransform = 10; // Transform


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform GetRootTransform(this IEntity obj) => obj.GetValue<Transform>(RootTransform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetRootTransform(this IEntity obj, out Transform value) => obj.TryGetValue(RootTransform, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddRootTransform(this IEntity obj, Transform value) => obj.AddValue(RootTransform, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasRootTransform(this IEntity obj) => obj.HasValue(RootTransform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelRootTransform(this IEntity obj) => obj.DelValue(RootTransform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRootTransform(this IEntity obj, Transform value) => obj.SetValue(RootTransform, value);
    }
}
