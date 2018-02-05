using UnityEngine;
using System.Collections;

public class SimpleRotate : MonoBehaviour {

	public float m_fSpeed;
	private Transform m_gcTransform;

	public enum tSpinAxis
	{
		_X,
		_Y,
		_Z,
	}

	public tSpinAxis m_iAxis = tSpinAxis._X;

	// Use this for initialization
	void Start () {
		m_gcTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {


		Vector3 vRot = m_gcTransform.rotation.eulerAngles;

		switch (m_iAxis)
		{
			case tSpinAxis._X:
                vRot.x += m_fSpeed * Time.deltaTime;
				break;

			case tSpinAxis._Y:
                vRot.y += m_fSpeed * Time.deltaTime;
                break;

			case tSpinAxis._Z:
                vRot.z += m_fSpeed * Time.deltaTime;
                break;

		}
		m_gcTransform.rotation = Quaternion.Euler(vRot);

	}
}
