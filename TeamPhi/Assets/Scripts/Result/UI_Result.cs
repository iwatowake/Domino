using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Result : MonoBehaviour {

	private static UI_Result instance;
	public static UI_Result Instance {
		get {
			if (instance == null) {
				instance = (UI_Result)FindObjectOfType (typeof(UI_Result));
				
				if (instance == null) {
					Debug.LogError ("UI_Game is nothing.");
				}
			}
			return instance;
		}
	}

	public	UI_NormaInfo	uiNorma;
	public	UI_Score		uiScore;
	public	UI_Score		uiBonus;

	public void SetNormaClearFromList(List<ItemDefinition.ItemKind> list)
	{
		uiNorma.SetNormaClearFromList (list);
	}

	public void SetNormaClear(ItemDefinition.ItemKind kind)
	{
		uiNorma.SetItemEnable (kind);
	}

	public void SetTotalScore(int score)
	{
		uiScore.SetScore (score);
	}

	public void SetBonus(int bonus)
	{
		uiBonus.SetScore (bonus);
	}
}
