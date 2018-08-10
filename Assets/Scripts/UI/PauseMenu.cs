using Assets.Scripts.Systems;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool IsPaused;
    public GameObject pauseMenu;

    
    public void PauseGame()
    {
        IsPaused = true;
        World.Active.GetExistingManager<MovementSystem>( ).Enabled = false;
        World.Active.GetExistingManager<InputSystem>   ( ).Enabled = false;
        World.Active.GetExistingManager<JumpSystem>    ( ).Enabled = false;
        World.Active.GetExistingManager<DamageSystem>  ( ).Enabled = false;
        pauseMenu.SetActive( true );
    }

    public void ResumeGame( )
    {
        IsPaused = false;
        World.Active.GetExistingManager<MovementSystem>( ).Enabled = true;
        World.Active.GetExistingManager<InputSystem>   ( ).Enabled = true;
        World.Active.GetExistingManager<JumpSystem>    ( ).Enabled = true;
        World.Active.GetExistingManager<DamageSystem>  ( ).Enabled = true;
        pauseMenu.SetActive( false );
    }

    public void ExitToMainMenu( )
    {
        ResumeGame( );
        SceneManager.LoadScene( "MainMenu" );
    }


    public void Update( )
    {
        var escape = Input.GetKeyUp( KeyCode.Escape );

        if( escape )
        {
            if( IsPaused ) ResumeGame( );
            else           PauseGame ( );
        }
    }

}
