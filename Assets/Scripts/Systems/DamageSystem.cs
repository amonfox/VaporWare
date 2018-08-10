using Assets.Scripts.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class DamageSystem : JobComponentSystem
    {
        [BurstCompile]
        struct MovementJob : IJobProcessComponentData<HealthComponent, DamageComponent>
        {
            public float deltaTime;

            public void Execute( ref HealthComponent health, [ReadOnly] ref DamageComponent damage )
            {
                float deltaDamage = damage.Value * deltaTime;

                health.Current = Mathf.Clamp( health.Current -= deltaDamage, 0, health.Max );
            }
        }

        protected override JobHandle OnUpdate( JobHandle inputDeps )
        {
            var job = new MovementJob( ) { deltaTime = Time.deltaTime };
            return job.Schedule( this, 64, inputDeps ); ;
        }
    }
}
