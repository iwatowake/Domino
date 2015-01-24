using UnityEngine;
using System.Collections;

public class HomeButton :  ButtonBase{

	public override void OnPushed ()
	{
		FindObjectOfType<Result_Manager> ().StateEndCall ();
	}

}
