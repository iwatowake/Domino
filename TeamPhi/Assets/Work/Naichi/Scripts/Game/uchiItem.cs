using UnityEngine;
using System.Collections;

public class uchiItem : MonoBehaviour
{
	public ItemDefinition.ItemKind ItemKind;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.rigidbody.WakeUp ();
		
		if (this.transform.position.y <= -1) {
			this.rigidbody.isKinematic = true;
			Stage_Manager.Instance.AddItem (this.ItemKind);
			Destroy (this.gameObject);
		}
		
	}
	
	
}
