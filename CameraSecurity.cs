using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (AudioSource))]
public class CameraSecurity : MonoBehaviour {

	public AudioClip alarmClip;
	public int cameraLevel = 5;
	public Light _light;

//	private Animator _animator;
	private AudioSource _audioSource;


	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource>();
		GetComponent<Collider>().isTrigger = true;
//		_animator = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("Player") && cameraLevel >= Globals.Instance.playerData.dexterity + GameSettings.Instance.player.GetComponent<BaseCharacter>().dexterityBonus){
			if(_light == null){
				
				_light = GetComponentInChildren<Light>();
			}

			if(_light != null){
				if(alarmClip != null){
					_audioSource.clip = alarmClip;
					_audioSource.Play();
				}
				_light.color = Color.red;
			}
		}
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.CompareTag("Player")){
			if(_light == null){
				_light = GetComponentInChildren<Light>();
			}

			_audioSource.Stop();

			if(_light != null){
				_light.color = Color.green;
			}
		}
	}
}
