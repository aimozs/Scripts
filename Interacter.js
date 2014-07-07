var interactLayers : LayerMask = -1;
var interactRange : float = 50.0f;
var hit : RaycastHit;
var boxWidth : int = 200;

var maxHealthBarLength = (Screen.width / 3);

function Update()
{
    if (Physics.Raycast (transform.position, transform.forward, hit, interactRange, interactLayers))
    {
        if (Input.GetKeyDown(KeyCode.E))
            hit.collider.SendMessage("OnInteract", gameObject, SendMessageOptions.DontRequireReceiver);
    }
}

function OnGUI()
{
	if (hit.collider.tag == "Interact")
	{
		GUI.Box(new Rect(Screen.width/2 - boxWidth/2, Screen.height/2-100, boxWidth, 25), hit.collider.name);
	}
	
	if (hit.collider.tag == "NPC")
	{
		GUI.Box(new Rect(Screen.width/2 - boxWidth/2, Screen.height/2-100, boxWidth, 25), "Talk to" + hit.collider.name);
	}
	
	if (hit.collider.tag == "Enemy")
	{
		GUI.Box(new Rect(maxHealthBarLength*2-60, 10, 40, 20), hit.collider.name);
	}
}