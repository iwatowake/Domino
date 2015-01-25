using UnityEngine;
using System.Collections;

public class TitleButton : MonoBehaviour {
	bool TitleClick = false;
	const float RayCastMaxDistance = 100.0f;
	public Camera camera;

	void Update(){
		checkTitlelogo ();
	}

	void checkTitlelogo(){
		if (Input.GetMouseButton (0)) {
			//Vector2 clickpos = Input.mousePosition;

			Ray ray = camera.ScreenPointToRay(Input.mousePosition);

			RaycastHit hitInfo;

			if(Physics.Raycast(ray, out hitInfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer("TitleLogo"))){
				TitleClick = true;
			}
		}
	}

	public bool getTitleClick(){
		return TitleClick;
	}
}
