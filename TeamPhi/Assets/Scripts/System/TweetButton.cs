using UnityEngine;
using System.Collections;

public class TweetButton : ButtonBase {
	
	public override void OnPushed ()
	{
		TweetManager.Tweet ("「DREAMINO」で" + Stage_Manager.Instance.TotalScore +"点獲得！！", "http://globalgamejam.org/2015/games/dreamino", "ggj2015");
	}

}
