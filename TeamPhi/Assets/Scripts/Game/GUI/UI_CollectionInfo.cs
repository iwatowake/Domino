using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI_CollectionInfo : MonoBehaviour {
	
	private Image[] mCollectedItemSlot;

	// Use this for initialization
	void Start () {
		List<Transform> slotList = new List<Transform>(); 

		int childCount = transform.childCount;
		for (int i=0; i<childCount; i++) 
		{
			slotList.Add(transform.GetChild(i));
		}

		slotList.Sort ((obj1, obj2) => (int)(obj1.transform.position.x - obj2.transform.position.x));

		foreach (Transform trns in slotList) 
		{
			trns.SetSiblingIndex(childCount - 1);
		}

		mCollectedItemSlot = GetComponentsInChildren<Image> ();
	}

//	public void SetItem(

}
