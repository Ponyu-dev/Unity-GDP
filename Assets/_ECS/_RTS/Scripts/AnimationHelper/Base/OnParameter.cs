using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _ECS._RTS.Scripts.AnimationHelper.Base
{
    public class OnParameter : StateMachineBehaviour
    {
        [SerializeField, Tooltip("Parameter to test")] private Parameters parameter;
        [SerializeField, Tooltip("Specify whether it should be on or off")] private bool target;
        [SerializeField, Tooltip("Chain of animations to play when condition is met")] private AnimationData[] nextAnimations;

        private AnimatorCoder _animatorBrain;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _animatorBrain = animator.GetComponent<AnimatorCoder>();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_animatorBrain.GetBool(parameter) != target) return;
            _animatorBrain.SetLocked(false, layerIndex);

            for (int i = 0, count = nextAnimations.Length - 1; i < count; ++i)
                nextAnimations[i].NextAnimation = nextAnimations[i + 1];

            _animatorBrain.PlayAsync(nextAnimations[0], layerIndex).Forget();
        }
    }
}