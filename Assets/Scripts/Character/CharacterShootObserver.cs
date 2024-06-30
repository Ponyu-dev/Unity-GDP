using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterShootObserver : MonoBehaviour
    {
        [SerializeField] private ShootInput shootInput;
        [SerializeField] private WeaponComponent _weaponComponent;
        
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        
        public bool _fireRequired;

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
            if (this._fireRequired) return;
            
            this._fireRequired = true;
            this.OnFlyBullet();
            this._fireRequired = false;
        }

        private Args BulletArgs() => new Args(
            isPlayer: true, 
            physicsLayer: (int)this._bulletConfig.physicsLayer,
            color: this._bulletConfig.color, 
            damage: this._bulletConfig.damage, 
            position: _weaponComponent.Position,
            velocity: _weaponComponent.Rotation * Vector3.up * this._bulletConfig.speed
        );

        private void OnFlyBullet()
        {
            _bulletSystem.FlyBulletByArgs(BulletArgs());
        }
    }
}