using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Entities;
using UnityEngine;
using Unity.Rendering;
using Assets.Scripts.Components;
using Assets.Scripts.Systems;

public class GameManager : MonoBehaviour
{
    #region Prefabs

    public GameObject Ship;
    public Mesh PlayerMesh;
    public Material PlayerMaterial;
    public Entity Player;

    #endregion
    
    EntityManager manager;

    private void Start( )
    {
        QualitySettings.vSyncCount = 0;
        var player = GameObject.Find( "Player" ).GetComponent<GameObjectEntity>( ).Entity;
        World.Active.GetExistingManager<EntityManager>( ).AddComponentData( player, new HealthComponent { Max = 100, Current = 100 } );
        World.Active.GetExistingManager<EntityManager>( ).AddComponentData( player, new DisplayHealthHUD() );

    }

    private void Update( )
    {
        if( Input.GetKeyDown("space") )
        {
            //Jump( );a
            //AddEntities( 500 );
        }

        Camera.onPostRender = null;
        //Camera.onPostRender += ( Camera camera ) =>
        //{
        //    GL.PushMatrix( );
        //    GL.PopMatrix( );
        //};

    }

    void Jump()
    {
        manager.SetComponentData( Player, new Position { Value = new float3( 0f, 5.0f, 0f ) } );
    }


    void AddEntities( int amount )
    {
        NativeArray<Entity> entities = new NativeArray<Entity>( amount, Allocator.Temp );
        manager.Instantiate( Ship, entities );

        for( int index = 0; index < amount; index++ )
        {
            float xVal = Random.Range( -100f, 100f );
            float zVal = Random.Range(    0f,  10f );
            manager.SetComponentData( entities[index], new Position  { Value = new float3( xVal, 0f, zVal ) } );
            manager.SetComponentData( entities[index], new Rotation  { Value = new quaternion( 0, 1, 0, 0 ) } );
            manager.SetComponentData( entities[index], new MoveSpeed { speed = -1 } );
        }

        entities.Dispose( );                
    }
}
