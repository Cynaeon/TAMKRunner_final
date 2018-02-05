using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text m_gcDistanceText;
    public Text m_gcCoinsText;
    public Text m_gcGameOverText;

    private GameGlobals m_gcGameGlobals;

    void Start()
    {
        m_gcGameGlobals = GameGlobals.Instance;
    }

	// Update is called once per frame
	void Update () {
        m_gcDistanceText.text = m_gcGameGlobals.m_fDistanceTravelled.ToString() + "m";
        m_gcCoinsText.text = m_gcGameGlobals.m_iCoinsCollected.ToString();
        if (!m_gcGameGlobals.m_bPlayerIsAlive)
            m_gcGameOverText.text = "You Scored: " + (m_gcGameGlobals.m_fDistanceTravelled + m_gcGameGlobals.m_fDistanceTravelled  * m_gcGameGlobals.m_iCoinsCollected).ToString() + "\nPress Jump to Play Again";
        else
            m_gcGameOverText.text = "";
    }
}
