using UnityEngine;
using System.Collections;

public class ChangeRing : MonoBehaviour
{
	public GameObject TrueStar;
	// Use this for initialization
	void Start ()
	{
		GameObject newStar = Instantiate (TrueStar, this.transform.position, this.transform.rotation) as GameObject;
		newStar.transform.parent = this.transform.parent;
		Destroy (this.gameObject);
	}
}
