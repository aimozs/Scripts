
var climbSpeed : float = 2.0f;
var canClimb : boolean = false;

function Start () {

}

function OnTriggerEnter (other : Collider) 
    {
    	canClimb = true;
	}
function OnTriggerExit (other : Collider) 
    {
    	canClimb = false;
	}


function Update () {
	if (canClimb == true){
		if(Input.GetKey(KeyCode.W)){
			transform.Translate(0, climbSpeed * Time.deltaTime, 0, Space.World);
		}
	}

}