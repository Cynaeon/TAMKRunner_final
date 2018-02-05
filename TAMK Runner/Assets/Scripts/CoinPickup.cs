using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : ScrollingFloorItem
{

    new void Start()
    {
        m_tTransform = GetComponent<Transform>();
        m_vTrajectory = Vector3.zero;
        m_vTrajectory.z = -1;
    }

    new void Update()
    {
        base.Update();
        if (m_tTransform.position.z <= m_fEndZ)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovementController gc = other.gameObject.GetComponent<PlayerMovementController>();
            if (null != gc)
            {
                // Spawn a new particle effect
                // Play an Audio Sting
                GameGlobals.Instance.m_iCoinsCollected++;
                Destroy(gameObject);
            }
        }
    }
}
