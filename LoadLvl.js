//import
import System;

//variables declaration
public var level : String;
public var spawn : String;

function OnTriggerEnter (player : Collider)
{
    if(player.tag=="Player")
    {
    	GameObject.Find("__GameSettings").GetComponent("GameSettings").spawnTr = spawn;
		Application.LoadLevel(level);
	}
    
 }

function OnInteract()
{
	GameObject.Find("__GameSettings").GetComponent("GameSettings").spawnTr = spawn;
	Application.LoadLevel(level);
}