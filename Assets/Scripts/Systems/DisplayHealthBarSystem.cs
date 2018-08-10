using Assets.Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    //[UpdateAfter(typeof(MeshInstanceRendererSystem))]
    class DisplayHealthBarSystem : ComponentSystem
    {
        private struct Data
        {
            readonly public int Length;
            public ComponentDataArray<HealthComponent>  HealthComponents;
            public ComponentDataArray<DisplayHealthHUD> HUDComponents;
        }

        [Inject] Data _data;

        

        protected override void OnUpdate( )
        {
            for( int index = 0; index < _data.Length; index++ )
            {
                var healthBarWidth = 200;
                var health = ( _data.HealthComponents[index].Current / _data.HealthComponents[index].Max );
                var healthWidth = healthBarWidth * health;
                healthWidth = Mathf.Clamp( healthWidth, 0, healthBarWidth );

                Camera.onPostRender = null;
                Camera.onPostRender += ( Camera camera ) =>
                {
                    GL.PushMatrix( );
                    GL.LoadPixelMatrix( 0, Screen.width, 0, Screen.height );
                };
                Camera.onPostRender += ( Camera camera ) =>
                {
                    // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
                    var texture1 = new Texture2D( 32, 32, TextureFormat.ARGB32, false );
                    var texture2 = new Texture2D( 32, 32, TextureFormat.ARGB32, false );

                    // set the pixel values
                    for( int y = 0; y < texture2.height; y++ )
                    {
                        for( int x = 0; x < texture2.width; x++ )
                        {
                            texture1.SetPixel( x, y, Color.red );
                            texture2.SetPixel( x, y, Color.green );
                        }
                    }

                    // Apply all SetPixel calls
                    texture1.Apply( );
                    texture2.Apply( );

                    Graphics.DrawTexture( new Rect( 100, Screen.height - 50, 200        , 20 ), texture1 );
                    Graphics.DrawTexture( new Rect( 100, Screen.height - 50, healthWidth, 20 ), texture2 );
                };
                Camera.onPostRender += ( Camera camera ) =>
                {
                    GL.PopMatrix( );
                };

            }
        }
    }
}
