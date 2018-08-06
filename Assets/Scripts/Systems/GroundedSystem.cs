using Assets.Scripts.Components;
using System;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class GroundedSystem : ComponentSystem
    {
        private struct Filter
        {
            public Rigidbody      Rigidbody;
            public JumpComponent  JumpComponent;
            public InputComponent InputComponent;
        }

        protected override void OnUpdate( )
        {
            var deltaTime = Time.deltaTime;

            foreach( var entity in GetEntities<Filter>() )
            {
                var moveVector = new Vector3( 0, entity.InputComponent.Jump, 0 );
                var movePosition = entity.Rigidbody.position + moveVector.normalized * 7 * deltaTime;

                entity.Rigidbody.MovePosition( movePosition );
            }
        }
    }
}
