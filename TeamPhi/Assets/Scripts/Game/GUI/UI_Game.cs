using UnityEngine;
using System.Collections;

public class UI_Game : MonoBehaviour {

	private static UI_Game instance;
	public static UI_Game Instance {
		get {
			if (instance == null) {
					instance = (UI_Game)FindObjectOfType (typeof(UI_Game));

					if (instance == null) {
							Debug.LogError ("UI_Game is nothing.");
					}
			}
			return instance;
		}
	}

	public	UI_CollectionInfo	uiNorma;
	public	UI_CollectionInfo	uiCollection;
	public	UI_Time				uiTime;
	public	UI_Score			uiScore;
	public	UI_TouchCount		uiTouchCount;
	public	UI_FollowGauge		uiDominoGauge;

	void Start()
	{

	}

	/// <summary>
	/// 目標アイテムをUIに表示する
	/// </summary>
	public void SetNormaItems()
	{

	}

	/// <summary>
	/// 今まで集めたアイテムをUIに表示する
	/// </summary>
	public void SetCollectedItems(ItemDefinition.ItemKind kind,int count)
	{
		
	}

	/// <summary>
	/// プレイ時間をUIに表示する
	/// </summary>
	/// <param name="sec">時間(秒)</param>
	public void SetTime(float sec)
	{
		uiTime.SetTime (sec);
	}

	/// <summary>
	/// スコアをUIに表示する
	/// </summary>
	/// <param name="score">現在のスコア</param>
	public void SetScore(int score)
	{
		uiScore.SetScore (score);
	}

	/// <summary>
	/// 手数をUIに表示する(最大値も指定)
	/// </summary>
	/// <param name="current">現在値</param>
	/// <param name="max">最大値</param>
	public void SetActionCount(int current, int max)
	{
		uiTouchCount.SetNum (current, max);
	}

	/// <summary>
	/// 手数をUIに表示する
	/// </summary>
	/// <param name="current">現在値</param>
	public void SetActionCount(int current)
	{
		uiTouchCount.SetNum (current);
	}

	/// <summary>
	/// ドミノ置ける距離のゲージに現在値を反映する
	/// </summary>
	/// <param name="amount">現在値(0.0f～1.0f)</param>
	public void SetLimitGaugeAmount(float amount)
	{
		uiDominoGauge.SetFillAmount (amount);
	}

	/// <summary>
	/// ドミノ置ける距離のゲージの表示/非表示を切り替える
	/// </summary>
	/// <param name="visible"> <c>true</c> なら表示</param>
	public void SetLimitGaugeVisible(bool visible)
	{
		if (visible) {
			// 表示前にゲージリセット
			SetLimitGaugeAmount (1.0f);
		}

		uiDominoGauge.SetVisible (visible);
	}

	void Update()
	{

	}
}
