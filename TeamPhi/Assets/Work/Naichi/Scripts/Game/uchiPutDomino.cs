using UnityEngine;
using System.Collections;

public class uchiPutDomino : MonoBehaviour
{
	// 生成したいPrefab
	public GameObject DominoPrefab;

	// クリックした位置座標
	private GameObject firstDomino;
	private GameObject lastDomino;
	private bool isFirstDomino;
	
	//domino to domino distance.
	private float DominoInterval = 0.02f;
	private float maxRadian = 30.0f * Mathf.Deg2Rad;	
	void Start ()
	{
	}
		
	void Update ()
	{
		if (Input.GetMouseButton (0)) {		
		
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				var target = new Vector3 (hit.point.x, hit.point.y, hit.point.z); 

				//first Domino
				if (firstDomino == null) {
					GameObject domino = (GameObject)Instantiate (DominoPrefab, target, Quaternion.identity);
					domino.transform.Rotate (new Vector3 (0, 180, 0));
					domino.rigidbody.isKinematic = true;
					this.firstDomino = domino;
					this.lastDomino = domino;
					this.isFirstDomino = true;
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
						
						this.firstDomino.transform.Rotate (new Vector3 (0, anglef2, 0));
						
						Debug.Log (anglef2);
						this.isFirstDomino = false;
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

//					Debug.Log (lastAngle2.ToString ("#.#") + " + " + angle2.ToString ("#.#") + " = " + newAngle.ToString ("#.#"));
					
					
					Vector3 move = new Vector3 (Mathf.Sin (newAngle * Mathf.Deg2Rad) * DominoInterval, 0f, Mathf.Cos (newAngle * Mathf.Deg2Rad) * DominoInterval);					

//					Debug.Log (move * 100);
							
					Vector3 newPos = new Vector3 (
					 lastDomino.transform.position.x - move.x
					 , 0
					 , lastDomino.transform.position.z + move.z);
								
					GameObject domino = (GameObject)Instantiate (DominoPrefab, newPos, Quaternion.identity);
					domino.transform.LookAt (this.lastDomino.transform.position);

					this.lastDomino = domino;
				}

			}
		}
		
	}
}