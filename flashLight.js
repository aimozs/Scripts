var flashLight : Light;

function Update () {

	if(Input.GetKeyDown(KeyCode.F))
	{
		flashLight.enabled = !flashLight.enabled;
	}
	

}