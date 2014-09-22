using UnityEngine;
using System;
using System.Collections;

public class GameSettings : MonoBehaviour
{
	//level
	public string spawnTr;

	//character
	public string clan = "Human";
	string covenant;
	
	int curHealth = 100;
	int maxHealth = 100;

	//positions
	int x = 10;
	int y = 10;
	int barThickness = 25;
	float barHeightPos;
	int disBox = 75;

	//sizes
	public float healthBarLength;
	public float maxHealthBarLength;

	//styles
	public GUIStyle Health;

//awake is used at launch
	void Awake()
	{
		DontDestroyOnLoad(this);
		barHeightPos = (barThickness + y);
	}


// Use this for initialization
	void Start ()
	{

	}
	
// Update is called once per frame
	void Update ()
	{
		healthBarLength = (Screen.width/3*curHealth/maxHealth);
		maxHealthBarLength = (Screen.width/3);



	}

	void OnGUI()
	{
		//character
		GUI.Box(new Rect(x, y, maxHealthBarLength, barThickness), clan + covenant);

		GUI.Box(new Rect(x, barHeightPos, healthBarLength, barThickness), "", Health);
		GUI.Box(new Rect(x, barHeightPos, maxHealthBarLength, barThickness), (curHealth).ToString() +" / "+ (maxHealth).ToString());

	}

	public void AdjustCurHealth(int adj)
	{
		curHealth += adj;
		
		if(curHealth < 0)
			curHealth = 0;
		
		if(curHealth > maxHealth)
			curHealth = maxHealth;
		
		if(maxHealth < 1)
			maxHealth = 1;
		
		healthBarLength = maxHealthBarLength * (curHealth / maxHealth);
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
