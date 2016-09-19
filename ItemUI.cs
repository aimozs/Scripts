using UnityEngine;
using System.Collections;

public class ItemUI : MonoBehaviour {

	public GameObject itemGO;

	public void AddItToInventory(){
		if(itemGO != null){
			InventoryManager.AddToInventory(itemGO);

			Destroy(gameObject);
		}
	}

	public void DepositItInContainer(){
		if(itemGO != null){
			InventoryManager.DepositCurrentItemInContainer(itemGO);
			Destroy(gameObject);
		}
	}

	public void DisplayItemDetails(){
		InventoryManager.DisplayItemDetails(itemGO);
	}

	public void Use(){
//		Item item = itemGO.GetComponent<Item>();
		InventoryManager.Inspect(itemGO);
	}
}
