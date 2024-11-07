using System;
using UnityEngine;

namespace Game.Scripts.Contexts.PlayerContext
{
    [Serializable]
    public sealed class InputMap
    {
        public KeyCode forward = KeyCode.W;
        public KeyCode back = KeyCode.S;
        public KeyCode left = KeyCode.A;
        public KeyCode right = KeyCode.D;
    }
}