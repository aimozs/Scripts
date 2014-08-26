using UnityEngine;
using System.Collections;

public class Vampire : PlayerCharacter
{
	private string _sir;
	private string _mentor;
	private string _clan;
	private string _covenant;
//	private Discipline[] _discipline;
	private int _vitae;
	private string intro;


	public void Awake()
	{
		_sir = "Someone";
		_mentor = "Echnor";
		_clan = "Daeva";
		_covenant = "Carthians";

//		switch (_clan) 
//		{
//			case "Daeva":
//				_discipline[1] = "Celerity";
//				_discipline[2] = "Majesty";
//				_discipline[3] = "Vigor";
//				break;
//			case "Gangrel":
//				_discipline[1] = "Animalism";
//				_discipline[2] = "Resilience";
//				_discipline[3] = "Protean";
//				break;
//			case "Ventrue":
//				_discipline[1] = "Animalism";
//				_discipline[2] = "Resilience";
//				_discipline[3] = "Dominate";
//				break;
//			case "Nosferatu":
//				_discipline[1] = "Nightmate";
//				_discipline[2] = "Vigor";
//				_discipline[3] = "Obfuscate";
//				break;
//			case "Daeva":
//				_discipline[1] = "Obfuscate";
//				_discipline[2] = "Celerity";
//				_discipline[3] = "Auspex";
//				break;
//			default:
//				;
//		}

		_vitae = 10;
		intro = "Personnal infos:\n" +
				"Your Sire: " + _sir + "\n"+
				"Your Clan: " + _clan + "\n"+
				"Current Mentor: " + _mentor + "\n"+
				"Your covenant: " + _covenant + "\n"
				;
	}

	void OnGUI()
	{
		if(Input.GetKey(KeyCode.I))
		{
			GUI.Label(new Rect(10, 300, 300, 500), intro);
		}
	}
}
