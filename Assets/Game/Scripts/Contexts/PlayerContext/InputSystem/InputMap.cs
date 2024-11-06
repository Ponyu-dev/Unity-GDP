using System;
using UnityEngine;

namespace Game.Scripts.Contexts.PlayerContext.InputSystem
{
    [Serializable]
    public sealed class InputMap
    {
        public KeyCode forward = KeyCode.UpArrow;
        public KeyCode back = KeyCode.DownArrow;
        public KeyCode left = KeyCode.LeftArrow;
        public KeyCode right = KeyCode.RightArrow;
    }
}