using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour
{
	public int dialogue;

	private BaseCharacter character;

	void Awake() {
		Dialoguer.Initialize();
	}

	void Start(){
		character = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseCharacter>();
	}

	void OnInteract(){
		if(gameObject.CompareTag("Animal")){
			if(character.FeralWhispers){
				UIManager.Instance.EnableControls(false);
				Dialoguer.StartDialogue(dialogue);
			}
		} else {
			UIManager.Instance.EnableControls(false);
			Dialoguer.StartDialogue(dialogue);
		}
	}
}
