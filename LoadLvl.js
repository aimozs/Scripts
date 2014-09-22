//import
import System;

//variables declaration
public var level : String;
public var spawn : String;

function OnInteract()
{
	GameObject.Find("__GameSettings").GetComponent("GameSettings").spawnTr = spawn;
	Application.LoadLevel(level);
}
