using Assets.Scripts.Systems;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject PauseMenu;

    public void Start( )
    {
        if( PauseMenu == null )
        {
            Debug.LogWarning( "Warning: Pause Menu not set to controller" );
            enabled = false;
        }
    }


    public void PauseGame()
    {
        //PauseMenu.PauseGame( );
    }

    public void ResumeGame( )
    {
       // IsPaused = false;
        World.Active.GetExistingManager<MovementSystem>( ).Enabled = true;
    }

    public void ExitToMainMenu( )
    {
        SceneManager.LoadScene( "MainMenu" );
    }


    public void Update( )
    {
        var escape = Input.GetKeyUp( KeyCode.Escape );

        if( escape )
        {
        }
    }

}
