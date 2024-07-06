using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterShootObserver : MonoBehaviour
    {
        [SerializeField] private ShootInput shootInput;
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private BulletConfig bulletConfig;
        
        private bool m_FireRequired;

        private void OnEnable()
        {
            shootInput.OnShoot += OnShoot;
        }

        private void OnDisable()
        {
            shootInput.OnShoot -= OnShoot;
        }

        private void OnShoot()
        {
            if (this.m_FireRequired) return;
            
            this.m_FireRequired = true;
            bulletSystem.CreateBullet(BulletDataDefault());
            this.m_FireRequired = false;
        }

        private BulletData BulletDataDefault() => new BulletData(
            isPlayer: true, 
            physicsLayer: (int)this.bulletConfig.physicsLayer,
            color: this.bulletConfig.color, 
            damage: this.bulletConfig.damage, 
            position: weaponComponent.position,
            velocity: weaponComponent.rotation * Vector3.up * this.bulletConfig.speed
        );
    }
}