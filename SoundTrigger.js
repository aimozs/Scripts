
var playOnlyOnce : boolean = true;
var sound : AudioClip;

private var audioSource : AudioSource;
private var played : boolean = false;

@script RequireComponent(AudioSource)

function Start(){
	audioSource = GetComponent(AudioSource);
}

function OnTriggerEnter() {
	if(sound != null){
		if(playOnlyOnce){
			if(!played){
				played = true;
	    		audioSource.PlayOneShot(sound);
    		}
    	} else {
    		audioSource.PlayOneShot(sound);
    	}
	}
}