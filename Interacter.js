var interactLayers : LayerMask = -1;
var interactRange : float = 5.0f;
var hit : RaycastHit;
var boxWidth : int = 200;
var interactHeight : float = (Screen.height/2);
var interactOrigin : float = (Screen.width/2 + 50);
var detail : String = "that scene";

var clan : String;
var feralWhispers_disabled : boolean;
var actionAllowed : boolean;
var colliderTag : String;
var colliderName : String;


function Update()
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
	if (colliderTag == "Interact")
	{
		GUI.Box(new Rect(interactOrigin, interactHeight, boxWidth, 25), colliderName + "(E)");
	}
	//display when pointing at a GO that leads to another scene
	if (colliderTag == "Level")
	{
		GUI.Box(new Rect(interactOrigin, interactHeight, boxWidth, 25), "Go to " + detail + " (E)");
	}
	//in the menu scene, when the player select his clan
	if (colliderTag == "Clan")
	{	
		GUI.Box(new Rect(Screen.width/2-75, Screen.height*1/10, 150, 25), "Choose that clan (E)");
	}
	//when pointing at NPCs
	if (colliderTag == "NPC")
	{
		GUI.Box(new Rect(interactOrigin, interactHeight, boxWidth, 25), "Talk to " + colliderName + " (E)");
	}
	//when pointing at animals, will work only if player has invoked FeralWhispers discipline
	if (colliderTag == "Animal" && feralWhispers_disabled)
	{
		GUI.Box(new Rect(interactOrigin, interactHeight, boxWidth, 25), "Talk to " + colliderName + " (E)");
	}
	
	if (colliderTag == "Enemy")
	{
		GUI.Box(new Rect(interactOrigin, interactHeight, boxWidth, 25), colliderName);
	}
}