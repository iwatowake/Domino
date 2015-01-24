using UnityEngine;
using System.Collections;

public class ItemDefinition : MonoBehaviour
{
	public enum ItemKind
	{
		Note,
		Bag,
		Ribbon,
		Pen ,
		CellPhone,

		None
	}
	
	public static int ScoreByItem (ItemKind kind)
	{
		switch (kind) {
		case ItemKind.Note:
		case ItemKind.Pen:
			return 300;
		case ItemKind.Bag:
			return 800;
		case ItemKind.Ribbon:
			return 1200;
		case ItemKind.CellPhone:
			return 1500;
		default:
			throw new System.NotImplementedException ();
		}
	}

	public static string GetItemIconPath(ItemKind kind)
	{
		switch (kind) {
		case ItemKind.Note:
			return "Sprites/yshTestSprite";
		case ItemKind.Pen:
			return "Sprites/yshTestSprite";
		case ItemKind.Bag:
			return "Sprites/yshTestSprite";
		case ItemKind.Ribbon:
			return "Sprites/yshTestSprite";
		case ItemKind.CellPhone:
			return "Sprites/yshTestSprite";

		case ItemKind.None:
			return "Sprites/yshTestSprite";

		default:
			throw new System.NotImplementedException ();
		}
	}
}
