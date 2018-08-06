using Assets.Scripts.Components;
using System;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class MovementSystem : ComponentSystem
    {
        private struct Filter
        {
            public Rigidbody      Rigidbody;
            public InputComponent InputComponent;
        }

        protected override void OnUpdate( )
        {
            var deltaTime = Time.deltaTime;

            foreach( var entity in GetEntities<Filter>() )
            {
                var moveVector = new Vector3( entity.InputComponent.Horizontal, 0, 0 );
                var movePosition = entity.Rigidbody.position + moveVector.normalized * 2 * deltaTime;

                entity.Rigidbody.MovePosition( movePosition );
            }
        }
    }
}
