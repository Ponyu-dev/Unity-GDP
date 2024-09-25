using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveComponent
    {
        public void Move(Vector2 offset);
    }
}