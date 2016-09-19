using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
	public bool debugAudio;

	public AudioSource fxSource;
	public static AudioSource musicSource;

//	private GameObject player;
	private static bool _fxEnabled = true;

	private static SoundManager instance;
	public static SoundManager Instance{
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<SoundManager>();
			}
			return instance;
		}
	}

	void Start () {
//		player = GameObject.FindGameObjectWithTag("Player");
		fxSource = GetComponent<AudioSource>();
		musicSource = transform.FindChild("Music").GetComponent<AudioSource>();
	}

	public static void PlaySound(AudioClip sound) {
		if(sound != null && Instance.fxSource != null && _fxEnabled){
			Instance.fxSource.PlayOneShot(sound);
		}
	}

//	public void PlaySoundOverlay(AudioClip sound) {
//		if(playerSource != null && _fxEnabled){
//			playerSource.clip = sound;
//			playerSource.loop = false;
//			playerSource.Play();
//		}
//	}

	public static void PlayMusic(AudioClip music) {
		if(musicSource == null){
			Debug.Log("Missing music source");
		}

		if(musicSource != null){
			musicSource.clip = music;
			musicSource.Play();
		}
	}

	public void EnableFX(bool on){
		_fxEnabled = on;
	}
}
