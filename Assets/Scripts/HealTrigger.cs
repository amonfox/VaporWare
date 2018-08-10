using Assets.Scripts.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts
{
    class HealTrigger : MonoBehaviour
    {
        public HealComponent Heal;
        public LayerMask collisionMask = ~0;

        private void Start( )
        {
            Heal = GetComponent<HealComponent>( );
        }

        void OnTriggerEnter( Collider other )
        {
            if( collisionMask.value == 1 << other.gameObject.layer )
            {
                var entity = other.GetComponent<GameObjectEntity>( ).Entity;
                EntityManager entityManager = World.Active.GetExistingManager<EntityManager>( );
                bool hasDamage = entityManager.HasComponent<DamageComponent>( entity );

                if( hasDamage )
                {
                    var damage = entityManager.GetComponentData<DamageComponent>( entity );
                    damage.Value -= Heal.Value;
                    entityManager.SetComponentData<DamageComponent>( entity, damage );
                }
                else
                {
                    entityManager.AddComponentData( entity, new DamageComponent { Value = -Heal.Value } );
                    //entityManager.SetComponentData( entity, new DamageComponent { Value = Attack.Value } );
                }
            }
        }

        private void OnTriggerExit( Collider other )
        {
            if( collisionMask.value == 1 << other.gameObject.layer )
            {
                var entity = other.GetComponent<GameObjectEntity>( ).Entity;

                EntityManager entityManager = World.Active.GetExistingManager<EntityManager>( );
                var damage = entityManager.GetComponentData<DamageComponent>( entity );
                damage.Value += Heal.Value;
                if( damage.Value <= 0 ) damage.Value = 0;
                entityManager.SetComponentData( entity, damage );
            }
        }


    }
}
