using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : ScrollingFloorItem
{

    public Transform m_tParticleEffect;

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
                Instantiate(m_tParticleEffect, other.transform.position, Quaternion.identity);
                FloorManager fm = GameObject.Find("FloorManager").GetComponent<FloorManager>();
                fm.IncreaseSpeed();
                // Play an Audio Sting
                GameGlobals.Instance.m_iCoinsCollected++;
                Destroy(gameObject);
            }
        }
    }
}
