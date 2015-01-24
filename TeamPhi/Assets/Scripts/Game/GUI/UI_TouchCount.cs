using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 手数のカウント
/// </summary>
public class UI_TouchCount : MonoBehaviour {

	public Text mMaxCountText;		//!< 最大数
	public Text mCurrentCountText;	//!< 現在数

	/// <summary>
	/// 初期化
	/// </summary>
	void Start()
	{
		mCurrentCountText.text = "0";
	}

	/// <summary>
	/// 手数をセットする
	/// </summary>
	/// <param name="current">現在数</param>
	/// <param name="max">最大数</param>
	public void SetNum(int current, int max)
	{
		if (current > max) {
			Debug.LogError("TouchCount: current > max");
		}

		mCurrentCountText.text = current.ToString ();
		mMaxCountText.text = max.ToString ();
	}

	public void SetNum(int current)
	{
		mCurrentCountText.text = current.ToString ();
	}
}