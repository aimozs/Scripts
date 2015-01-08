import System;

public class Discipline extends MonoBehaviour
{
	class Level
	{
		var level : int;
		var image : Texture;
	}
	class Discipline
	{
		var name : String;
		var vitaeCost : int;
		var level : Level;
	}
	
	//display
	private var x : int = 10;
	private var y : int = 10;
	private var barThickness : int = 25;
	private var barHeightPos : float;
	private var disBox : int = 80;
	//sizes
	private var vitaeBarLength : float;
	private var maxVitaeBarLength : float;

	public var hiddenObject : GameObject;
	public var playerTransform : Transform;
	
	//disciplines
	private var needClanAssigned : boolean = true;
	private var clan : String;
	private var disciplines = new Array("discipline");
	var disciplineIndex : int = 1;
	private var t : int = 0;
	private var countDown : float = 10f;
	private var vitaeCost : float = 10f;
	private var vitaeUsed : float = 10f;
//	private var adj : float = 10f;
	
	//celerity
	private var celerityLvl : float;
	private var celerity_disabled : boolean = false;
	
	//feralWhispers(animalism)
	private var feralWhispers_disabled : boolean = false;
	
	//heigtenedSenses(auspex)
	private var heightenedSenses_disabled : boolean = false;
	private var heightenedSenses : GameObject;
	
	private var slowTime : float;
	private var slowTimeSound : AudioClip;
	
	//vigor
	
	private var vigorLvl : float = 1;
	
	//resilience
	
	private var resilienceLvl : float = 1;
	

	//icons => need to stay public
	var celerityIcon : Texture;
	var resilienceIcon : Texture;
	var heightenedIcon : Texture;
	var touchIcon : Texture;
	var feralIcon : Texture;
	var vigorIcon : Texture;
	var aweIcon : Texture;
	var commandIcon : Texture;
	var monstrousIcon : Texture;
	var aspectIcon : Texture;
	
	
	//character
	private var curVitae : int = 100;
	private var maxVitae : int = 100;
	
	var current_discipline : String;
	
	//style
	public var Vitae : GUIStyle;
		
