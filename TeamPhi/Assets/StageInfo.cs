using UnityEngine;
using System.Collections;

public class StageInfo : MonoBehaviour
{

	public int StageNo;

	public void StageClear ()
	{
		if (StageNo == 0)
			throw new UnityException ("StageNo UNknown");
		var gameManager = GameObject.FindObjectOfType<Game_Manager> ();
		gameManager.StageClear (this.StageNo);
	}
	
}
