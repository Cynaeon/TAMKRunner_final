using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour
{
    protected GameStateManager m_gcGameStateManager;
	protected GameGlobals m_gcGameGlobals;
    protected string m_sStateName = "BaseClass";


    // Normally I would use the following pattern:
    //
    // SplashScreenEnter, SplashScreenIn, SplashScreenExit
    // MainMenuEnter, MainMenuIn, MainMenuExit, etc.
    // 
    // As there are normally animations, fade ins/outs, audio effects etc
    // that are used to transition in and out of each section of the game. 
    // 
    // It's easier to control these transitions in separate states...
    




    // This enum stops us sending random junk to GameStateManager::ChangeState

    public enum tStateType
    {
        _NULL,
        _SplashScreenIn,
        _MainMenuIn,
        _GameIn,
    }

    virtual public void Start()
    {
		Debug.Log("GameState: " + m_sStateName + "\n" );
		m_gcGameGlobals = GameGlobals.Instance;
        m_gcGameStateManager = m_gcGameGlobals.m_gcGameStateManager;
    }

    virtual public void Update()
    {
    }

	virtual public void LateUpdate()
	{
	}

    virtual public void OnDisable()
    {
//        Debug.Log("Exiting game state...\n");
    }
}
