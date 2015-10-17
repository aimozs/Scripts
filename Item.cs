using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]

public class Item : MonoBehaviour
{
	public enum itemTypes {key, quest, weapon, ammo};
	public itemTypes itemType;

	void OnInteract(GameObject interacter)
	{
		switch(itemType)
		{
		case itemTypes.weapon:
			interacter.GetComponent<BaseCharacter>().AddToWeapons(gameObject);
			break;
		default:
			interacter.GetComponent<BaseCharacter>().AddToInventory(gameObject);
			break;
		}
	}
}
