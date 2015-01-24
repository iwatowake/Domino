using UnityEngine;
using System.Collections;

public class ItemDefinition : MonoBehaviour
{
	public enum ItemKind
	{
		Bread,
		TextBook,
		CellPhone,
		Skirt,
		LipStick,

		None
	}
	
	public static int ScoreByItem (ItemKind kind)
	{
		switch (kind) {
		case ItemKind.Bread:
		case ItemKind.TextBook:
			return 300;
		case ItemKind.CellPhone:
			return 800;
		case ItemKind.Skirt:
			return 1200;
		case ItemKind.LipStick:
			return 1500;
		default:
			throw new System.NotImplementedException ();
		}
	}

	public static string GetItemIconPath(ItemKind kind)
	{
		switch (kind) {
		case ItemKind.Bread:
			return "Sprites/UI_Item_01";
		case ItemKind.TextBook:
			return "Sprites/UI_Item_02";
		case ItemKind.CellPhone:
			return "Sprites/UI_Item_03";
		case ItemKind.Skirt:
			return "Sprites/UI_Item_04";
		case ItemKind.LipStick:
			return "Sprites/UI_Item_05";

		case ItemKind.None:
			return "Sprites/UI_Item_00";

		default:
			throw new System.NotImplementedException ();
		}
	}
}
