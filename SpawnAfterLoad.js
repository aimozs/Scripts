import System;

//declare variables
public var spawn : String;
public var pc : GameObject;

var GS;
var spawnGo : GameObject;



function Awake ()
{
	pc = GameObject.Find("First Person Controller");
	// Make this game object and all its transform children survive when loading a new scene.
	DontDestroyOnLoad (pc);
}

function OnLevelWasLoaded ()
{
	GS = GameObject.Find("__GameSettings").GetComponent("GameSettings");
	spawn = GS.spawnTr;
	spawnGo = GameObject.Find(spawn);
	pc.transform.position = spawnGo.transform.position;
	
	if (GS.clan == "Human" )
	{
		GS.clan = spawn;
	}
}