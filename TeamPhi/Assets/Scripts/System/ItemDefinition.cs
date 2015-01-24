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
		default:
			throw new System.NotImplementedException ();
		}
	}
	
	
}
