using Atomic.Entities;
using UnityEngine;

namespace Game.Scripts.Entities
{
    public sealed class CharacterEntityInstaller : EntityInstaller
    {
        [SerializeField] private Camera cameraMain;

        public override void Install(IEntity entity)
        {
            base.Install(entity);
            entity.AddCharacterTag();
            entity.AddCameraMain(cameraMain);
        }
    }
}