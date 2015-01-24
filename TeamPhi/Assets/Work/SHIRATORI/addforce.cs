using UnityEngine;
using System.Collections;

public class addforce : MonoBehaviour {

	void Start(){
		Vector3 velocity = transform.forward * 100.0f;
		rigidbody.velocity = velocity;
	}
}
