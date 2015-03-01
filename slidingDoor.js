// Smothly open a door
var openedPosition : Vector3;
var smooth : float = 2.0;
var wideness : float = 2.0;
//var doorSlideSound = AudioClip;
var closedPosition : Vector3;
var open : boolean = false;


function Awake()
{
	closedPosition = transform.position;
	openedPosition = closedPosition;
	openedPosition.z = closedPosition.z + wideness;
}
//Main function
function Update ()
{
	//store position of the object in var closedPosition
	
	//if open become true, move the object to closedPosition + 20
	
	if(open == true)
	{
		//openedPosition = closedPosition.transform.position;
		//openedPosition.transform.position.y = openedPosition.transform.position.y + 20;
		//transform.position = Vector3.MoveTowards(transform.position, openedPosition.position, Time.deltaTime * smooth);
		//transform.Translate(Vector3.forward * smooth * Time.deltaTime);
		transform.position = Vector3.Lerp(transform.position, openedPosition, smooth * Time.deltaTime);
	}

	if(open == false)
	{
		transform.position = Vector3.Lerp(transform.position, closedPosition, Time.deltaTime * smooth);
	}


}

//Activate the Main function when player is near the door
function OnInteract()
{
	open = !open;
}
