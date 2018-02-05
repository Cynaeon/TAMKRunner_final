using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gs_SplashScreenIn : GameState
{
    private float m_fEventTime;

    // Use this for initialization
    new void Start()
    {
        m_sStateName = "Splash Screen";
        base.Start();

        m_fEventTime = Time.time;

        // Check system locale and set language
        // Check for save games
        // Grab achievements and leaderboard entries from Steam (or where ever)
        // Check for player preferences file and set game globals
        // Check for game preferences and update Unity quality settings if required
    }

    // Update is called once per frame
    new void Update()
    {
        // We aren't doing anything in this scene yet
        if (Time.time - m_fEventTime > 1.0f)
            GameGlobals.Instance.m_gcGameStateManager.ChangeState(GameState.tStateType._MainMenuIn, "MainMenu");
    }
}
