using UnityEngine;
using System.Collections;

public class DominoRotate : MonoBehaviour {

	private		Vector3			m_vecRotate	= Vector3.zero;

	void	Awake() {
		m_vecRotate	= transform.localRotation.eulerAngles;
	}

	// Update is called once per frame
	void Update () {
		m_vecRotate.y				+= (Time.deltaTime *180f);
		transform.localRotation		= Quaternion.Euler( m_vecRotate);
	}
}
