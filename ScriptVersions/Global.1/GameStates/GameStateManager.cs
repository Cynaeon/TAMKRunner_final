using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour 
{
	private static GameObject m_goGameState;
    private bool m_bWaitingForLoad = false;
    private GameState.tStateType m_iNewState = GameState.tStateType._NULL;

    void OnEnable()
    {
        //Tell our 'LevelWasLoaded' function to start listening for a scene change
        SceneManager.sceneLoaded += LevelWasLoaded;
    }

    void OnDisable()
    {
        //Tell our 'LevelWasLoaded' function to stop listening for a scene change - always remember to unsubscribe delegates
        SceneManager.sceneLoaded -= LevelWasLoaded;
    }

    void Awake()
	{
        // We know that this component is loaded in the splash screen, so it's the responsibility of this function 
        // to create an initial gamestate and then let it do its thing...
		DontDestroyOnLoad(GameObject.Find("PersistentObjects"));
        m_goGameState = new GameObject();
        DontDestroyOnLoad(m_goGameState);
        SetState(GameState.tStateType._SplashScreenIn);
    }

	
	void Start () 
	{
	
	}


    // We want to change to a new 'state' after the new scene has loaded. 
    // This function captures the Scenemanager's levelLoaded event and allows
    // the new state to do what ever setup it wants during the Awake/Start 
    // phase of the process cycle
 
	public void LevelWasLoaded(Scene scene, LoadSceneMode mode)
	{
        if (m_bWaitingForLoad)
        {
            SetState(m_iNewState);
        }
    }

    // -------------------------------------------------------------------

    // ChangeState can be called by any component, at any time, while this 
    // function is public. This is useful, but can lead to you making a mess. 
    // Don't do it! ;)
    //
    // In general the active GameState should be responsible for changing to a new state
    // and you restrict flow control code to those components. That's the point
    // of a state machine ;D
    // 
    public void ChangeState(GameState.tStateType iState, string sLevelName="" )
	{
        // Clean out the old state
		Destroy(m_goGameState);

        // Make a new, empty object and make sure it can't be deleted during the scene load (if there is one)
		m_goGameState = new GameObject();
		DontDestroyOnLoad(m_goGameState);		// Never destroy...


        // Load the new scene if we need it...
        // Our level loaded delegate will take over once the scene has loaded
		if (sLevelName != "")
		{
			Debug.Log("Attempting to load: " + sLevelName + "\n");
			m_bWaitingForLoad = true;
			m_iNewState = iState;
            SceneManager.LoadScene(sLevelName);
        }
        else
		{
            // No new scene to load, so change state immediately...
            SetState(iState);
        }
    }



    private void SetState(GameState.tStateType iState)
    {
        switch (iState)
        {
            case GameState.tStateType._SplashScreenIn:
                m_goGameState.AddComponent<gs_SplashScreenIn>();
                m_goGameState.name = "GS: Splash Screen";
                break;

            case GameState.tStateType._MainMenuIn:
                m_goGameState.AddComponent<gs_MainMenuIn>();
                m_goGameState.name = "GS: Main Menu";
                break;

            case GameState.tStateType._GameIn:
                m_goGameState.AddComponent<gs_GameIn>();
                m_goGameState.name = "GS: Game";
                break;

            case GameState.tStateType._NULL:
            default:
                Debug.Log("Change state called with an unknown game state");
                break;
        }
        m_iNewState = GameState.tStateType._NULL;
        m_bWaitingForLoad = false;
    }
}
