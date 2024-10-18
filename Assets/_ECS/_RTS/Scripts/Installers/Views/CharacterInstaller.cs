using _ECS._RTS.Scripts.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS._RTS.Scripts.Installers
{
    public class CharacterInstaller : EntityInstaller
    {
        //TODO Maybe move to Configs ?
        [SerializeField] private float moveSpeed = 5.0f;
        [SerializeField] private float detectorRange = 10.0f;
        [SerializeField] private float attackRange = 3.0f;
        [SerializeField] private LayerMask layerMask;
        
        [SerializeField] private Transform moveDirection;
        
        private bool _isEnemyDetected;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new Position {Value = transform.position});
            entity.AddData(new Rotation {Value = transform.rotation});
            entity.AddData(new MoveDirection {Value = moveDirection.forward});
            entity.AddData(new MoveSpeed {Value = moveSpeed});
            entity.AddData(new DetectorRange {Value = detectorRange});
            entity.AddData(new AttackRange {Value = attackRange});
            entity.AddData(new Layer { Value = layerMask});
            entity.AddData(new TransformView { Value = transform});
            entity.AddData(new IsMoving { Value = true});
        }

        protected override void Dispose(Entity entity) { }
        
        void Update()
        {
            _isEnemyDetected = Physics.OverlapSphere(transform.position, detectorRange, layerMask).Length > 0;
        }
        
        private void OnDrawGizmos()
        {
            // Отрисовка сферы поиска
            Gizmos.color = _isEnemyDetected ? Color.red : Color.white;
            Gizmos.DrawWireSphere(transform.position, detectorRange);
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}