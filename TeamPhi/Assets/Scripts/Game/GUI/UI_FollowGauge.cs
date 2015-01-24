using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_FollowGauge : UI_Gauge {

	private RectTransform rectTransform;

	void Start(){
		rectTransform = GetComponent<RectTransform> ();
	}
	
	void Update () {
		rectTransform.position = Input.mousePosition;
	}

}