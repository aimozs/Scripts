using UnityEngine;
using System.Collections;
using System;						//used for the enum class

public class CharacterGenerator : MonoBehaviour {

//declare variables and constantes
	private PlayerCharacter _toon;
	private const int STARTING_POINTS = 35;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 1;
	private int expPointsLeft;
	
	private const int Offset = 5;
	private const int LINE_HEIGHT = 20;
	
	private int statStartingPos = 40;
	
	private const int StatLabelWidth = 150;
	private const int BUTTON_WIDTH = 25;
	private const int BASEVALUE_LABEL_WIDTH = 30;
	
	public GameObject playerPrefab;
	public bool menu = true;
	
	
//GUI style
	
	public GUISkin	VentrueSkin;
	
	
// Use this for initialization
	void Start () {
//create the character
		GameObject pc = Instantiate(playerPrefab, new Vector3(778,37,480), Quaternion.identity) as GameObject;
		pc.name = "Player";
		DontDestroyOnLoad(pc);
//		_toon = new PlayerCharacter();
//		_toon.Awake();
		_toon = pc.GetComponent<PlayerCharacter>();
		
		expPointsLeft = STARTING_POINTS;

//assign the base value to attribute the first time
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
			_toon.GetPrimaryAttribute(cnt).BaseValue = MIN_STARTING_ATTRIBUTE_VALUE;
		}
		_toon.StatUpdate();

	}
	
// Update is called once per frame
	void Update () {
		
	}
	
//will be display as UI

		void OnGUI(){
			if (menu == true){
			GUI.skin = VentrueSkin;
			DisplayName();
			DisplayVitals();
			DisplayExpPointsAvail();
			DisplayAttribute();
			DisplaySkills();
			DisplayCreateButton();
			}
		}
	
// first line (40), name, points to spend on stats
	private void DisplayName() {
		GUI.Label(new Rect(	Offset, 					//x
							statStartingPos,			//y
							StatLabelWidth,				//Length
							LINE_HEIGHT),				//height
							"Name:");					//object
		_toon.Name = GUI.TextArea(new Rect(	StatLabelWidth + Offset*2,
											statStartingPos,
											StatLabelWidth,
											LINE_HEIGHT),
											_toon.Name);
		
	}

	private void DisplayExpPointsAvail() {
		GUI.Label(new Rect(	StatLabelWidth*2 + Offset *3,
							statStartingPos,
							StatLabelWidth,
							LINE_HEIGHT),
							"Experience Points: " + expPointsLeft.ToString());
		
		
	}

//1st column, maxHealth, humanity, and maxVitae
	private void DisplayVitals() {
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) {
			GUI.Label(new Rect(	Offset,
								70 + LINE_HEIGHT*cnt,
								StatLabelWidth,
								LINE_HEIGHT),
								((VitalName)cnt).ToString());
			
			GUI.Label(new Rect(	Offset+StatLabelWidth,
								70 + LINE_HEIGHT*cnt,
								BASEVALUE_LABEL_WIDTH,
								LINE_HEIGHT),
								_toon.GetVital(cnt).AdjustedBaseValue.ToString()) ;
			
		}
	}
//2nd line (250 from top +30 for each attribute), Intelligence etc...
	private void DisplayAttribute() {
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
//display Attribute Name
			GUI.Label(new Rect(	Offset,
								250+LINE_HEIGHT*cnt,
								StatLabelWidth,
								LINE_HEIGHT),
								((AttributeName)cnt).ToString() );
//display minus button
			if(GUI.Button(new Rect(	Offset*2 + StatLabelWidth,
									250+LINE_HEIGHT*cnt,
									BUTTON_WIDTH,
									LINE_HEIGHT),
									"-")){
//transfer back to expPoints if click
				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
					_toon.GetPrimaryAttribute(cnt).BaseValue--;
					expPointsLeft = expPointsLeft + _toon.GetPrimaryAttribute(cnt).BaseValue + 1;
					_toon.StatUpdate();
				}
			}
//display the attribute value
			GUI.Label(new Rect(	Offset*4 + StatLabelWidth + BUTTON_WIDTH,
								250+LINE_HEIGHT*cnt,
								BASEVALUE_LABEL_WIDTH,
								LINE_HEIGHT),
								_toon.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());
//display plus button
			if(GUI.Button(new Rect(	Offset*6 + StatLabelWidth + BUTTON_WIDTH + BASEVALUE_LABEL_WIDTH,
									250+LINE_HEIGHT*cnt,
									BUTTON_WIDTH,
									LINE_HEIGHT),
									"+")) {
//add point to Attribute and substract from expPoints
				if(expPointsLeft > _toon.GetPrimaryAttribute(cnt).BaseValue) {
					_toon.GetPrimaryAttribute(cnt).BaseValue++;
					expPointsLeft = expPointsLeft - _toon.GetPrimaryAttribute(cnt).BaseValue;
					_toon.StatUpdate();
				}
			}
		}
		
	}
	
	
//2nd column, skills like brawl, drive, larceny, stealth, etc..
	private void DisplaySkills() {
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++) {
//display skill name
			GUI.Label(new Rect(StatLabelWidth*2, 100+LINE_HEIGHT*cnt, StatLabelWidth, LINE_HEIGHT), ((SkillName)cnt).ToString() );
//display minus button
			if(GUI.Button(new Rect(StatLabelWidth*3, 100+LINE_HEIGHT*cnt, BUTTON_WIDTH, LINE_HEIGHT), "-")){
//transfer exp back to expPoints if player click on it
				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
					_toon.GetPrimaryAttribute(cnt).BaseValue--;
					expPointsLeft++;
					_toon.StatUpdate();
				}
			}
//display plus button
			GUI.Label(new Rect(StatLabelWidth*3 + Offset*2 + BUTTON_WIDTH, 100+LINE_HEIGHT*cnt, BASEVALUE_LABEL_WIDTH, LINE_HEIGHT), _toon.GetSkill(cnt).AdjustedBaseValue.ToString()) ;
//transfer expPoints to skills
			if(GUI.Button(new Rect(StatLabelWidth*3 + Offset*2 + BUTTON_WIDTH + BASEVALUE_LABEL_WIDTH, 100+LINE_HEIGHT*cnt, BUTTON_WIDTH, LINE_HEIGHT), "+")){
				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
					_toon.GetPrimaryAttribute(cnt).BaseValue++;
					expPointsLeft--;
					_toon.StatUpdate();
				}
			}
		}
	}
	
	private void DisplayCreateButton() {
		
		if(_toon.Name != ""){

//that button save the data and load parcLaf at the beginning of the game
			if (GUI.Button(new Rect(	StatLabelWidth*3 + Offset *5,
										Screen.height - 100,
										StatLabelWidth,
										LINE_HEIGHT), "Create")) {
				GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();
			
			//change the cur value of the vitals to the max of the modified value
				UpdateCurVitalVal();
				menu = false;
				gsScript.SaveCharacterData();
				
			}
		}
	}
	
	private void UpdateCurVitalVal(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) {
			_toon.GetVital(cnt).CurValue = _toon.GetVital(cnt).AdjustedBaseValue;
		}
	}
}