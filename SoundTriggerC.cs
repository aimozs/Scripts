using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundTriggerC : MonoBehaviour {

	public AudioClip audioClip;
	public AudioSource audioSource;

	public bool playOnlyOnce = true;
	public Globals.Conviction conviction;
	public int maxConvictionStep;

	private bool played = false;

	// Use this for initialization
	void Start () {
		if(audioSource == null)
			audioSource = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")){
			if(playOnlyOnce){
				if(!played){
					played = true;
					PlayClip();
				}
			} else {
				PlayClip();
			}
		}
	}

	void PlayClip(){
		if(audioClip != null && Globals.GetConvictionProgress(conviction) <= maxConvictionStep)
			audioSource.PlayOneShot(audioClip);

	}
}
