using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour, IInteractable {

	public GameObject spawn;
	public AnimationClip _anim;

	public void OnInteract (){
		if(_anim != null)
			GameSettings.PlayerBC.PlayAnim(_anim);
	}

	public GameObject GetGameObject(){
		if(spawn != null)
			return spawn;
		else
			return gameObject;
	}
}