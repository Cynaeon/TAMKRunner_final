using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddieController : ScrollingFloorItem {

    public Transform m_tParticleEffect;

    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
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
                gc.KillPlayer();
                Instantiate(m_tParticleEffect, GetComponent<Transform>().position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
