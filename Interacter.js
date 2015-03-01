var interactLayers : LayerMask = -1;
var interactRange : float = 4.0f;
var hit : RaycastHit;
var boxWidth : int = 200;
var interactHeight : float = (Screen.height/2);
var interactOrigin : float = (Screen.width/2 + 50);
var detail : String = "that scene";

var interactPosX : int;
var interactPosY : int;
var interactSizeX : int;
var interactSizeY : int;

var clan : String;
var feralWhispers_disabled : boolean;
var actionAllowed : boolean;
var colliderTag : String;
var colliderName : String;

function Start()
{
	interactPosX = Screen.width*40/100;
	interactPosY = Screen.height/2 - 200;
	interactSizeX = Screen.width*20/100;
	interactSizeY = 25;
}

function Update() : GameObject
{
    if (Physics.Raycast (transform.position, transform.forward, hit, interactRange, interactLayers))
    {
    	colliderTag = hit.collider.tag;
    	colliderName = hit.collider.name;
    	
    	if (colliderTag == "Level")
		{
			detail = hit.collider.GetComponent("LoadLvl").level;
		}

		
       
        
        if (colliderTag == "Animal")
        {
        	feralWhispers_disabled = GameObject.Find("First Person Controller").GetComponent("Discipline").feralWhispers_disabled;
	        if (Input.GetKeyDown(KeyCode.E) && feralWhispers_disabled)
	        {
	        	hit.collider.SendMessage("OnInteract", gameObject, SendMessageOptions.DontRequireReceiver);
	        }
	    }
	    else
	    {
	    	if (Input.GetKeyDown(KeyCode.E))
        	{
        	hit.collider.SendMessage("OnInteract", gameObject, SendMessageOptions.DontRequireReceiver);
        	}
	    }
    }
}

function OnLevelWasLoaded ()
{
	clan = GameObject.Find("__GameSettings").GetComponent("GameSettings").clan;
	
}

function OnGUI()
{
	//display different hints based on the GameObject's tag
	switch (colliderTag)
    {
    case "Interact":
        GUI.Box(new Rect(interactPosX, interactPosY, interactSizeX, interactSizeY), colliderName + "(E)");
        break;
    case "carry":
        GUI.Box(new Rect(interactPosX, interactPosY, interactSizeX, interactSizeY), "Grab" + "(E)");
        break;
    case "Level":
        GUI.Box(new Rect(interactPosX, interactPosY, interactSizeX, interactSizeY), "Go to " + detail + " (E)");
        break;
    case "Clan":
        GUI.Box(new Rect(interactPosX, interactPosY, interactSizeX, interactSizeY), "Choose that clan (E)");
        break;
    case "NPC":
        GUI.Box(new Rect(interactPosX, interactPosY, interactSizeX, interactSizeY), "Talk to " + colliderName + " (E)");
        break;
    case "Enemy":
        GUI.Box(new Rect(interactPosX, interactPosY, interactSizeX, interactSizeY), colliderName);
        break;
    }
    //when pointing at animals, will work only if player has invoked FeralWhispers discipline
    if (colliderTag == "Animal" && feralWhispers_disabled)
	{
		GUI.Box(new Rect(interactPosX, interactPosY, interactSizeX, interactSizeY), "Talk to " + colliderName + " (E)");
	}
	

	// if (colliderTag == "Interact")
	// {
	// 	GUI.Box(new Rect(interactPosX, interactPosY, interactSizeX, interactSizeY), colliderName + "(E)");
	// }
	// if (colliderTag == "carry")
	// {
	// 	GUI.Box(new Rect(interactPosX, interactPosY, interactSizeX, interactSizeY), "Grab" + "(E)");
	// }
	// //display when pointing at a GO that leads to another scene
	// if (colliderTag == "Level")
	// {
	// 	GUI.Box(new Rect(interactPosX, interactPosY, interactSizeX, interactSizeY), "Go to " + detail + " (E)");
	// }
	// //in the menu scene, when the player select his clan
	// if (colliderTag == "Clan")
	// {	
	// 	GUI.Box(new Rect(interactPosX, interactPosY, interactSizeX, interactSizeY), "Choose that clan (E)");
	// }
	// //when pointing at NPCs
	// if (colliderTag == "NPC")
	// {
	// 	GUI.Box(new Rect(interactPosX, interactPosY, interactSizeX, interactSizeY), "Talk to " + colliderName + " (E)");
	// }
	// if (colliderTag == "Enemy")
	// {
	// 	GUI.Box(new Rect(interactPosX, interactPosY, interactSizeX, interactSizeY), colliderName);
	// }
}