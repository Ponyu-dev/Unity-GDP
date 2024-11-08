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
    public static class AnimationAPI
    {
        ///Keys
        public const int Animator = 19; // Animator
        public const int AnimBoolEvent = 20; // BaseEvent<string, bool>
        public const int AnimTriggerEvent = 21; // BaseEvent<string>


        ///Extensions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Animator GetAnimator(this IEntity obj) => obj.GetValue<Animator>(Animator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAnimator(this IEntity obj, out Animator value) => obj.TryGetValue(Animator, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAnimator(this IEntity obj, Animator value) => obj.AddValue(Animator, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnimator(this IEntity obj) => obj.HasValue(Animator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAnimator(this IEntity obj) => obj.DelValue(Animator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAnimator(this IEntity obj, Animator value) => obj.SetValue(Animator, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseEvent<string, bool> GetAnimBoolEvent(this IEntity obj) => obj.GetValue<BaseEvent<string, bool>>(AnimBoolEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAnimBoolEvent(this IEntity obj, out BaseEvent<string, bool> value) => obj.TryGetValue(AnimBoolEvent, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAnimBoolEvent(this IEntity obj, BaseEvent<string, bool> value) => obj.AddValue(AnimBoolEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnimBoolEvent(this IEntity obj) => obj.HasValue(AnimBoolEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAnimBoolEvent(this IEntity obj) => obj.DelValue(AnimBoolEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAnimBoolEvent(this IEntity obj, BaseEvent<string, bool> value) => obj.SetValue(AnimBoolEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseEvent<string> GetAnimTriggerEvent(this IEntity obj) => obj.GetValue<BaseEvent<string>>(AnimTriggerEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAnimTriggerEvent(this IEntity obj, out BaseEvent<string> value) => obj.TryGetValue(AnimTriggerEvent, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAnimTriggerEvent(this IEntity obj, BaseEvent<string> value) => obj.AddValue(AnimTriggerEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnimTriggerEvent(this IEntity obj) => obj.HasValue(AnimTriggerEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAnimTriggerEvent(this IEntity obj) => obj.DelValue(AnimTriggerEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAnimTriggerEvent(this IEntity obj, BaseEvent<string> value) => obj.SetValue(AnimTriggerEvent, value);
    }
}
