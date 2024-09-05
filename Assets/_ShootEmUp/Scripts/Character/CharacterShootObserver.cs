using System;
using UnityEngine;
using Utils;
using VContainer;

namespace ShootEmUp
{
    public sealed class CharacterShootObserver :
        IGameStartListener,
        IGameFinishListener
    {
        private bool m_FireRequired;
        private readonly CompositeCondition m_Condition = new();
        
        private WeaponData m_WeaponData;
        private IShootInput m_ShootInput;
        private IBulletSpawner m_BulletSpawner;
        private BulletConfig m_BulletConfig;

        [Inject]
        public void Construct(
            IShootInput shootInput,
            IHitPointsComponent hitPointsComponent,
            IBulletSpawner bulletSpawner,
            BulletConfig bulletConfig,
            WeaponData weaponData)
        {
            Debug.Log("[CharacterShootObserver] Construct");
            m_ShootInput = shootInput;
            m_WeaponData = weaponData;

            m_BulletSpawner = bulletSpawner;
            m_BulletConfig = bulletConfig;
            
            AppendCondition(hitPointsComponent.IsHitPointsExists);
        }
        
        private void AppendCondition(Func<bool> condition)
        {
            m_Condition.Append(condition);
        }
        
        void IGameStartListener.OnStartGame()
        {
            m_ShootInput.OnShoot += OnShoot;
        }

        void IGameFinishListener.OnFinishGame()
        {
            m_ShootInput.OnShoot -= OnShoot;
        }

        private void OnShoot()
        {
            Debug.Log("[CharacterShootObserver] OnShoot");
            if (m_Condition.IsAllFalse()) return;
            
            if (this.m_FireRequired) return;
            
            this.m_FireRequired = true;
            m_BulletSpawner.CreateBullet(BulletDataDefault());
            this.m_FireRequired = false;
        }

        private BulletData BulletDataDefault() => new BulletData(
            isPlayer: true, 
            physicsLayer: (int)this.m_BulletConfig.physicsLayer,
            color: this.m_BulletConfig.color, 
            damage: this.m_BulletConfig.damage, 
            position: m_WeaponData.position,
            velocity: m_WeaponData.rotation * Vector3.up * this.m_BulletConfig.speed
        );
    }
}