var climbSpeed : float = 2.0f;
private var canClimb : boolean = false;

function Update () 
{
	if (canClimb == true)
	{
		if(Input.GetKey(KeyCode.W))
		{
			transform.Translate(0, climbSpeed * Time.deltaTime, 0, Space.World);
		}
	}
}

function OnTriggerEnter (other : Collider) 
    {
    	if (other.tag == "ladder")
    	{
    		canClimb = true;
    		Debug.Log("climb on");
    	}
    	
	}
function OnTriggerExit (other : Collider) 
    {
    	if (other.tag == "ladder")
    	{
    		canClimb = false;
	    	Debug.Log("climb off");
	    }
	}