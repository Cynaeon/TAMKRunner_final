using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingFloorItem : MonoBehaviour {

    public float m_fStartZ = 25.0f;
    public float m_fEndZ = -15.0f;
    public float m_fMovementSpeed;

    public FloorManager m_gcFloorManager;

    protected Transform m_tTransform;
    protected Vector3 m_vTrajectory;

    // Use this for initialization
    protected void Start()
    {
        m_tTransform = GetComponent<Transform>();
        m_tTransform.position = new Vector3(0.0f, 0.0f, m_fStartZ);
        m_vTrajectory = Vector3.zero;
        m_vTrajectory.z = -1;
    }

    // Update is called once per frame
    protected void Update()
    {
        m_tTransform.position += (m_vTrajectory * m_fMovementSpeed) * Time.deltaTime;
    }
}
