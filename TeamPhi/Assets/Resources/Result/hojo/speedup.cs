using UnityEngine;
using System.Collections;

public class speedup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				if (Input.GetKey (KeyCode.UpArrow)) {
						animation ["runUnitychan"].speed = 200;
				} else if (Input.GetKey (KeyCode.UpArrow)) {
						animation ["runUnitychan"].speed--;
				}
	}
}
