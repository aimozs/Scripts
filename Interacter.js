var interactLayers : LayerMask = -1;
var interactRange : float = 2.0f;

function Update() {
    if (Input.GetKeyDown(KeyCode.E)) {
        var hit : RaycastHit;
        if (Physics.Raycast (transform.position, transform.forward, hit, interactRange, interactLayers)) 
            hit.collider.SendMessage("OnInteract", gameObject, SendMessageOptions.DontRequireReceiver);
    }
}