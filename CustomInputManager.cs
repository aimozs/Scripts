using UnityEngine;
using System.Collections;

public class CustomInputManager : MonoBehaviour {

	private float scroll;

	private static CustomInputManager instance;
	public static CustomInputManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<CustomInputManager>();
			}
			return instance;
		}
	}

	void Update () {
		if(GameSettings.PlayerBC != null){
			
			if(Input.GetButtonDown("interact")) {
				GameSettings.Interact();
			}

			if(Input.GetButtonDown("feed")) {
				GameSettings.PlayerBC.Feed();
			}

			if(Input.GetButtonDown("phone")) {
				UIManager.TogglePhone();
			}

			if(Input.GetButtonDown("Fire1")) {
				GameSettings.PlayerBC.activeWeapon.GetComponent<Weapon>().Attack();
			}

			if(Input.GetButtonDown("Fire2")) {
				GameSettings.PlayerBC.UseCurrentPower();
			}

			if(Input.GetButtonDown("crouch")){
				GameSettings.PlayerBC.Crouch();
			}

			if(Input.GetButtonDown("switchView")){
				CameraManager.Instance.SwitchPersonCamera();
			}

			float moveSides = Input.GetAxis ("Horizontal");
			float moveForward = Input.GetAxis ("Vertical");
			Vector3 movement = new Vector3 (moveForward, 0.0f, moveSides);

			GameSettings.PlayerBC.SetMoveSpeed(movement);

			scroll = Input.GetAxis("Mouse ScrollWheel");

			if(scroll > 0.01f) {
				GameSettings.PlayerBC.NextPower(true);
			}

			if(scroll < -0.01f){
				GameSettings.PlayerBC.NextPower(false);
			}

//			if(playerChar.weapons.Count > 1){
//				scroll = Input.GetAxis("Mouse ScrollWheel");
//				if(scroll > 0.01f) {
//					if(playerChar.currentWeapon < playerChar.weapons.Count-1) {
//						playerChar.currentWeapon++;
//						if(GameSettings.Instance.debugInput)
//							Debug.Log("[IM] next weapon");
//					} else {
//						playerChar.currentWeapon = 0;
//					}
//					playerChar.SwitchWeapon();
//				} else if(scroll < -0.01f) {
//					if(playerChar.currentWeapon > 0) {
//						playerChar.currentWeapon--;
//					} else {
//						playerChar.currentWeapon = playerChar.weapons.Count-1;
//					}
//					playerChar.SwitchWeapon();
//				}
//			}
		}
	}

//	public static void EnableControls(bool on) {
//		if(Instance.playerChar == null)
//			Instance.playerChar = GameSettings.Instance.player.GetComponent<BaseCharacter>();
//
//		GameSettings.PlayerBC.EnableControls();
//		if(Instance.playerChar != null){
//			Instance.playerChar.EnableControls(on);
//		}
//	}
}
