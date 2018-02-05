using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour 
{
	private static GameObject m_goGameState;
    private bool m_bWaitingForLoad = false;
    private GameState.tStateType m_iNewState = GameState.tStateType._NULL;
    private string m_sDevModeSceneToLoad = "";

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
        // This must never die!
        DontDestroyOnLoad(GameObject.Find("PersistentObjects"));

        // Check for Dev Mode
        GameObject obj = GameObject.Find("DevModeCheck");
        if (null != obj)
        {
            Debug.Log("Found a DevModeCheck from open scene, entering dev mode...");
            DevModeCheck scene = obj.GetComponent<DevModeCheck>();
            m_sDevModeSceneToLoad = scene.m_sSceneName;
            Destroy(obj);
        }
    }

	
	void Start () 
	{
        if (m_sDevModeSceneToLoad != "")
        {
            // Did Awake find a dev mode check game object?
            ChangeState(GameState.tStateType._GameIn, m_sDevModeSceneToLoad);
            m_sDevModeSceneToLoad = "";
        }
        else
        {
            // No? Start up as normal then
            DontDestroyOnLoad(m_goGameState);
            SetState(GameState.tStateType._SplashScreenIn);
        }
    }


    // We want to change to a new 'state' after the new scene has loaded. 
    // This function captures the Scenemanager's levelLoaded event and allows
    // the new state to do what ever setup it wants during the Awake/Start 
    // phase of the cycle
	public void LevelWasLoaded(Scene scene, LoadSceneMode mode)
	{
        if (m_bWaitingForLoad)
        {
            SetState(m_iNewState);
        }
    }

    // -------------------------------------------------------------------
    // 
    public void ChangeState(GameState.tStateType iState, string sLevelName="" )
	{
        // Clean out the old state
		Destroy(m_goGameState);

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
        // Make a new, empty object and make sure it can't be deleted during the scene load (if there is one)
        m_goGameState = new GameObject();
        DontDestroyOnLoad(m_goGameState);       // Never destroy...

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
