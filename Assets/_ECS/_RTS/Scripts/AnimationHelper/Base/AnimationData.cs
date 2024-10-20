using System;

namespace _ECS._RTS.Scripts.AnimationHelper.Base
{
    /// <summary> Holds all data about an animation </summary>
    [Serializable]
    public class AnimationData
    {
        public Animations Animation;

        /// <summary> Should the layer lock for this animation? </summary>
        public bool LockLayer;

        /// <summary> Should an animation play immediately after? </summary>
        public AnimationData NextAnimation;

        /// <summary> Should there be a transition time into this animation? </summary>
        public float CrossFade;

        /// <summary> Sets the animation data </summary>
        public AnimationData(Animations animation = Animations.RESET, bool lockLayer = false, AnimationData nextAnimation = null, float crossFade = 0.3f)
        {
            Animation = animation;
            LockLayer = lockLayer;
            NextAnimation = nextAnimation;
            CrossFade = crossFade;
        }
    }
}