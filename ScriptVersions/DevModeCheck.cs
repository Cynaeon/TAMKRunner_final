using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevModeCheck : MonoBehaviour {

    public string m_sSceneName;

    void Awake()
    {
        GameObject obj = GameObject.Find("PersistentObjects");
        if (null == obj)
        {
            Debug.Log("Unable to find Persistent objects, loading the splashscreen...");
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene("SplashScreen");
        }
    }
}
