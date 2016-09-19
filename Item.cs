using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

public class Item : MonoBehaviour, ICollectable, IInteractable {

	public float weight;
	public float price;
	public string notification;
	public bool increaseProg = false;
	public Globals.Conviction conviction;
	public int toStep = 0;

	public void OnInteract(){
		PickUp();

		if(notification != ""){
			UIManager.Notify(notification);
		}
	}

	public GameObject GetGameObject(){
		return gameObject;
	}

	public void PickUp(){
		AddToInventory();
//		Destroy(gameObject);
	}

	public void AddToInventory(){
		InventoryManager.AddToInventory(gameObject);
	}

}
