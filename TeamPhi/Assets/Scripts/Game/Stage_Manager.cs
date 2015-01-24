using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Stage_manager : SingletonMonoBehaviour<Stage_manager>
{
	

	const int MAX_BALL = 10;
	const int MAX_DOMINO = 20;
	const int POINT_OF_STAR = 100;
	const int POINT_OF_REMAINING_BALL = 1000;
	
	
	private List<StageResult> StageResults;
	private List<ItemDefinition.ItemKind> CollectItems;
	private int CollectStarCount;
	
	
	private StageResult CurrentStage;
	
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
	
	
	public void Reset ()
	{
		this.StageResults.Clear ();
		this.CollectItems.Clear ();
		this.CollectStarCount = 0;
		this.CurrentStage = null;
	}
	
	public void StartStage (int ballCount)
	{
		if (this.CurrentStage != null) {
			this.StageResults.Add (this.CurrentStage);
			this.CurrentStage = null;
		}
		
		this.CurrentStage = new StageResult (ballCount);
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
			// call UI draw method
			// hoge.SetItemUI(kind, GetItemCount(kind));
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
		//call remaining ball UI draw method
	}
	
	private void DrawStaminaUI ()
	{
		// 0.0 ~ 1.0
		float stamina = CurrentStage == null ? 0.0f : CurrentStage.Stamina;
		
		//call remaining ball UI draw method
	}

		
	private int GetItemCount (ItemDefinition.ItemKind kind)
	{
		return  this.CollectItems.Count (k => k == kind);
	}
	


	
	public class StageResult
	{
		const int DOMINO_PER_SHOT = 30;
		public int RemainBall{ get; set; }
		private List<int> usedDominos; 		
		
		public StageResult (int balls)
		{
			this.RemainBall = balls;
			this.usedDominos = new List<int> ();
		}
		
		public void Shot (int usedDominoCount)
		{
			this.usedDominos.Add (usedDominoCount);
			this.PutDominoCount = 0;
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