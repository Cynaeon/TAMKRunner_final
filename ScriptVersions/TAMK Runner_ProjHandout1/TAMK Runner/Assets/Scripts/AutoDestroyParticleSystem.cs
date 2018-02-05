using UnityEngine;
using System.Collections;

public class AutoDestroyParticleSystem : MonoBehaviour
{

    private ParticleSystem m_gcParticles;

    public void Start()
    {
        m_gcParticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_gcParticles.IsAlive(true))
        {
            Destroy(gameObject);
        }
    }
}