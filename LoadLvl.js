//Instruction:
//Make an empty game object and call it "Door"
//Rename your 3D door model to "Body"
//Parent a "Body" object to "Door"
//Make sure thet a "Door" object is in left down corner of "Body" object. The place where a Door Hinge need be
//Add a box collider to "Door" object and make it much bigger then the "Body" model, mark it trigger
//Assign this script to a "Door" game object that have box collider with trigger enabled
//Press "f" to open the door and "g" to close the door
//Make sure the main character is tagged "player"


// Smothly open a door
/*var smooth = 2.0;
var DoorOpenAngle = 0.0;
var DoorCloseAngle = 90.0;
var open : boolean;
var spawn : Transform;*/
var level : int;
var player : GameObject;
var spawnPlayer : Transform;
//var loadedlevel : int;

// Make this game object and all its transform children
	// survive when loading a new scene.
	function Awake () {
		DontDestroyOnLoad (player);
		player = GameObject.Find("First Person Controller");
		
	}
	
	function OnLevelWasLoaded (level : int) {
		if (level == 2) {
			print ("644 loaded");
			player.transform.position = GameObject.Find("doorFront").transform.position;
		}
	}

//Main function
	function Update (){
	}
	

	
		
	/*DoorAngle = transform.Rotation;
	if(open == true){
		var target = Quaternion.Euler (270, DoorCloseAngle, 0);
	// Dampen towards the target rotation
	transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smooth);
	}

	if(open == false){
	var target1 = Quaternion.Euler (270, DoorOpenAngle, 0);
	// Dampen towards the target rotation
	transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * smooth);
}


}*/

//Activate the Main function when player is near the door
function OnInteract() {
	//open = !open;
	Application.LoadLevel(level);

}
