using System;
using System.Collections.Generic;
using System.Linq;
using SampleGame;
using UnityEngine;

namespace _Addressables.Game.Scripts.Gameplay.Locations.Trigger
{
    public sealed class TriggerPresenter
    {
        private const string Prefix = "Trigger";
        
        private readonly Transform _container;
        private readonly List<ITriggerView> _triggerViews;
        private readonly IAddressablesService _addressablesService;

        public TriggerPresenter(Transform worldContainer, List<ITriggerView> triggerViews, IAddressablesService addressablesService)
        {
            _container = worldContainer;
            _triggerViews = triggerViews;
            _addressablesService = addressablesService;
            
            foreach (var triggerView in _triggerViews)
            {
                triggerView.OnTriggerEntered += HandleTriggerEntered;
            }
        }
        
        ~TriggerPresenter()
        {
            foreach (var triggerView in _triggerViews)
            {
                triggerView.OnTriggerEntered -= HandleTriggerEntered;
            }
        }

        private void HandleTriggerEntered(string triggerName)
        {
            var location = GetNewName(triggerName);
            Debug.Log($"TriggerPresenter: Trigger {triggerName} {location} entered");
            _addressablesService.SpawnAsync(location, _container);
        }
        
        private string GetNewName(string currentName)
        {
            var numberPart = string.Concat(currentName.Where(char.IsDigit));
            Debug.Log($"numberPart = {numberPart}");
            return int.TryParse(numberPart, out var number) ? $"Location{number}" : default;
        }
    }
}