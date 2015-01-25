using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_FollowGauge : UI_Gauge {

	public	Camera			referenceCamera;
	private RectTransform 	rectTransform;

	void Start(){
		rectTransform = GetComponent<RectTransform> ();
	}
	
	void Update () {
		Vector3 pos = referenceCamera.ScreenToWorldPoint (Input.mousePosition);
		pos.z = 0;
		rectTransform.position = pos;
	}

}