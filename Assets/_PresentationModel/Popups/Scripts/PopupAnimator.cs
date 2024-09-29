using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
public class PopupAnimator : MonoBehaviour
{
    private const string NAME_SHOW = "Show";
    private const string NAME_HIDE = "Hide";

    [SerializeField]
    private Animator animator;

    [Header("Events")]
    [SerializeField]
    private UnityEvent onShown;

    [SerializeField]
    private UnityEvent onHidden;

    public event Action OnShowStarted;
    public event Action OnShowFinished;
    public event Action OnHideStarted;
    public event Action OnHideFinished;

    public void Show()
    {
        this.OnShowStarted?.Invoke();
        this.animator.Play(NAME_SHOW, -1, 0);
    }

    public void Hide()
    {
        this.OnHideStarted?.Invoke();
        this.animator.Play(NAME_HIDE, -1, 0);
    }

    [UsedImplicitly]
    private void OnShown()
    {
        this.onShown?.Invoke();
        this.OnShowFinished?.Invoke();
    }

    [UsedImplicitly]
    private void OnHidden()
    {
        this.onHidden?.Invoke();
        this.OnHideFinished?.Invoke();
    }


#if UNITY_EDITOR
        private void Reset()
        {
            this.animator = this.GetComponent<Animator>();
        }
#endif
}