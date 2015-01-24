using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 時間表示
/// </summary>
public class UI_Time : MonoBehaviour {

	public Text mTimeText;

	/// <summary>
	/// 秒単位を分：秒で表示
	/// </summary>
	/// <param name="time">時間(秒)</param>
	public void SetTime(float time)
	{
		float minutes = time / 60.0f;
		minutes = Mathf.Floor (minutes);
		float sec = Mathf.Floor(time % 60.0f);

		mTimeText.text = minutes + ":" + sec.ToString("00");
	}
}
