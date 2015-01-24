using UnityEngine;
using System.Collections;

public class uchiDomino : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		this.renderer.material.color = this.renderer.material.color.Offset (Random.Range (0, 360), 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
