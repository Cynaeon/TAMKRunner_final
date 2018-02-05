using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gs_MainMenuIn : GameState
{

    private float m_fEventTime;
    private enum tMenuState
    {
        _FadeIn,
        _Idle,
        _FadeOut,
    };

    private tMenuState iState = tMenuState._FadeIn;

    new void Start()
    {
        m_sStateName = "Main Menu";
        base.Start();

        m_fEventTime = Time.time;
    }

    // Update is called once per frame
    new void Update()
    {
        switch (iState)
        {
            case tMenuState._FadeIn:
                Debug.Log("Fade Menu In");
                iState = tMenuState._Idle;
                break;

            case tMenuState._Idle:
                Debug.Log("Wait For Input");
                iState = tMenuState._FadeOut;
                break;

            case tMenuState._FadeOut:
                Debug.Log("Fade menu out...");
                m_gcGameStateManager.ChangeState(GameState.tStateType._GameIn, "level1");
                break;
        }
    }
}
