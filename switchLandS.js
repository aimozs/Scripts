var targetL : Light;
var targetS : AudioSource;

function OnInteract() {
    targetL.enabled = !targetL.enabled;
    targetS.enabled = !targetS.enabled;
}