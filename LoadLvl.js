import System;


public var level : String;
public var spawn : String;
//the two data I need, what level to load and where to spawn
//public string level;
//public string spawn;

function Awake()
	{

	}

function Update ()
	{
		
	}

function OnInteract ()
	{
		GameObject.Find("__GameSettings").GetComponent("GameSettings").spawnTr = spawn;
		
		Application.LoadLevel(level);
	}
