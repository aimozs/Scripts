
var step: AudioClip;

function Update() {
	if (Input.GetKeyDown (KeyCode.W)){
	audio.loop = true;
	audio.volume = 0.03;
	audio.clip = step;
	audio.Play();
	}
	
	if (Input.GetKeyUp (KeyCode.W)){
	audio.Stop();
	}
}
