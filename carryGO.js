var carry : boolean;
private var pos : GameObject;
private var player : GameObject;

function Start()
{
	pos = GameObject.Find("spawnPoint");
	player = GameObject.FindGameObjectWithTag("Player");
}
//Main function
function Update ()
{
	if (carry == true)
	{
		this.transform.position = pos.transform.position;
		
	}
	if(Input.GetButtonDown("Fire2"))
		{
			this.rigidbody.AddForce(pos.transform.forward * 200);
			// this.rigidbody.useGravity = true;
			// this.rigidbody.velocity = pos.Transform.TransformDirection(Vector3(0,-100,0));
			carry = false;
		}
}

//Activate the Main function when player is near the door
function OnInteract()
{	
	carry = !carry;
}