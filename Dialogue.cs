using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NPC))]
public class Dialogue : MonoBehaviour
{
	public int dialogue;

	private BaseCharacter playerBC;

//	void Awake() {
//		Dialoguer.Initialize();
//	}
//	
//
//	void OnInteract(){
//		SetDialoguerGlobals();
//		if (playerBC == null)
//			playerBC = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseCharacter>();
//
//		if(playerBC != null){
//			if(gameObject.CompareTag("Animal")){
//				if(playerBC.FeralWhispers){
//					CustomInputManager.EnableControls(false);
//					Dialoguer.StartDialogue(dialogue);
//					UIManager.DisplayCursor(true);
//				} else {
//					UIManager.Notify("That's a animal.. you can't talk to animals..");
//				}
//			} else {
//				Debug.Log ("START DIALOGUE");
//				CustomInputManager.EnableControls(false);
//				SoundManager.Instance.EnableFX(false);
//				Dialoguer.StartDialogue(dialogue);
//				UIManager.DisplayCursor(true);
//
//			}
//		} else {
//			Debug.LogWarning("Cant start dialogue because we cant find the player's character");
//		}
//	}
//
//	void SetDialoguerGlobals(){
//		Dialoguer.SetGlobalString(0, SceneManager.GetActiveScene().name);
//		Dialoguer.SetGlobalString(1, Globals.Instance.playerData.spectrum1);
//		Dialoguer.SetGlobalString(2, Globals.Instance.playerData.spectrum2);
//		Dialoguer.SetGlobalString(3, Globals.Instance.playerData.spectrum3);
//
////		Debug.Log((float)Globals.Instance.playerData.progPride);
//		UIManager.Notify("prog sloth " + Globals.Instance.playerData.progSloth);
//		Dialoguer.SetGlobalFloat(0, (float)Globals.Instance.playerData.progAnger);
//		Dialoguer.SetGlobalFloat(1, (float)Globals.Instance.playerData.progEnvy);
//		Dialoguer.SetGlobalFloat(2, (float)Globals.Instance.playerData.progGluttony);
//		Dialoguer.SetGlobalFloat(3, (float)Globals.Instance.playerData.progGreed);
//		Dialoguer.SetGlobalFloat(5, (float)Globals.Instance.playerData.progLust);
//		Dialoguer.SetGlobalFloat(4, (float)Globals.Instance.playerData.progPride);
//		Dialoguer.SetGlobalFloat(6, (float)Globals.Instance.playerData.progSloth);
//
//	}
}
