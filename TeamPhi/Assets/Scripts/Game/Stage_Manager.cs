using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Stage_Manager : SingletonMonoBehaviour<Stage_Manager>
{
	Camera stageCamera;

	const int MAX_BALL = 10;
	const int MAX_DOMINO = 20;
	const int POINT_OF_STAR = 100;
	const int POINT_OF_REMAINING_BALL = 1000;
	const float STOP_JUDGE_INTERVAL = 2.0f;

	private List<StageResult> StageResults;
	private List<ItemDefinition.ItemKind> CollectItems;
	private int CollectStarCount;
	public StageResult CurrentStage;
	
	public float lastFallTime;
	
	
	public void Awake ()
	{
		if (this != Instance) {
			Destroy (this);
			return;
		}
		
		DontDestroyOnLoad (this.gameObject);
		
		this.CollectItems = new List<ItemDefinition.ItemKind> ();
		this.StageResults = new  List<StageResult> ();
		this.Reset ();
	}
	
	public void OnGUI ()
	{
		if (GUI.Button (new Rect (10, 10, 100, 20), "Stage Clear")) {
			this.gameObject.GetComponent<StageInfo> ().StageClear ();
		}
	}
	
	public void Update ()
	{
		var systemManager = GameObject.FindObjectOfType<System_Manager> ();
		if (systemManager.m_eMenuKind_Now.ToString ().StartsWith ("Stage")) {
		
			if (this.stageCamera == null) {	
				GameObject go = ((GameObject)GameObject.FindGameObjectWithTag ("STAGE_CAMERA"));
				Debug.Log ("go" + go);
				if (go == null)
					return;
				this.stageCamera = go.GetComponent<Camera> ();
				Debug.Log ("cm" + stageCamera);
			}
			if (this.stageCamera == null) {
				return;
			}
		
			if (this.stageCamera != null) {
				this.stageCamera.transform.position = new Vector3 (
				this.stageCamera.transform.position.x,
				this.stageCamera.transform.position.y,
				Mathf.Lerp (this.stageCamera.transform.position.z, this.cameraTarget.z + 0.05f, 0.1f)
				);
			}
		
			if (CurrentStage.Shotted) {
				if (lastFallTime + STOP_JUDGE_INTERVAL <= Time.time) {
			
					foreach (GameObject domino in GameObject.FindGameObjectsWithTag("DOMINO")) {
						Destroy (domino);
					}
				
					if (this.CurrentStage.RemainBall > 0) {
						this.CurrentStage.NextShot ();
					} else {
						//gameover
						
						GameObject.FindGameObjectWithTag ("STAGE").GetComponent<StageInfo> ().StageClear ();
						
//						var gameManager = GameObject.FindObjectOfType<Game_Manager> ();
//						gameManager.StageClear ();
					}
								
				
						
					Debug.Log ("NextShot");
				}
			}
			
			
			this.DrawBallCountUI ();
			this.DrawItemUI ();
			this.DrawScoreUI ();
			this.DrawStaminaUI ();
			
		}
	}
	
	public void Reset ()
	{
		this.StageResults.Clear ();
		this.CollectItems.Clear ();
		this.CollectStarCount = 0;
		this.CurrentStage = null; 
		this.StartStage (5);
	}
	
	public void StartStage (int ballCount)
	{
		if (this.CurrentStage != null) {
			this.StageResults.Add (this.CurrentStage);
			this.CurrentStage = null;
		}
		
		var go = GameObject.Find ("StageCamera");//.FindGameObjectWithTag ("STAGE_CAMERA");
		this.stageCamera = go.GetComponent<Camera> ();
		Debug.Log ("Camera : " + this.stageCamera);
		this.CurrentStage = new StageResult (ballCount);
		this.CurrentStage.NextShot ();
	}
	
	// Exclude Item Bonus and Ball Bonus
	public int Score {
		get {
			return this.CollectStarCount * POINT_OF_STAR;
		}
	}
	
	
	public int TotalScore {
		get {
			int itemBonus = this.CollectItems.Sum (k => ItemDefinition.ScoreByItem (k));
			int remainingBallBonus = this.StageResults.Sum (s => s.RemainBall * POINT_OF_REMAINING_BALL); 
			
			return this.Score + itemBonus + remainingBallBonus;
		}	
	}
	

		
	public void AddItem (ItemDefinition.ItemKind kind)
	{
		Debug.Log ("Get Item " + kind.ToString ());

		this.CollectItems.Add (kind);
		
		this.DrawItemUI ();
	}
	
	public void AddStar ()
	{
		this.CollectStarCount++;
		this.DrawScoreUI ();
	}
	
	
	
	private void DrawItemUI ()
	{
		//Items
		foreach (ItemDefinition.ItemKind kind in Enum.GetValues(typeof(ItemDefinition.ItemKind))) {	
			UI_Game.Instance.SetCollectedItems (kind, GetItemCount (kind));
		}
	}
	
	private void DrawScoreUI ()
	{
		int score = this.Score;
		// call score UI draw method
	}
	
	private void DrawBallCountUI ()
	{
		int remainingBalls = CurrentStage == null ? 0 : CurrentStage.RemainBall;
		
		UI_Game.Instance.SetActionCount (remainingBalls);
		//call remaining ball UI draw method
	}
	
	public void DrawStaminaUI ()
	{
		// 0.0 ~ 1.0
		float stamina = CurrentStage == null ? 0.0f : CurrentStage.Stamina;
		UI_Game.Instance.SetLimitGaugeAmount (stamina);
//		Debug.Log ("Stamina:" + stamina);
		
		//call remaining ball UI draw method
	}

		
	private int GetItemCount (ItemDefinition.ItemKind kind)
	{
		return  this.CollectItems.Count (k => k == kind);
	}
	
	
	
	private Vector3 lastDominoPoint;
	private Vector3 cameraTarget;
	
	public void MoveCamera (GameObject domino)
	{
		if (this.stageCamera == null) {
			Debug.Log ("Camera is NULL!!!");
			return;
		}
		
		if (domino.transform.position.z > this.stageCamera.transform.position.z + 0.05f) {
			this.cameraTarget = new Vector3 (
				this.stageCamera.transform.position.x
				, this.stageCamera.transform.position.y
				, domino.transform.position.z + 0.05f);
		}
		
		lastDominoPoint = domino.transform.position;
		Debug.Log ("LastDominoPosition : " + lastDominoPoint);
				
		this.lastFallTime = Time.time;
	}
	
	
	
	public class StageResult
	{
		const int DOMINO_PER_SHOT = 30;
		public int RemainBall{ get; set; }
		private List<int> usedDominos; 		
		public bool Shotted = true;
		
		public StageResult (int balls)
		{
			this.RemainBall = balls;
			this.usedDominos = new List<int> ();
		}
		
		public bool CanShot {
			get{ return !this.Shotted;}
		}
		
		public void Shot ()
		{
			this.usedDominos.Add (this.PutDominoCount);
			this.PutDominoCount = 0;
			this.RemainBall--;
			Debug.Log ("RemainingBall=" + RemainBall);
			Stage_Manager.Instance.lastFallTime = Time.time;
			this.Shotted = true;
			
			Stage_Manager.Instance.DrawBallCountUI ();
		}
		
		public void NextShot ()
		{
			this.PutDominoCount = 0;
			this.Shotted = false;
			
			Stage_Manager.Instance.DrawBallCountUI ();
		}
		
		public bool Puttable {
			get {
				if (!this.CanShot)
					return false;
				return (DOMINO_PER_SHOT - this.PutDominoCount) > 0;
			}
		}
		
		public int PutDominoCount;
		public void PutDomino ()
		{
			this.PutDominoCount++;
			if (DOMINO_PER_SHOT - this.PutDominoCount < 0)
				throw new UnityException ("Ball count error");
			
		}
		
		public float Stamina {
			get {
				return (float)(DOMINO_PER_SHOT - PutDominoCount) / (float)DOMINO_PER_SHOT;
			}
		}
		
		
		
	}
	
	
	
}