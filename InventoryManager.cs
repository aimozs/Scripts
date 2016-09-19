using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {

	public Text itemName;

	private static GameObject _currentItem;
	private static Container _currentContainer;

	private GameObject _inspectedItem;
	private static GameObject _inspectionHolder;


	private static InventoryManager instance;
	public static InventoryManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<InventoryManager>();
			}
			return instance;
		}
	}

	public static List<GameObject> PlayerInventory {
		get { return GameSettings.Instance.PlayerInventory; }
	}

	public static GameObject InspectionHolder {
		get { return _inspectionHolder; }
		set { _inspectionHolder = value; }
	}

	public static void SetCurrentContainer(Container container = null) {
//		get { return _currentContainer; }
		if(container == null && _currentContainer != null)
			_currentContainer.SetOpen = false;
		_currentContainer = container;
//		if(_currentContainer != null)
//			Debug.Log(_currentContainer.name);
//		else {
//			Debug.Log("Container is null");
//		}
	}

	public static void AddToInventory(GameObject itemGO){
		PlayerInventory.Add(itemGO);

		if(_currentContainer != null){
			_currentContainer.contents.Remove(itemGO);
			UIManager.CreateItemUI("inventory", itemGO);
		}

		Item newItem = itemGO.GetComponent<Item>();
		if(newItem != null && newItem.increaseProg)
			Globals.IncrementProgress(newItem.conviction, newItem.toStep);
	}

	public static void DepositCurrentItemInContainer(GameObject itemGO = null){
		if(itemGO == null)
			itemGO = _currentItem;

		PlayerInventory.Remove(itemGO);

		if(_currentContainer != null){
			_currentContainer.Deposit(itemGO);
			UIManager.CreateItemUI("container", itemGO);
		} else {
			itemGO.transform.SetParent(null);
			itemGO.transform.localScale = Vector3.one;
		}
	}

	public static void UseCurrentItem(){
		Inspect(_currentItem);
	}

	public static void Inspect(GameObject item){
		if(Instance._inspectedItem != null){
			bool sameItem = Instance._inspectedItem.name == item.name + ("(Clone)");
			if(!sameItem){
				Destroy(Instance._inspectedItem);
				InstantiateInspectedItem(item);
			} else {
				Destroy(Instance._inspectedItem);
			}
		} else {
			InstantiateInspectedItem(item);
		}

	}

	public static void InstantiateInspectedItem(GameObject item){
		Instance._inspectedItem = (GameObject)Instantiate(item);
		Instance._inspectedItem.transform.SetParent(InspectionHolder.transform);
		Instance._inspectedItem.transform.localPosition = Vector3.zero;
		Instance._inspectedItem.transform.localRotation = Quaternion.Euler(Vector3.zero);
	}

	public static void HideInspectedItem(){
		Destroy(Instance._inspectedItem);
	}


	public static void DisplayItemDetails(GameObject item){
		_currentItem = item;
		Instance.itemName.text = _currentItem.name;
	}
}
