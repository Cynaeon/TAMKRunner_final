using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gs_GameIn : GameState {

	// Use this for initialization
	new void Start () {
        m_sStateName = "Game In";
        base.Start();
	}
	
	// Update is called once per frame
	new void Update () {
        if (!m_gcGameGlobals.m_bPlayerIsAlive)
        {
            // Temporary hack to let us dev!
            if (Input.GetButton("Jump"))
            {
                m_gcGameGlobals.SetGameDefaults();
                m_gcGameStateManager.ChangeState(GameState.tStateType._GameIn, "level1");
            }
        }
	}
}
