var interactLayers : LayerMask = -1;
var interactRange : float = 2.0f;
var hit : RaycastHit;

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
	if (hit.collider.name != ("" || "First Person Controller"))
	{
		GUI.Box(new Rect(Screen.width/2-50, Screen.height/2+50, 200, 20), hit.collider.name);
	}
}