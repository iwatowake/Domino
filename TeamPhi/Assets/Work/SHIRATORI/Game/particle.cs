using UnityEngine;
using System.Collections;

public class particle : MonoBehaviour {
	float mTime;

	void Start(){
		mTime = 0.0f;
	}
	// Update is called once per frame
	void Update () {
		mTime += Time.deltaTime;

		if (mTime > 2.0f)
			Destroy (this.gameObject);
	}
}
