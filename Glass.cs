using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]
public class Glass : MonoBehaviour, IBreakable, IInteractable {

	public GameObject brokenGlass;
	public AudioClip audioFx;

	public void OnInteract() {
		Break();
	}

	public GameObject GetGameObject(){
		return gameObject;
	}

	public void Break(){
		if(brokenGlass != null) {
			Instantiate(brokenGlass, transform.position, transform.rotation);
		}

		if(audioFx != null)
			SoundManager.PlaySound(audioFx);
		
		Destroy(gameObject);
	}
}
