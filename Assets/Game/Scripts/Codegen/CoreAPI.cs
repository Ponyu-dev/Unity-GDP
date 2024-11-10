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
    public static class CoreAPI
    {
        ///Keys
        public const int RootTransform = 10; // Transform
        public const int CameraMain = 1; // Camera
        public const int TriggerEventReceiver = 27; // TriggerEventReceiver
        public const int Rigidbody = 35; // Rigidbody


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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rigidbody GetRigidbody(this IEntity obj) => obj.GetValue<Rigidbody>(Rigidbody);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetRigidbody(this IEntity obj, out Rigidbody value) => obj.TryGetValue(Rigidbody, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddRigidbody(this IEntity obj, Rigidbody value) => obj.AddValue(Rigidbody, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasRigidbody(this IEntity obj) => obj.HasValue(Rigidbody);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelRigidbody(this IEntity obj) => obj.DelValue(Rigidbody);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRigidbody(this IEntity obj, Rigidbody value) => obj.SetValue(Rigidbody, value);
    }
}
