using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPart : ScrollingFloorItem {


    // Use this for initialization
    new void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	new void Update () {

        base.Update();

        if (m_tTransform.position.z <= m_fEndZ)
        {
            Destroy(gameObject);
            if (null != m_gcFloorManager)
                m_gcFloorManager.SpawnNewFloor(m_tTransform.position.z - m_fEndZ);
            else
                Debug.LogError("Floor part %s has no reference to the floor manager!");
        }
    }

}
