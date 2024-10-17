using System;
using _ECS._RTS.Scripts.Reactivies;
using _ECS._RTS.Scripts.UI.Views;
using UniRx;
using UnityEngine;
using VContainer;

namespace _ECS._RTS.Scripts.UI.Presenters
{
    public class HealthPresenter : IDisposable
    {
        private ReactiveHealth _reactiveHealth;
        private HealthView _view;
        private readonly CompositeDisposable _disposables = new();

        private const int DefaultHeight = 2;
        private const int DefaultWidth = 16;

        private int GetCurrentWidth()
        {
            return (_reactiveHealth.Current.Value / _reactiveHealth.Max.Value) * DefaultWidth;
        }

        [Inject]
        public HealthPresenter()
        {
            Debug.Log("[HealthPresenter] Constructor");
        }
        
        public void Init(ReactiveHealth reactiveHealth, HealthView view)
        {
            Debug.Log("[HealthPresenter] Init");

            _reactiveHealth = reactiveHealth;
            _view = view;
            
            _reactiveHealth.Current.Subscribe(UpdateView).AddTo(_disposables);
            _reactiveHealth.Current.Subscribe(UpdateView).AddTo(_disposables);
        }

        private void UpdateView(int current)
        {
            Debug.Log($"[HealthPresenter] UpdateView");
            _view.UpdateHealth($"{_reactiveHealth.Current.Value} / {_reactiveHealth.Max.Value}");
            _view.UpdateHealthBarFill(new Vector2(GetCurrentWidth(), DefaultHeight));
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}