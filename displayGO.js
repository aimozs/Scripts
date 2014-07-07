

// Smothly open a door
var smooth = 2.0;
var displayPos : Vector3;
var originalPos : Vector3;
var displayed : boolean;

//Main function
function Update ()
{
//display position would be the crosshair
originalPos = this.Vector3;
displayPos = GameObject.Find("spawnPoint").Vector3;


	if(displayed == true)
	{
		// Dampen towards the target rotation
		transform.position = Vector3.Slerp(displayPos, originalPos, Time.deltaTime * smooth);
	}

	if(displayed == false)
	{
		// Dampen towards the target rotation
		transform.position = Vector3.Slerp(originalPos, displayPos, Time.deltaTime * smooth);
	}


}

//Activate the Main function when player is near the door
function OnInteract()
{
	displayed = !displayed;
}
