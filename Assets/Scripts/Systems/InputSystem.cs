using Assets.Scripts.Components;
using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class InputSystem : ComponentSystem
    {
        private struct Data
        {
            readonly public int Length;
            public ComponentArray<InputComponent> InputComponents;
        }

        [Inject] Data _data;

        protected override void OnUpdate( )
        {
            var horizontal = Input.GetAxis( "Horizontal" );
            var jump       = Input.GetAxis( "Jump" );


            for( int index = 0; index < _data.Length; index++)
            {
                _data.InputComponents[index].Horizontal = horizontal;
                _data.InputComponents[index].Jump       = jump;
            }
        }
    }
}
