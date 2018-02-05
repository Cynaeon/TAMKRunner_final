using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gs_PlayButton : GameState {

    public void Play()
    {
        m_gcGameStateManager.ChangeState(GameState.tStateType._GameIn, "level1");
    }
}
