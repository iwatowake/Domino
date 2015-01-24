using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// ゲージへのアクセス用コンポーネント
/// </summary>
public class UI_Gauge : MonoBehaviour {

	public Image mGauge;	//!< ゲージ中身部分
	public Image mFrame;	//!< ゲージ枠部分

	/// <summary>
	/// Sets the fill amount.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void SetFillAmount(float amount)
	{
		float clampedAmount = Mathf.Clamp01 (amount);
		mGauge.fillAmount = clampedAmount;
	}

	/// <summary>
	/// Sets the visible.
	/// </summary>
	/// <param name="visible">If set to <c>true</c> visible.</param>
	public void SetVisible(bool visible)
	{
		mGauge.enabled = visible;
		mFrame.enabled = visible;
	}

}
