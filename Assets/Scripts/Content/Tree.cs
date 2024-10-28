using System;
using Game.Engine;
using UnityEngine;

namespace Game.Content
{
    public sealed class Tree : MonoBehaviour
    {
        public event Action<GameObject> OnStateInActived;
        
        private static readonly int ChopAnimHash = Animator.StringToHash("Chop");

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private ResourceStorageComponent storage;

        private void OnEnable()
        {
            this.storage.OnStateChanged += this.OnStateChanged;
        }

        private void OnDisable()
        {
            this.storage.OnStateChanged -= this.OnStateChanged;
        }

        private void OnStateChanged()
        {
            if (this.storage.IsEmpty())
            {
                this.gameObject.SetActive(false);
                OnStateInActived?.Invoke(gameObject);
            }
            else
            {
                _animator.Play(ChopAnimHash, -1, 0);
            }
        }

        public void InitDefault()
        {
            storage.AddResources(5);
        }
    }
}