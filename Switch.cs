using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class Switch : MonoBehaviour, IInteractable {

	public bool state;
	public MeshRenderer emitting;
	public List<GameObject> objects = new List<GameObject>();

	void Start(){
		ActivateObjects(state);
	}

	public void OnInteract(){
		ActivateObjects(!state);
		state = !state;
		
	}

	public GameObject GetGameObject(){
		return gameObject;
	}

	void ActivateObjects(bool on){
		if(objects.Count > 0){
			
			foreach(GameObject obj in objects){
				if(obj != null){
					Trap trap = obj.GetComponent<Trap>();
					if(!on && trap != null){
						GameObject lockedItem = trap.lockedItem;
						if(lockedItem.GetComponent<ILockable>() != null)
							lockedItem.GetComponent<ILockable>().Unlock();
					} else {
						obj.SetActive(on);
					}
				}
			}
		}

		if(emitting != null){
			if(on)
				emitting.material.SetColor("_EmissionColor", Color.white);
			else
				emitting.material.SetColor("_EmissionColor", Color.black);
		}
	}
}
