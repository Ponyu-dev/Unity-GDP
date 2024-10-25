using System;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Views
{
    internal sealed class CollisionComponent : MonoBehaviour
    {
        /*[SerializeField]
        private Entity entity;*/

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Entity target))
            {
                Debug.Log($"[CollisionComponent] ON COLLISION ENTER {target.Id}", this);
                

                /*EcsStartup.Instance.CreateEntity(EcsWorlds.EVENTS)
                    .Add(new CollisionEnterRequest())
                    .Add(new BulletTag())
                    .Add(new SourceEntity {Value = _entity.Id})
                    .Add(new TargetEntity {Value = target.Id})
                    .Add(new Position {Value = collision.GetContact(0).point});*/
            }
        }
    }
}