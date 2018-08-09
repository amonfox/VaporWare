using Assets.Scripts.Components;
using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class DamageSystem : JobComponentSystem
    {
        [ComputeJobOptimization]
        struct MovementJob : IJobProcessComponentData<HealthComponent, DamageComponent>
        {
            public float deltaTime;

            public void Execute( ref HealthComponent health, [ReadOnly] ref DamageComponent damage )
            {
                float deltaDamage = damage.Value * deltaTime;

                Mathf.Clamp( health.Current -= deltaDamage, 0, health.Max );
            }
        }

        protected override JobHandle OnUpdate( JobHandle inputDeps )
        {
            var job = new MovementJob( ) { deltaTime = Time.deltaTime };
            return job.Schedule( this, 64, inputDeps ); ;
        }
    }
}
