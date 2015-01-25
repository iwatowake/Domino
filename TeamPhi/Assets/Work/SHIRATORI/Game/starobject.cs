using UnityEngine;
using System.Collections;

public class starobject : MonoBehaviour {
	public GameObject particle;

	void OnTriggerEnter(Collider other){
		Debug.Log ("tes");
		if (other.collider.tag == "DOMINO") {
			Instantiate(particle, this.transform.position, this.transform.rotation);
			Destroy(this.gameObject);
		}
	}
}
