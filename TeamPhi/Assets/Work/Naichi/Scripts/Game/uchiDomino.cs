using UnityEngine;
using System.Collections;

public class uchiDomino : MonoBehaviour
{

	Transform body;

	// Use this for initialization
	void Start ()
	{
		this.body = this.transform.GetChild (0);
		
		Renderer bodyRenderer = this.body.GetComponent<Renderer> ().renderer;
		bodyRenderer.material.color = bodyRenderer.material.color.Offset (Random.Range (0, 360), 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.rigidbody.WakeUp ();
	}
}
