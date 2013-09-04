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
	
	
//GUI style
	
	public GUISkin	VentrueSkin;
	
	
// Use this for initialization
	void Start () {
		_toon = new PlayerCharacter();
		_toon.Awake();
		
		expPointsLeft = STARTING_POINTS;
		
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
		GUI.skin = VentrueSkin;
		
		DisplayName();
		DisplayVitals();
		DisplayExpPointsAvail();
		DisplayAttribute();
		DisplaySkills();
		}
// first line (40)	
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

//1st column
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
//3rd line (250+30 for each attribute)
	private void DisplayAttribute() {
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
			GUI.Label(new Rect(	Offset,
								250+LINE_HEIGHT*cnt,
								StatLabelWidth,
								LINE_HEIGHT),
								((AttributeName)cnt).ToString() );
			
			if(GUI.Button(new Rect(	Offset*2 + StatLabelWidth,
									250+LINE_HEIGHT*cnt,
									BUTTON_WIDTH,
									LINE_HEIGHT),
									"-")){
				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
					_toon.GetPrimaryAttribute(cnt).BaseValue--;
					expPointsLeft++;
					_toon.StatUpdate();
				}
			}
			GUI.Label(new Rect(	Offset*4 + StatLabelWidth + BUTTON_WIDTH,
								250+LINE_HEIGHT*cnt,
								BASEVALUE_LABEL_WIDTH,
								LINE_HEIGHT),
								_toon.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());

			if(GUI.Button(new Rect(	Offset*6 + StatLabelWidth + BUTTON_WIDTH + BASEVALUE_LABEL_WIDTH,
									250+LINE_HEIGHT*cnt,
									BUTTON_WIDTH,
									LINE_HEIGHT),
									"+")) {
				if(expPointsLeft > 0) {
					_toon.GetPrimaryAttribute(cnt).BaseValue++;
					expPointsLeft--;
					_toon.StatUpdate();
				}
			}
		}
		
	}
	
	
//3rd line but 2nd column
	private void DisplaySkills() {
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++) {
			GUI.Label(new Rect(StatLabelWidth*2, 100+LINE_HEIGHT*cnt, StatLabelWidth, LINE_HEIGHT), ((SkillName)cnt).ToString() );
			if(GUI.Button(new Rect(StatLabelWidth*3, 100+LINE_HEIGHT*cnt, BUTTON_WIDTH, LINE_HEIGHT), "-")){
				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
					_toon.GetPrimaryAttribute(cnt).BaseValue--;
					expPointsLeft++;
					_toon.StatUpdate();
				}
			}
			GUI.Label(new Rect(StatLabelWidth*3 + Offset*2 + BUTTON_WIDTH, 100+LINE_HEIGHT*cnt, BASEVALUE_LABEL_WIDTH, LINE_HEIGHT), _toon.GetSkill(cnt).AdjustedBaseValue.ToString()) ;
			if(GUI.Button(new Rect(StatLabelWidth*3 + Offset*2 + BUTTON_WIDTH + BASEVALUE_LABEL_WIDTH, 100+LINE_HEIGHT*cnt, BUTTON_WIDTH, LINE_HEIGHT), "+")){
				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
					_toon.GetPrimaryAttribute(cnt).BaseValue++;
					expPointsLeft--;
					_toon.StatUpdate();
				}
			}
		}
	}
	

}