using System;
using UnityEngine;
using UnityEngine.Serialization;
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
        
        //private BulletSpawner bulletSpawner;
        //private BulletConfig bulletConfig;

        [Inject]
        public void Construct(
            IShootInput shootInput,
            IHitPointsComponent hitPointsComponent,
            WeaponData weaponData)
        {
            Debug.Log("[CharacterShootObserver] Construct");
            m_ShootInput = shootInput;
            m_WeaponData = weaponData;
            
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
            /*if (m_Condition.IsAllFalse()) return;
            
            if (this.m_FireRequired) return;
            
            this.m_FireRequired = true;
            bulletSpawner.CreateBullet(BulletDataDefault());
            this.m_FireRequired = false;*/
        }

        /*private BulletData BulletDataDefault() => new BulletData(
            isPlayer: true, 
            physicsLayer: (int)this.bulletConfig.physicsLayer,
            color: this.bulletConfig.color, 
            damage: this.bulletConfig.damage, 
            position: weaponData.position,
            velocity: weaponData.rotation * Vector3.up * this.bulletConfig.speed
        );*/
    }
}