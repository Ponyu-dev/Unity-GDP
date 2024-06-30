using UnityEngine;

namespace ShootEmUp
{
    public struct Args
    {
        public Vector2 _position { get; private set; }
        public Vector2 _velocity { get; private set; }
        public Color _color { get; private set; }
        public int _physicsLayer { get; private set; }
        public int _damage { get; private set; }
        public bool _isPlayer { get; private set; }

        public Args(Vector2 position, Vector2 velocity, Color color, int physicsLayer, int damage, bool isPlayer)
        {
            this._position = position;
            this._velocity = velocity;
            this._color = color;
            this._physicsLayer = physicsLayer;
            this._damage = damage;
            this._isPlayer = isPlayer;
        }
    }
}