/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;
using Atomic.Extensions;
using Unity.Mathematics;
using Game.Scripts.Common.Trigger;

namespace Atomic.Entities
{
    public static class CoreAPI
    {
        ///Keys
        public const int RootTransform = 10; // Transform
        public const int CameraMain = 1; // Camera
        public const int IsActive = 18; // IReactiveVariable<bool>
        public const int TriggerEventReceiver = 27; // TriggerEventReceiver


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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Camera GetCameraMain(this IEntity obj) => obj.GetValue<Camera>(CameraMain);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCameraMain(this IEntity obj, out Camera value) => obj.TryGetValue(CameraMain, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCameraMain(this IEntity obj, Camera value) => obj.AddValue(CameraMain, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCameraMain(this IEntity obj) => obj.HasValue(CameraMain);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCameraMain(this IEntity obj) => obj.DelValue(CameraMain);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCameraMain(this IEntity obj, Camera value) => obj.SetValue(CameraMain, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReactiveVariable<bool> GetIsActive(this IEntity obj) => obj.GetValue<IReactiveVariable<bool>>(IsActive);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetIsActive(this IEntity obj, out IReactiveVariable<bool> value) => obj.TryGetValue(IsActive, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIsActive(this IEntity obj, IReactiveVariable<bool> value) => obj.AddValue(IsActive, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIsActive(this IEntity obj) => obj.HasValue(IsActive);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelIsActive(this IEntity obj) => obj.DelValue(IsActive);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIsActive(this IEntity obj, IReactiveVariable<bool> value) => obj.SetValue(IsActive, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TriggerEventReceiver GetTriggerEventReceiver(this IEntity obj) => obj.GetValue<TriggerEventReceiver>(TriggerEventReceiver);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTriggerEventReceiver(this IEntity obj, out TriggerEventReceiver value) => obj.TryGetValue(TriggerEventReceiver, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTriggerEventReceiver(this IEntity obj, TriggerEventReceiver value) => obj.AddValue(TriggerEventReceiver, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTriggerEventReceiver(this IEntity obj) => obj.HasValue(TriggerEventReceiver);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTriggerEventReceiver(this IEntity obj) => obj.DelValue(TriggerEventReceiver);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTriggerEventReceiver(this IEntity obj, TriggerEventReceiver value) => obj.SetValue(TriggerEventReceiver, value);
    }
}
