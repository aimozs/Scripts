
//in unity, assign the index level to load for each
var level : int;
var player : GameObject;
var spawnPosX : int;
var spawnPosY : int;
var spawnPosZ : int;
var lastScene : int;


function Awake () {
	//discover at each scene load the FPC and assign it to player
	player = GameObject.Find("Player");
	// Make this game object and all its transform children survive when loading a new scene.
	DontDestroyOnLoad (player);
	lastScene = Application.loadedLevel;
	
		
}
	
function OnLevelWasLoaded (level: int) {

	if (level == 1) {
		player.transform.position = GameObject.Find("644doorFront").transform.position;
		Debug.Log("lafontaine loaded");
		Debug.Log(lastScene);
		
	}
	
	if (level == 2) {
		player.transform.position = GameObject.Find("644doorFront").transform.position;
		Debug.Log("644 loaded");
		Debug.Log(lastScene);
		
	}

	if (level == 3) {
		player.transform.position = GameObject.Find("echnorLift").transform.position;
		Debug.Log("echnor loaded");
		Debug.Log(lastScene);
	}

}


function Update (){
}
	
function OnInteract() {
	Application.LoadLevel(level);
	//need to find how to know which object we interact with so we can look for the same object in the new scene to spawn at the right place
}
	
		
	

