using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// スコア表示
/// </summary>
public class UI_Score : MonoBehaviour {

	public	Text	mScoreText;
	
	public void SetScore(int score)
	{
		mScoreText.text = score.ToString ("00000");
	}

}
