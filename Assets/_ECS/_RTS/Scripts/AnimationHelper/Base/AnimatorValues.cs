using System;
using UnityEngine;

namespace _ECS._RTS.Scripts.AnimationHelper.Base
{
    /// <summary> Class the manages the hashes of animations and parameters </summary>
    public class AnimatorValues
    {
        /// <summary> Returns the animation hash array </summary>
        public static int[] Animations { get; private set; }
        private static bool _initialized = false;

        /// <summary> Initializes the animator state names </summary>
        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            var names = Enum.GetNames(typeof(Animations));

            Animations = new int[names.Length];
            for (int i = 0, count = names.Length; i < count; i++)
                Animations[i] = Animator.StringToHash(names[i]);
        }

        /// <summary> Gets the animator hash value of an animation </summary>
        public static int GetHash(Animations animation)
        {
            return Animations[(int)animation];
        }
    }
}