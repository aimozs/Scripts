import System;

	private var x : int = 10;
	private var y : int = 10;
	private var barThickness : int = 25;
	private var barHeightPos : float;

	private var disBox : int = 75;

	private var clan : String;
	private var disciplines = new Array("discipline");
	private var i : int = 0;
	private var t : int = 0;

	//character
	private var curVitae : int = 100;
	private var maxVitae : int = 100;
	private var celerity_disabled : boolean = false;
	private var countDown : float = 10f;
	private var adj : int = 10;

	//sizes
	private var vitaeBarLength : float;
	private var maxVitaeBarLength : float;
	var current_discipline : String;

	//style
	public var Vitae : GUIStyle;
		
	function Awake()
	{
		clan = GameObject.Find("__GameSettings").GetComponent("GameSettings").clan;
		barHeightPos = (barThickness + y);
	}
	
	function Update()
	{
		vitaeBarLength = (Screen.width/3*curVitae/maxVitae);
		maxVitaeBarLength = (Screen.width/3);
		current_discipline = disciplines[i];
		
		if (Input.GetButtonDown("Switch"))
		{
			i++;
			if (i >= disciplines.length)
			{
				i = 1;
			}
		}
		
		if (Input.GetButtonDown("Fire2"))
		{
			if(current_discipline == "Celerity" && curVitae > adj && !celerity_disabled)
			{
				AdjustCurVitae(-adj);
				CelerityOn();
				celerity_disabled = true;
				Invoke ("CelerityOff", countDown);
				
			}
			
		}
	}

	function OnGUI()
	{
		if(clan != "Human")
		{
			GUI.Box(new Rect(x, barHeightPos*2-y, vitaeBarLength, barThickness), "", Vitae);
			GUI.Box(new Rect(x, barHeightPos*2-y, maxVitaeBarLength, barThickness), (curVitae).ToString() +" / "+ (maxVitae).ToString());
			GUI.Box(new Rect(maxVitaeBarLength + x*2, y, disBox, disBox), current_discipline);
		}
	}
	
	function CelerityOn()
	{
		Time.timeScale = .3;
	}
	
	function CelerityOff()
	{
		Time.timeScale = 1;
		celerity_disabled = false;
	}
	
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
	
	function OnLevelWasLoaded ()
	{
		clan = GameObject.Find("__GameSettings").GetComponent("GameSettings").clan;
		if (clan == "Daeva" )
		{
			disciplines.Add("Celerity");
			disciplines.Add("Majesty");
			disciplines.Add("Vigor");
		}
		if (clan == "Gangrel" )
		{
			disciplines.Add("Animalism");
			disciplines.Add("Protean");
			disciplines.Add("Resilience");
		}
		if (clan == "Mekhet" )
		{
			disciplines.Add("Auspex");
			disciplines.Add("Celerity");
			disciplines.Add("Obfuscate");
		}
		if (clan == "Nosferatu" )
		{
			disciplines.Add("Nightmare");
			disciplines.Add("Obfuscate");
			disciplines.Add("Vigor");
		}
		if (clan == "Ventrue" )
		{
			disciplines.Add("Animalism");
			disciplines.Add("Dominate");
			disciplines.Add("Resilience");
		}
		i++;
	}
