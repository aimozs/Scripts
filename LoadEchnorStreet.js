
//in unity, assign the index level to load for each
var level : int;
var player : GameObject;


function Awake () {
	//discover at each scene load the FPC and assign it to player
	player = GameObject.Find("First Person Controller");
	// Make this game object and all its transform children survive when loading a new scene.
	DontDestroyOnLoad (player);
		
}
	
function OnLevelWasLoaded (level: int) {

	if (level == 1) {
		player.transform.position = GameObject.Find("SFDE").transform.position;
		Debug.Log("you have been spawn in front of Echnor's tower");
		
	}
}


function Update (){
}
	
function OnInteract() {
	Application.LoadLevel(level);
	//need to find how to know which object we interact with so we can look for the same object in the new scene to spawn at the right place
}
	
		
	

