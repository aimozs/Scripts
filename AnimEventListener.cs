using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]
public class AnimEventListener : MonoBehaviour {

	public AudioClip sound;

	private AudioSource animAudioSource;

	void Start(){
		animAudioSource = GetComponent<AudioSource>();
	}

	public void Bubble(){
		GetComponent<ParticleSystem>().Play ();
	}

	public void PlaySound(){
		if(animAudioSource == null){
			animAudioSource = GetComponent<AudioSource>();
			if(SoundManager.Instance.debugAudio)
				Debug.LogWarning("[AEL] Getting AudioSource is failing on " + gameObject.name);
			PlaySound();

		} else {
			if(sound != null)
				animAudioSource.PlayOneShot(sound);
			else{
				if(SoundManager.Instance.debugAudio)
					Debug.LogWarning("[AEL] Animator is trying to play a sound, but it's null on " + gameObject.name);
			}
		}
	}

	public void PlaySpecificSound(AudioClip newSound){
		if(animAudioSource != null)
			animAudioSource.PlayOneShot(newSound);
	}

	public void LoadTransition(string transition){
		animAudioSource.Stop();
		switch (transition){
		case "embrace":
			GameSettings.Instance.player.GetComponent<BaseCharacter>().charKind = BaseCharacter.Kind.vampire;
			GameManager.Instance.LoadScene("echnorRoom", "echnorBed");
			break;
		default:
			break;
		}

	}
}
