var interactLayers : LayerMask = -1;
var interactRange : float = 5.0f;
var hit : RaycastHit;
var boxWidth : int = 200;
var interactHeight : float = (Screen.height/2);
var interactOrigin : float = (Screen.width/2 + 50);

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
		GUI.Box(new Rect(interactOrigin, interactHeight, boxWidth, 25), hit.collider.name + "(E)");
	}
	
	if (hit.collider.tag == "Level")
	{
		GUI.Box(new Rect(interactOrigin, interactHeight, boxWidth, 25), "Go (E)");
	}
	
	if (hit.collider.tag == "NPC")
	{
		GUI.Box(new Rect(interactOrigin, interactHeight, boxWidth, 25), "Talk to " + hit.collider.name + " (E)");
	}
	
	if (hit.collider.tag == "Enemy")
	{
		GUI.Box(new Rect(interactOrigin, interactHeight, boxWidth, 25), hit.collider.name);
	}
}