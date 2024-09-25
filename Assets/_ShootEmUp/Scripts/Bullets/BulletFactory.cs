using UnityEngine;

namespace ShootEmUp
{
    public interface IBulletFactory
    {
        Bullet Create();
    }
    
    public sealed class BulletFactory : IBulletFactory
    {
        private readonly Bullet m_Prefab;
        private readonly Transform m_WorldTransform;

        public BulletFactory(Bullet prefab, Transform worldTransform)
        {
            m_Prefab = prefab;
            m_WorldTransform = worldTransform;
        }

        public Bullet Create()
        {
            var bullet = Object.Instantiate(m_Prefab, m_WorldTransform);
            return bullet;
        }
    }
}