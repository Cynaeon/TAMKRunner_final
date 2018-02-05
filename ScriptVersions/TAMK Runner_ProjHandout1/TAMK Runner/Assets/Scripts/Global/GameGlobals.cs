using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGlobals : MonoBehaviour {

    private static GameGlobals instance;
    public static GameGlobals Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<GameGlobals>();

                if (!instance)
                {
                    Debug.LogError("Unable to find GameGlobals.Instance! Awake function possibly calling for .Instance before persistent objects hass been loaded?");
                }
            }

            return instance;
        }
    }


    //=======================================================================================


    public bool m_bPlayerIsAlive = true;
    public int m_iCurrentHighScore = 0;
    public int m_iMultiplier = 1;

    public GameStateManager m_gcGameStateManager;

    public void Awake()
    {
        Debug.Log("Game Globals Created");
    }


    // Use this for initialization
    void Start()
    {
    }
}