	function Awake()
	{
		celerityLvl = 1;
		slowTime = .7f;
		clan = GameObject.Find("__GameSettings").GetComponent("GameSettings").clan;
		barHeightPos = (barThickness + y);

		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	function Update()
	{
		vitaeBarLength = (Screen.width/3 * curVitae/maxVitae);
		maxVitaeBarLength = (Screen.width/3);
		
		//using tab to switch between the different discipline
		if (Input.GetButtonDown("Switch"))
		{
			disciplineIndex++;
			if (disciplineIndex >= disciplines.length)
			{
				disciplineIndex = 1;
			}
			current_discipline = disciplines[disciplineIndex];
		}

		if (heightenedSenses_disabled)
		{
			hiddenObject = GameObject.FindGameObjectWithTag("Hidden");
			hiddenDist = Vector3.Distance(hiddenObject.transform.position, playerTransform.position);
			if(hiddenDist < 10)
			{
				hiddenObject.GetComponent(ParticleSystem).Play(); // the GetComponent
				hiddenObject.GetComponent(ParticleSystem).enableEmission = true;
				hiddenObject.GetComponent(ParticleSystem).loop = true;
			}
		}
		// display helpers
		
		//if right click is pressed, activate the current_discipline
		if (Input.GetButtonDown("Fire2"))
		{
		//calculate the vitae amount that will be withdrawn
		vitaeUsed = vitaeCost * slowTime * celerityLvl;
			//verify you have enough vitae to call the discipline
			if (curVitae > vitaeUsed)
			{
				if(current_discipline == "Celerity" && !celerity_disabled)
				{
				//substract the vitaeUsed from curVitae
				AdjustCurVitae(-vitaeUsed);
				CelerityOn();
				Invoke ("CelerityOff", countDown * celerityLvl);
				}
			
				//call feralWhispers
				if(current_discipline == "Feral Whispers" && !feralWhispers_disabled)
				{
					AdjustCurVitae(-vitaeUsed);
					FeralWhispersOn();
					Invoke ("FeralWhispersOff", countDown);
				}
				
				if(current_discipline == "Heightened Senses" && !heightenedSenses_disabled)
				{
					AdjustCurVitae(-vitaeUsed);
					HeightenedSensesOn();
					Invoke ("HeightenedSensesOff", countDown);
				}
			}
		}
	}
//display icons for disciplines
	function OnGUI()
	{
		if(clan != "Human")
		{
			GUI.Box(new Rect(x, barHeightPos*2-y, vitaeBarLength, barThickness), "", Vitae);
			GUI.Box(new Rect(x, barHeightPos*2-y, maxVitaeBarLength, barThickness), (curVitae).ToString() +" / "+ (maxVitae).ToString());
		    switch (current_discipline)
		    {
		    case "Celerity":
		        GUI.DrawTexture(Rect(maxVitaeBarLength + x*2,y,disBox,disBox), celerityIcon, ScaleMode.ScaleToFit, true, 0.0f);
		        break;
		    case "Vigor":
		        GUI.DrawTexture(Rect(maxVitaeBarLength + x*2,y,disBox,disBox), vigorIcon, ScaleMode.ScaleToFit, true, 0.0f);
		        break;
		    case "Resilience":
		        GUI.DrawTexture(Rect(maxVitaeBarLength + x*2,y,disBox,disBox), resilienceIcon, ScaleMode.ScaleToFit, true, 0.0f);
		        break;
		    case "Awe":
		        GUI.DrawTexture(Rect(maxVitaeBarLength + x*2,y,disBox,disBox), aweIcon, ScaleMode.ScaleToFit, true, 0.0f);
		        break;
		    case "Command":
		        GUI.DrawTexture(Rect(maxVitaeBarLength + x*2,y,disBox,disBox), commandIcon, ScaleMode.ScaleToFit, true, 0.0f);
		        break;
		    case "Monstrous Countenance":
		        GUI.DrawTexture(Rect(maxVitaeBarLength + x*2,y,disBox,disBox), monstrousIcon, ScaleMode.ScaleToFit, true, 0.0f);
		        break;
		    case "Touch of Shadow":
		        GUI.DrawTexture(Rect(maxVitaeBarLength + x*2,y,disBox,disBox), touchIcon, ScaleMode.ScaleToFit, true, 0.0f);
		        break;
		    case "Aspect of the Predator":
		        GUI.DrawTexture(Rect(maxVitaeBarLength + x*2,y,disBox,disBox), aspectIcon, ScaleMode.ScaleToFit, true, 0.0f);
		        break;
		    case "Feral Whispers":
		        GUI.DrawTexture(Rect(maxVitaeBarLength + x*2,y,disBox,disBox), feralIcon, ScaleMode.ScaleToFit, true, 0.0f);
		        break;
		    case "Heightened Senses":
		        GUI.DrawTexture(Rect(maxVitaeBarLength + x*2,y,disBox,disBox), heightenedIcon, ScaleMode.ScaleToFit, true, 0.0f);
		        break;
		    default:
		        GUI.Box(new Rect(maxVitaeBarLength + x*2, y, disBox, disBox), current_discipline);
		        break;
		    }
		}
	}
	//Discipline Celerity slows time enhance your reaction speed
	function CelerityOn()
	{
		celerity_disabled = true;
		Time.timeScale = slowTime;
	}
	function CelerityOff()
	{
		Time.timeScale = 1;
		celerity_disabled = false;
	}
	
	//discipline FeralWhispers let you talk to animal
	function FeralWhispersOn()
	{
		feralWhispers_disabled = true;
	}
	
	function FeralWhispersOff()
	{
		feralWhispers_disabled = false;
	}
	
	
	function HeightenedSensesOn()
	{
		heightenedSenses_disabled = true;
		// brighter vision
		heightenedSenses = GameObject.Find("heightenedSenses");
		heightenedSenses.light.intensity = .1f;
		
	}
	function HeightenedSensesOff()
	{
		// turn off better vision
		heightenedSenses.light.intensity = 0f;
		// turn off visual helpers
		// hiddenObject.GetComponent(ParticleSystem).enableEmission = false;
		heightenedSenses_disabled = false;
	}
	
	//called when activating a discpline to reduce your vitae gauge
	function AdjustCurVitae(adj)
	{
		curVitae += adj;
		
		if(curVitae < 0)
			curVitae = 0;
		
		if(curVitae > maxVitae)
			curVitae = maxVitae;
		
		if(maxVitae < 1)
			maxVitae = 1;
		
		vitaeBarLength = maxVitaeBarLength * (curVitae / maxVitae);
	}
	//supposed to be called only once at the beginning of the game after selecting your clan, it gives the player its 3 first discipline
	function OnLevelWasLoaded ()
	{
		if (needClanAssigned == true)
		{
			clan = GameObject.Find("__GameSettings").GetComponent("GameSettings").clan;
			if (clan == "Daeva" )
			{
				disciplines.Add("Celerity");
				disciplines.Add("Awe");
				disciplines.Add("Vigor");
			}
			if (clan == "Gangrel" )
			{
				disciplines.Add("Feral Whispers");
				disciplines.Add("Aspect of the Predator");
				disciplines.Add("Resilience");
			}
			if (clan == "Mekhet" )
			{
				disciplines.Add("Heightened Senses");
				disciplines.Add("Celerity");
				disciplines.Add("Touch of Shadow");
			}
			if (clan == "Nosferatu" )
			{
				disciplines.Add("Monstrous Countenance");
				disciplines.Add("Touch of Shadow");
				disciplines.Add("Vigor");
			}
			if (clan == "Ventrue" )
			{
				disciplines.Add("Feral Whispers");
				disciplines.Add("Command");
				disciplines.Add("Resilience");
			}
			current_discipline = disciplines[disciplineIndex];
			needClanAssigned = false;
			// Invoke ("CelerityOff", countDown * celerityLvl);clan = GameObject.Find("__GameSettings").GetComponent("GameSettings").clan;
			var meetSmilingJack = GameObject.Find("__GameSettings").GetComponent("MeetSmilingJack");
			meetSmilingJack.SpawnJack();
		}
		
	}
}