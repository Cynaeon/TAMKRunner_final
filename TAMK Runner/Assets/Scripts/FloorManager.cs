using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {

    public Transform[] m_aFloorPrefabs;

    public Transform m_tBaddiePrefab;
    public Transform m_tCoinPrefab;

    public float m_fFloorStartZ;
    public float m_fFloorEndZ;
    public float m_fMinMovementSpeed;
    public float m_fMaxMovementSpeed;

    public float m_fBadddieSpawnProb;
    public float m_fCoinSpawnInterval;

    public float m_fSpeedupInterval = 1.0f;
    public float m_fSpeedUpAmount;

    private float m_fMovementSpeed;
    private int m_iNumberOfParts;
    private float m_fBaddieSpawnEventTime;
    private float m_fCoinSpawnEventTime;

    private GameGlobals m_gcGameGlobals;

	// Use this for initialization
	void Start () {
        float fZPos = 0.0f;
        float fFloorLength = Mathf.Abs(m_fFloorStartZ) + Mathf.Abs(m_fFloorEndZ);
        m_fMovementSpeed = m_fMinMovementSpeed;

        Debug.Log("Floor length is: " + fFloorLength.ToString());

        while (fZPos > -(fFloorLength-15))
        {
            SpawnNewFloor(fZPos);
            fZPos -= 5.0f;
        }

        SpawnNewFloor(fZPos, false);
        fZPos -= 5.0f;
        SpawnNewFloor(fZPos, false);
        fZPos -= 5.0f;
        SpawnNewFloor(fZPos, false);
        fZPos -= 5.0f;

        m_fBaddieSpawnEventTime = Time.time;
        m_fCoinSpawnEventTime = Time.time;

        m_gcGameGlobals = GameGlobals.Instance;
    }


    public void SpawnNewFloor(float fZOffset, bool bRand=true)
    {
        Transform tFloorTransform = null;

        if (bRand)
            tFloorTransform = Instantiate(m_aFloorPrefabs[Random.Range(0, m_aFloorPrefabs.Length - 1)], new Vector3(0.0f, 0.0f, m_fFloorStartZ + fZOffset), Quaternion.identity) as Transform;
        else
            tFloorTransform = Instantiate(m_aFloorPrefabs[0], new Vector3(0.0f, 0.0f, m_fFloorStartZ + fZOffset), Quaternion.identity) as Transform;

        // Check we spawned something...
        if (null == tFloorTransform)
        {
            Debug.LogError("Unable to instantiate floor part");
            return;
        }

        // Get and check for the floor part controller
        ScrollingFloorItem gcFloorPart = tFloorTransform.GetComponent<ScrollingFloorItem>();
        if (null == gcFloorPart)
        {
            Debug.LogError("Prefab does not have a FloorPart component, unable to create the floor!");
            return;
        }

        // Set default params
        gcFloorPart.m_fEndZ = m_fFloorEndZ;
        gcFloorPart.m_fStartZ = m_fFloorStartZ + fZOffset;
        gcFloorPart.m_fMovementSpeed = m_fMovementSpeed;
        gcFloorPart.m_gcFloorManager = this;


        // Should we spawn a bad guy?
        if (bRand && (Random.Range(0.0f, 100.0f) > m_fBadddieSpawnProb))
        {
            Transform tBaddie = Instantiate(m_tBaddiePrefab, new Vector3(0.0f, 0.0f, m_fFloorStartZ + fZOffset), Quaternion.identity) as Transform;
            if (null != tBaddie)
            {
                // Reuse the variable we have
                gcFloorPart = tBaddie.GetComponent<ScrollingFloorItem>();

                // Set default params
                gcFloorPart.m_fEndZ = m_fFloorEndZ;
                gcFloorPart.m_fStartZ = m_fFloorStartZ + fZOffset;
                gcFloorPart.m_fMovementSpeed = m_fMovementSpeed;
                gcFloorPart.m_gcFloorManager = this;


                // Should we flip sides?
                if (Random.Range(0.0f, 100.0f) > 50.0f)
                {
                    tBaddie.GetComponent<SimpleBounceBetweenPoints>().FlipDirection();
                }
            }
        }
    }


    // Update is called once per frame
    void Update ()
    {
        if (m_gcGameGlobals.m_bPlayerIsAlive)
        {
            m_gcGameGlobals.m_fDistanceTravelled += m_fMovementSpeed * Time.deltaTime;

            if (Time.time - m_fBaddieSpawnEventTime >= m_fSpeedupInterval)
            {
                m_fMovementSpeed = Mathf.Clamp(m_fMovementSpeed + m_fSpeedUpAmount, m_fMinMovementSpeed, m_fMaxMovementSpeed);
                m_fBaddieSpawnEventTime = Time.time;
                SetSpeed(m_fMovementSpeed);
            }
        }
        else
        {
            m_fMovementSpeed = 0.0f;
            SetSpeed(m_fMovementSpeed);
        }
    }

    void SetSpeed(float fSpeed)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Floor Parts"))
        {
            ScrollingFloorItem gcFloorPart = go.GetComponent<ScrollingFloorItem>();
            if (null != gcFloorPart)
                gcFloorPart.m_fMovementSpeed = fSpeed;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Time.time - m_fCoinSpawnEventTime > m_fCoinSpawnInterval)
        {
            Vector3[] vaValidPositions = new Vector3[5];
            Vector3 vPos = new Vector3(-2.0f, 1.5f, 25.0f);
            bool bFoundASpawn = false;
            int iValidCount = 0;

            for (int i = 0; i < 5; i++)
            {
                if (Physics.Raycast(vPos, -Vector3.up, out hit, 3.0f))
                {
                    Debug.DrawLine(vPos, new Vector3(vPos.x, vPos.y - 2, vPos.z), Color.green, 1, false);
                    vaValidPositions[iValidCount] = vPos;
                    ++iValidCount;
                    bFoundASpawn = true;
                }
                else
                    Debug.DrawLine(vPos, new Vector3(vPos.x, vPos.y - 2, vPos.z), Color.red, 1, false);

                vPos.x += 1.0f;
            }


            if (bFoundASpawn)
            {
                // Opportunity to refactor here. We generically spawn ScrollingFloorItems several times...
                Transform tCoin = Instantiate(m_tCoinPrefab, vaValidPositions[Random.Range(0, iValidCount)], m_tCoinPrefab.rotation) as Transform;
                if (null != tCoin)
                {
                    // Reuse the variable we have
                    ScrollingFloorItem gcFloorPart = tCoin.GetComponent<ScrollingFloorItem>();

                    // Set default params
                    gcFloorPart.m_fEndZ = m_fFloorEndZ;
                    gcFloorPart.m_fStartZ = 25.0f;
                    gcFloorPart.m_fMovementSpeed = m_fMovementSpeed;
                    gcFloorPart.m_gcFloorManager = this;
                }
            }

            m_fCoinSpawnEventTime = Time.time;
        }
    }
}
