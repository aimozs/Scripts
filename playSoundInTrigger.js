var sound: AudioClip;

function OnTriggerEnter()
{
    audio.PlayOneShot(sound);

}

function OnTriggerExit()
{
	audio.Stop();

}