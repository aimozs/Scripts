using UnityEngine;
using System;
using System.Collections;

public class GameSettings : MonoBehaviour
{
	public string spawnTr;
//awake is used at launch
	void Awake()
	{
		DontDestroyOnLoad(this);
	}


// Use this for initialization
	void Start ()
	{

	}
	
// Update is called once per frame
	void Update ()
	{
		//Debug.Log(spawnTr);
	}
	
	public void SaveCharacterData()
	{
		GameObject player = GameObject.Find("First Person Controller");
		
	//use that line below once if you change the way you save playerPrefs to clean it
		//PlayerPrefs.DeleteAll();
		
	
		PlayerCharacter pcClass = player.GetComponent<PlayerCharacter>();
	//saving name
		PlayerPrefs.SetString("Player_Name", pcClass.Name);
		PlayerPrefs.SetString("Player_Clan", pcClass.Name);

	//saving the attribute
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
		{
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " baseVal", pcClass.GetPrimaryAttribute(cnt).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " xpToLvl", pcClass.GetPrimaryAttribute(cnt).ExpToLevel);
		}

	//saving the vitals
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
		{
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " baseVal", pcClass.GetVital(cnt).BaseValue);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " xpToLvl", pcClass.GetVital(cnt).ExpToLevel);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " CurrVal", pcClass.GetVital(cnt).CurValue);
			PlayerPrefs.SetString(((VitalName)cnt) + " Mods", pcClass.GetVital(cnt).GetModifyingAttributes());
		}

	//saving the vitals
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
		{
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " baseVal", pcClass.GetSkill(cnt).BaseValue);
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " xpToLvl", pcClass.GetSkill(cnt).ExpToLevel);
			PlayerPrefs.SetString(((SkillName)cnt) + " Mods", pcClass.GetSkill(cnt).GetModifyingAttributes());
		}
		
	//saving disciplines
		
	//saving the level and position
	}
	
	public void LoadCharacterData()
	{

	}
}
