using UnityEngine;
using System.Collections;

public class uchiDominoGenerator_Straight : MonoBehaviour
{
	public	Vector3 start;
	public	Vector3 offset;
	
	public GameObject domino;
	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i<100; i++) {
			Instantiate (this.domino
			, new Vector3 (
			this.start.x + this.offset.x * i
			                                       , this.start.y + this.offset.y * i
			                                       , this.start.z + this.offset.z * i)
			                                       , Quaternion.identity
			);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
