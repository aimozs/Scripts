using UnityEngine;
using System.Collections;
using System;

public class GameSettings : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(this);
	}
// Use this for initialization
	void Start () {
	
	}
	
// Update is called once per frame
	void Update () {
	
	}
	
	public void SaveCharacterData(){
		GameObject player = GameObject.Find("Player");
		
//use that line below once if you change the way you save playerPrefs to clean it
		//PlayerPrefs.DeleteAll();
		
		PlayerCharacter pcClass = player.GetComponent<PlayerCharacter>();
//saving name
		PlayerPrefs.SetString("Player_Name", pcClass.Name);
//saving the attribute
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " baseVal", pcClass.GetPrimaryAttribute(cnt).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " xpToLvl", pcClass.GetPrimaryAttribute(cnt).ExpToLevel);
		}
//saving the skills
		
//saving disciplines
		
//
	}
	
	public void LoadCharacterData(){
	}
}
