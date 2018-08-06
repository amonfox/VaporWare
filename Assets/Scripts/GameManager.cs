using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Entities;
using UnityEngine;
using Unity.Rendering;
using Assets.Scripts.Components;

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

        /*
        manager = World.Active.GetOrCreateManager<EntityManager>( );

        var playerArchetype = manager.CreateArchetype(
            typeof( TransformMatrix ),
            typeof( MeshInstanceRenderer ),
            typeof( Position ),
            typeof( MoveSpeed ),
            typeof( InputComponent )
            );

        Player = manager.CreateEntity( playerArchetype );

        manager.SetSharedComponentData( Player, new MeshInstanceRenderer
        {
            mesh = PlayerMesh,
            material = PlayerMaterial
        } );

        var playerSpawn = GameObject.Find( "PlayerSpawn" );

        manager.SetComponentData( Player, new Position { Value = playerSpawn.transform.position } );
        manager.SetComponentData( Player, new MoveSpeed { speed = 0 } );

        Object.Destroy( playerSpawn );
        */

        //AddEntities( 500 );
    }

    private void Update( )
    {
        if( Input.GetKeyDown("space") )
        {
            //Jump( );
            //AddEntities( 500 );
        }
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
