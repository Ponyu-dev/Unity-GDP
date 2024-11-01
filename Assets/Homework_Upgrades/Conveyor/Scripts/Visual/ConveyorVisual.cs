using Sirenix.OdinInspector;
using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.Visual
{
    public sealed class ConveyorVisual : MonoBehaviour
    {
        private static readonly int State = Animator.StringToHash("State");

        private const int IdleAnimation = 0;
        private const int SawAnimation = 1;

        [Space]
        [SerializeField]
        private Animator workerAnimator;

        [SerializeField]
        private GameObject sawObject;

        [SerializeField]
        private GameObject woodObject;

        private void Awake()
        {
            sawObject.SetActive(false);
            woodObject.SetActive(false);
        }

        [Button]
        public void Play()
        {
            workerAnimator.SetInteger(State, SawAnimation);
            sawObject.SetActive(true);
            woodObject.SetActive(true);
        }

        [Button]
        public void Stop()
        {
            workerAnimator.SetInteger(State, IdleAnimation);
            sawObject.SetActive(false);
            woodObject.SetActive(false);
        }
    }
}