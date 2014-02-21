import System;



public var spawn : String;
public var pc : GameObject;
private var spawnGo : GameObject;



function Awake ()
	{
		pc = GameObject.Find("First Person Controller");
		// Make this game object and all its transform children survive when loading a new scene.
		DontDestroyOnLoad (pc);
	}
	
function Start()
	{
	
	}
	
function Update ()
	{
		Debug.Log(spawnGo);

	}

function OnLevelWasLoaded ()
	{
		spawn = GameObject.Find("__GameSettings").GetComponent("GameSettings").spawnTr;
		spawnGo = GameObject.Find(spawn);
		pc.transform.position = spawnGo.transform.position;
	}
