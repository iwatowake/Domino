using UnityEngine;
using System.Collections;

public class GameTimer {

	public enum eTYPE{
		NORMAL,
		LOOP,
		LOOP_SMOOTH
	}

	public float limitTime 		= 0;
	public float currentTime	= 0;
	public eTYPE type = eTYPE.NORMAL;

	public bool isTimeUp{
		get{
			if(currentTime >= limitTime)
			{
				return true;
			}else{
				return false;
			}
		}
	}

	public float timeRate{
		get{
			return Mathf.Clamp01( currentTime / limitTime );
		}
	}

	public float inverseTimeRate{
		get{
			return Mathf.Clamp01( limitTime - currentTime / limitTime);
		}
	}

	public bool Update(){

		currentTime += Time.deltaTime;

		if (isTimeUp) {
			switch(type)
			{
			case eTYPE.NORMAL:
				return true;

			case eTYPE.LOOP:
				Reset();
				return true;

			case eTYPE.LOOP_SMOOTH:
				float overHead = currentTime - limitTime;
				Reset();
				currentTime = overHead;
				return true;

			default:
				return true;
			}
		}else{
			return false;
		}
	}

	public void Reset(){
		currentTime = 0;
	}
}
