using UnityEngine;
using System.Collections;

public class uchiPutDomino : MonoBehaviour
{
	private Camera stageCamera;
	// 生成したいPrefab
	public GameObject DominoPrefab;

	// クリックした位置座標
	private GameObject firstDomino;
	private GameObject lastDomino;
	private bool isFirstDomino;
	
	
	//domino to domino distance.
	private float DominoInterval = 0.02f;
	private float maxRadian = 30.0f * Mathf.Deg2Rad;	
	
	private float timeInterval = 0.03f;
	private float lastPutted = 0;
		
	private bool isPressing;

	void Start ()
	{
		GameObject go = ((GameObject)GameObject.FindGameObjectWithTag ("STAGE_CAMERA"));
		this.stageCamera = go.GetComponent<Camera> ();
		
		this.reset ();
	}
	
	void OnGUI ()
	{
	}
	
	void reset ()
	{
		this.firstDomino = null;
		this.lastDomino = null;
		this.isFirstDomino = true;
		this.isPressing = false;
		
		foreach (var domino in	GameObject.FindGameObjectsWithTag("DOMINO")) {
			Destroy (domino);
		}
	} 
	
	void Update ()
	{
		if (this.stageCamera == null) {	
			GameObject go = ((GameObject)GameObject.FindGameObjectWithTag ("STAGE_CAMERA"));
			Debug.Log ("go2" + go);
			this.stageCamera = go.GetComponent<Camera> ();
			Debug.Log ("cm2" + stageCamera);
		}
		if (this.stageCamera == null) {
			return;
		}
		
		
		if (Input.GetKeyDown (KeyCode.R)) {
			this.reset ();
		}
				
		if (Input.GetMouseButton (0)) {	
			
			this.isPressing = true;
			UI_Game.Instance.SetLimitGaugeVisible (true);
			Debug.Log ("Call Camera:" + this.stageCamera);
			Stage_Manager.Instance.DrawStaminaUI ();
			
			if (lastPutted + timeInterval > Time.time) {
				return;
			}
			
			
			Ray ray = this.stageCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				var target = new Vector3 (hit.point.x, hit.point.y, hit.point.z); 
				
				//1つめのドミノお置くトコロ
				Debug.Log ("First domino : " + this.firstDomino);
				if (firstDomino == null) {
					
					if (Stage_Manager.Instance.CurrentStage.Puttable) {
						
						//置く
						
						GameObject domino = (GameObject)Instantiate (DominoPrefab, target, Quaternion.identity);
						domino.transform.Rotate (new Vector3 (0, 180, 0));
						this.firstDomino = domino;
						this.lastDomino = domino;
						this.isFirstDomino = true;
						
						//Putを増やす
						
						Stage_Manager.Instance.CurrentStage.PutDomino ();
						//UI更新
						Stage_Manager.Instance.DrawStaminaUI ();
					}
					
					
					
				}
				
				//last domino to click pos.
				var heading = target - this.lastDomino.transform.position;
				// ignore height.
				heading.y = 0;
				
				//check last to click distance
				if (heading.sqrMagnitude < DominoInterval * DominoInterval) {
					//click pos is too nearly.
				} else {
					if (this.isFirstDomino) {					
						Vector3 relativef = firstDomino.transform.InverseTransformPoint (target);
						float anglef = Mathf.Atan2 (relativef.x, relativef.z);// * Mathf.Rad2Deg;					
						anglef = anglef < 0f ? anglef + Mathf.PI * 2 : anglef;
						anglef -= Mathf.PI; 
						float anglef2 = anglef * Mathf.Rad2Deg;
						
						Vector3 now = this.firstDomino.transform.localRotation.eulerAngles;
						now.y += anglef2;
						this.firstDomino.transform .localRotation = Quaternion.Euler (now);
						
						this.isFirstDomino = false;
						
						return;
					}
					
					
					
					var distance = heading.magnitude;
					var direction = heading / distance;
					
					Vector3 relative = lastDomino.transform.InverseTransformPoint (target);
					
					float lastAngle = Mathf.Atan2 (lastDomino.transform.forward.x, lastDomino.transform.forward.z);
					lastAngle = lastAngle < 0f ? lastAngle + Mathf.PI * 2 : lastAngle;
					lastAngle -= Mathf.PI;
					float lastAngle2 = -lastAngle * Mathf.Rad2Deg;
					
					float angle = Mathf.Atan2 (relative.x, relative.z);// * Mathf.Rad2Deg;					
					angle = angle < 0f ? angle + Mathf.PI * 2 : angle;
					angle -= Mathf.PI;
					
					if (Mathf.Abs (angle) > this.maxRadian) {
						angle = angle < 0f ? -maxRadian : maxRadian;
					}
					
					float angle2 = -angle * Mathf.Rad2Deg;
					
					float newAngle = lastAngle2 + angle2;
					
					Vector3 move = new Vector3 (Mathf.Sin (newAngle * Mathf.Deg2Rad) * DominoInterval, 0f, Mathf.Cos (newAngle * Mathf.Deg2Rad) * DominoInterval);					
					
					Vector3 newPos = new Vector3 (
						lastDomino.transform.position.x - move.x
						, 0
						, lastDomino.transform.position.z + move.z);
					
					//2つめ以降を置く
					
					if (Stage_Manager.Instance.CurrentStage.Puttable) {
						
						//置く
						
						
						GameObject domino = (GameObject)Instantiate (DominoPrefab, newPos, Quaternion.identity);
						domino.transform.LookAt (this.lastDomino.transform.position);
						
						this.lastDomino = domino;
						
						//Putを増やす
						Stage_Manager.Instance.CurrentStage.PutDomino ();
						//UI更新
						Stage_Manager.Instance.DrawStaminaUI ();
					}
					
					
				}
				
			}
		} else {
			if (isPressing) {
				Vector3 now = this.firstDomino.transform.localRotation.eulerAngles;
				now.x -= 20;
				this.firstDomino.transform .localRotation = Quaternion.Euler (now);
				Debug.Log ("Shot!!!!");
				Stage_Manager.Instance.CurrentStage.Shot ();
				this.isPressing = false;
				UI_Game.Instance.SetLimitGaugeVisible (false);
			}
			
			
		}
		
	}
}