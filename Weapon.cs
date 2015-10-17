using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : Item 
{
	public string nameWeapon;
	public Sprite iconWeapon;
	public AudioClip soundWeapon;
	public Animation animWeapon;
	public Ammo ammoWeapon;

	private Transform spawnPoint;

	// Use this for initialization
	void Start () {
		itemType = itemTypes.weapon;
		nameWeapon = gameObject.name;
//		iconWeapon = Resources.Load<Sprite>(nameWeapon);
//		soundWeapon = Resources.Load<AudioClip>(nameWeapon);
//		if(ammoWeapon == null)
//			GetAmmo(nameWeapon);
		spawnPoint = GameObject.Find("spawnPoint").transform;
	}
	
	void Update () {
		if(Input.GetButtonDown("Fire1") && transform.parent.name == "fist") {
			Attack();	
		}
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
			GameObject bullet =  Instantiate(ammoWeapon.gameObject, spawnPoint.position, spawnPoint.rotation) as GameObject;
			bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
			Destroy (bullet, 5);
		}

		if(soundWeapon != null)
			SoundManager.Instance.PlaySound(soundWeapon);
	}
}