using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveInput
    {
        event Action<Vector2> OnMove;
    }
}