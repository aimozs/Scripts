

// Smothly open a door
// var smooth = 2.0;
// var displayPos : Vector3;
// var originalPos : Vector3;
private var displayed : boolean;
private var showGO: boolean;
var go : GameObject;
// var dist : float;
// var lastInteraction : Transform;
private var pos : GameObject;
private var item: GameObject;
private var player : GameObject;
private var fpsinput : FPSInputController;

function Start()
{
	 displayed = false;
	 showGO = false;
	 pos = GameObject.Find("spawnPoint");
	 player = GameObject.FindGameObjectWithTag("Player");
	 fpsinput = player.GetComponent("FPSInputController");


}
//Main function
function Update ()
{
//display position would be the crosshair
// originalPos = this.Vector3;

// go.transform.position = GameObject.Find("spawnPoint").transform.position;
// go.transform = displayPos;


	if(displayed == true)
	{
		// Dampen towards the target rotation
		// transform.position = Vector3.Slerp(displayPos, originalPos, Time.deltaTime * smooth);
		if (showGO == false)
		{
			item = Instantiate(go, pos.transform.position, pos.transform.rotation);
			showGO = true;
			fpsinput.enabled = false;
			// fpsinput.enable = !fpsinput.enable;
			// lastInteraction.position = pos.transform.position;
		}
		item.transform.position = pos.transform.position;
		

		/*dist = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, lastInteraction.position);
		if(dist > 5)
		{
			displayed = false;
		}*/
		
	}

	if(displayed == false)
	{
		showGO = false;
		Destroy(item);
		fpsinput.enabled = true;
		// fpsinput.enable = !fpsinput.enable;
		// Dampen towards the target rotation
		// transform.position = Vector3.Slerp(originalPos, displayPos, Time.deltaTime * smooth);
		// Destroy (item);
	}
}

//Activate the Main function when player is near the door
function OnInteract()
{	
	displayed = !displayed;
    // go.enabled = !go.enabled;
}