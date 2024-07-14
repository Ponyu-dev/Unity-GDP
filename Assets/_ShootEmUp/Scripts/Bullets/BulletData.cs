using UnityEngine;

namespace ShootEmUp
{
    public struct BulletData
    {
        public Vector2 position { get; private set; }
        public Vector2 velocity { get; private set; }
        public Color color { get; private set; }
        public int physicsLayer { get; private set; }
        public int damage { get; private set; }
        public bool isPlayer { get; private set; }

        public BulletData(Vector2 position, Vector2 velocity, Color color, int physicsLayer, int damage, bool isPlayer)
        {
            this.position = position;
            this.velocity = velocity;
            this.color = color;
            this.physicsLayer = physicsLayer;
            this.damage = damage;
            this.isPlayer = isPlayer;
        }
    }
}