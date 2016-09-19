using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Container : MonoBehaviour, ILockable, IContainer<GameObject> {

	public bool isLocked;
	public string code;
	public GameObject key;
	public bool deleteKeyOnUnlock = true;
	public int safeLevel = 5;

	public Trap trap;

	public List<GameObject> contents = new List<GameObject>();

	private GameObject player;
	private bool _open = false;

	void Start(){
		if(trap == null)
			trap = GetComponentInChildren<Trap>();
		
		if(player == null)
			player = GameSettings.Instance.player;
	}

	void OnInteract(){
		if(isLocked){
			if(code != null && code != "")
				DialACode.Instance.Init(gameObject, code);
			else {
				if(key != null && InventoryManager.PlayerInventory.Find(obj=>obj.name==key.name) != null) {
					Unlock();
				} else {
					if(safeLevel < Globals.Instance.playerData.strength + player.GetComponent<BaseCharacter>().strengthBonus){
						Unlock();
						UIManager.Notify("You used your unnatural strength to force the safe");
					} else {
						UIManager.Notify("This lock seems stronger than usual..");
					}

					if(trap != null){
						trap.ApplyDamage();
					}

				}
			}
		} else {
			if(_open){
				Close();
			} else {
				Open();
			}
		}

	}

	public void Unlock(){
		isLocked = false;

		Trap trap = GetComponentInChildren<Trap>();
		if(trap != null)
			trap.gameObject.SetActive(false);

		if(key != null && player != null) {
			if(InventoryManager.PlayerInventory.Contains(key) && deleteKeyOnUnlock) {
				InventoryManager.PlayerInventory.Remove(key);
			}
		}
//		GetComponent<MeshRenderer>().material.color = Color.green;
	}

	public void Lock(){}

	public void Open(){
		SetOpen = true;
		InventoryManager.SetCurrentContainer(this);
		UIManager.OpenContainer(contents);
	}

	public void Close(){
		SetOpen = false;
//		InventoryManager.SetCurrentContainer();
		UIManager.CloseContainer();
//		UIManager.ShowPhone(false);
	}

	public bool SetOpen{
		get { return _open; }
		set { _open = value; }
	}

	public void Take (){}

	public void Deposit (GameObject item){
		contents.Add(item);
	}
}
