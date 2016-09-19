using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour, IInteractable {

	public Ammo ammoWeapon;
	public Animation animWeapon;
	public AudioClip soundWeapon;
	public Sprite iconWeapon;


	public void OnInteract(){
		GameSettings.Instance.player.GetComponent<BaseCharacter>().AddToWeapons(gameObject);
	}

	public GameObject GetGameObject(){
		return gameObject;
	}
	
//	void GetAmmo(string nameWeapon) {
//		switch(nameWeapon) {
//		case "bow":
//			ammoWeapon = Resources.Load<Ammo>("arrow");
//			break;
//		default:
//			Debug.Log ("couldnt find the ammo going with that weapon");
//			break;
//		}
//	}

	public void Attack() {
		if(ammoWeapon != null) {
			GameObject bullet =  Instantiate(ammoWeapon.gameObject, transform.position, transform.rotation) as GameObject;
			bullet.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
			Destroy (bullet, 5);
			GameSettings.Instance.player.GetComponent<BaseCharacter>().AnimateRange();
		} else {
			GameSettings.Instance.player.GetComponent<BaseCharacter>().AnimateBrawl();
		}

//		if(soundWeapon != null)
//			SoundManager.Instance.PlaySound(soundWeapon);
	}


}