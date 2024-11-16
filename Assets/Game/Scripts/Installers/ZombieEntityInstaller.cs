using Atomic.Entities;
using Atomic.Extensions;
using Game.Scripts.Common.ComponentInstallers;
using Game.Scripts.Common.Mechanics;
using UnityEngine;

namespace Game.Scripts.Entities
{
    public sealed class ZombieEntityInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private LifeComponentInstaller lifeComponentInstaller;
        [SerializeField] private MovementComponentInstaller movementComponentInstaller;
        [SerializeField] private RotationComponentInstaller rotateComponentInstaller;

        //TODO maybe move MeleeInstaller
        [SerializeField] private RangeAttackComponentInstaller rangeAttackComponentInstaller;
        [SerializeField] private MeleeAttackComponentInstaller meleeAttackComponentInstaller;
        [SerializeField] private TriggerEventComponentInstaller triggerEventComponentInstaller;
        
        //TODO maybe move to VisualInstaller
        [SerializeField] private AnimatorComponentInstaller animatorComponentInstaller;

        public override void Install(IEntity entity)
        {
            entity.AddZombieTag();
            entity.AddRootTransform(transform);
            entity.AddPosition(new float3Reactive(transform.position));
            entity.AddRotation(new quaternionReactive(transform.rotation));

            entity.AddBehaviour<TransformBehaviour>();
            
            lifeComponentInstaller.Install(entity);
            movementComponentInstaller.Install(entity);
            rotateComponentInstaller.Install(entity);
            rangeAttackComponentInstaller.Install(entity);
            meleeAttackComponentInstaller.Install(entity);
            triggerEventComponentInstaller.Install(entity);
            
            animatorComponentInstaller.Install(entity);
            
            movementComponentInstaller.Append(lifeComponentInstaller.IsNotDead());
            rotateComponentInstaller.Append(lifeComponentInstaller.IsNotDead());
            
            meleeAttackComponentInstaller.Append(lifeComponentInstaller.IsNotDead());
            meleeAttackComponentInstaller.Append(rangeAttackComponentInstaller.IsRangeAttack());
        }
    }
}