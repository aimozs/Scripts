using UnityEngine;
using System.Collections;

public class Vent : MonoBehaviour, IBreakable, IInteractable {

	public GameObject brokenPieces;

	public void OnInteract(){

		Break();
	}

	public GameObject GetGameObject(){
		return gameObject;
	}

	public void Break(){
		if(brokenPieces != null) {
			Instantiate(brokenPieces, transform.position, transform.rotation);

		}
		Destroy(gameObject);
	}
}
