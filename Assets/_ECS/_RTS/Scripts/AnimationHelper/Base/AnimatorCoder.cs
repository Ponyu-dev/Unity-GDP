using System;
using System.Collections;
using UnityEngine;

namespace _ECS._RTS.Scripts.AnimationHelper.Base
{
    public interface IAnimatorCoder
    {
        /// <summary> Sets up the Animator Brain </summary>
        void Initialize();

        /// <summary> Returns the current animation that is playing on a specific layer </summary>
        Animations GetCurrentAnimation(int layer);

        /// <summary> Locks or unlocks the animation layer </summary>
        void SetLocked(bool lockLayer, int layer);

        /// <summary> Checks if a layer is locked </summary>
        bool IsLocked(int layer);

        /// <summary> Sets an animator parameter by id </summary>
        void SetBool(Parameters id, bool value);

        /// <summary> Returns the value of an animator parameter by id </summary>
        bool GetBool(Parameters id);

        /// <summary> Plays an animation on the specified layer asynchronously </summary>
        bool Play(AnimationData data, int layer = 0);
    }

    public abstract class BaseAnimatorCoder : MonoBehaviour, IAnimatorCoder
    {
        /// <summary> The baseline animation logic on a specific layer </summary>
        public abstract void DefaultAnimation(int layer);

        [SerializeField] private Animator animator;
        private Animations[] _currentAnimation;
        private bool[] _layerLocked;
        private ParameterDisplay[] _parameters;
        private Coroutine[] _currentCoroutine;

        /// <summary> Sets up the Animator Brain </summary>
        public void Initialize()
        {
            AnimatorValues.Initialize();

            var layerCount = this.animator.layerCount;
            _layerLocked = new bool[layerCount];
            _currentAnimation = new Animations[layerCount];
            _currentCoroutine = new Coroutine[layerCount];

            for (int i = 0; i < layerCount; ++i)
            {
                _layerLocked[i] = false;

                var hash = this.animator.GetCurrentAnimatorStateInfo(i).shortNameHash;
                for (int k = 0, count = AnimatorValues.Animations.Length; k < count; ++k)
                {
                    if (hash != AnimatorValues.Animations[k]) continue;

                    _currentAnimation[i] = (Animations)Enum.GetValues(typeof(Animations)).GetValue(k);
                    k = AnimatorValues.Animations.Length;
                }
            }

            var names = Enum.GetNames(typeof(Parameters));
            _parameters = new ParameterDisplay[names.Length];
            for (int i = 0, count = names.Length; i < count; ++i)
            {
                _parameters[i].name = names[i];
                _parameters[i].value = false;
            }
        }

        /// <summary> Returns the current animation that is playing </summary>
        public Animations GetCurrentAnimation(int layer)
        {
            try
            {
                return _currentAnimation[layer];
            }
            catch
            {
                LogError(
                    "Can't retrieve Current Animation. Fix: Initialize() in Start() and don't exceed number of animator layers");
                return Animations.RESET;
            }
        }

        /// <summary> Sets the whole layer to be locked or unlocked </summary>
        public void SetLocked(bool lockLayer, int layer)
        {
            try
            {
                _layerLocked[layer] = lockLayer;
            }
            catch
            {
                LogError(
                    "Can't retrieve Current Animation. Fix: Initialize() in Start() and don't exceed number of animator layers");
            }
        }

        public bool IsLocked(int layer)
        {
            try
            {
                return _layerLocked[layer];
            }
            catch
            {
                LogError(
                    "Can't retrieve Current Animation. Fix: Initialize() in Start() and don't exceed number of animator layers");
                return false;
            }
        }

        /// <summary> Sets an animator parameter </summary>
        public void SetBool(Parameters id, bool value)
        {
            try
            {
                _parameters[(int)id].value = value;
            }
            catch
            {
                LogError("Please Initialize() in Start()");
            }
        }

        /// <summary> Returns an animator parameter </summary>
        public bool GetBool(Parameters id)
        {
            try
            {
                return _parameters[(int)id].value;
            }
            catch
            {
                LogError("Please Initialize() in Start()");
                return false;
            }
        }
        
        /// <summary> Takes in the animation details and the animation layer, then attempts to play the animation </summary>
        public bool Play(AnimationData data, int layer = 0)
        {
            try
            {
                if (data.Animation == Animations.RESET)
                {
                    DefaultAnimation(layer);
                    return false;
                }

                if (_layerLocked[layer] || _currentAnimation[layer] == data.Animation) return false;

                if (_currentCoroutine[layer] != null) StopCoroutine(_currentCoroutine[layer]);
                _layerLocked[layer] = data.LockLayer;
                _currentAnimation[layer] = data.Animation;

                animator.CrossFade(AnimatorValues.GetHash(_currentAnimation[layer]), data.CrossFade, layer);

                if (data.NextAnimation == null) return true;
                
                _currentCoroutine[layer] = StartCoroutine(Wait());
                
                IEnumerator Wait()
                {
                    animator.Update(0);
                    var delay = animator.GetNextAnimatorStateInfo(layer).length;
                    if (data.CrossFade == 0) delay = animator.GetCurrentAnimatorStateInfo(layer).length;
                    yield return new WaitForSeconds(delay - data.NextAnimation.CrossFade);
                    SetLocked(false, layer);
                    Play(data.NextAnimation, layer);
                }

                return true;
            }
            catch
            {
                LogError("Please Initialize() in Start()");
                return false;
            }
        }

        private void LogError(string message)
        {
            Debug.LogError("AnimatorCoder Error: " + message);
        }
    }
}