using UnityEngine;
using System.Collections;

public class CustomInputManager : MonoBehaviour {

	private BaseCharacter character;

	// Use this for initialization
	void Start () {
		character = GetComponent<BaseCharacter>();

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Flashlight")) {
			character.ToggleFlashlight();
		}

		if(Input.GetButtonDown("nextDiscipline")) {
			if(character.disciplines.Count > 1){
				character.NextDiscipline();
			}
		}
		if(Input.GetButtonDown("Fire2")) {
			character.disciplines[character.currentDiscipline].Attack();
		}

	}
}
