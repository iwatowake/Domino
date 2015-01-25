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
		Debug.Log ("Item.Update");
		this.rigidbody.WakeUp ();
		
		if (Stage_Manager.Instance == null)
			return;
		
		if (this.transform.position.y <= -1) {
			this.rigidbody.isKinematic = true;  
			
			Stage_Manager.Instance.AddItem (this.ItemKind);

			Stage_Manager.Instance.GameOver();

//			GameObject.FindGameObjectWithTag ("STAGE").GetComponent<StageInfo> ().StageClear ();
			
			Destroy (this.gameObject);
		}
		
	}
	
	
}
