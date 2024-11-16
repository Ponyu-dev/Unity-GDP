// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-16
// <file>: AnimatorDispatcher.cs
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Helpers
{
    public enum ActionType
    {
        NONE,
        SHOOT
    }
    
    public sealed class AnimatorDispatcher : MonoBehaviour
    {
        private readonly Dictionary<ActionType, List<Action>> _dictionary = new();

        public void ReceiveEvent(ActionType actionType)
        {
            if (!_dictionary.TryGetValue(actionType, out var actionList)) return;
            
            foreach (var action in actionList)
            {
                action?.Invoke();
            }
        }
        
        public void SubscribeOnEvent(ActionType key, Action action)
        {
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, new List<Action>());
            }
            
            _dictionary[key].Add(action);
        }
        
        public void UnsubscribeOnEvent(ActionType key, Action action)
        {
            if (!_dictionary.TryGetValue(key, out var actionList)) return;
            
            actionList.Remove(action);
        }
    }
}