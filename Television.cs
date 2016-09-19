using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof (BoxCollider))]
[RequireComponent(typeof (AudioSource))]
[RequireComponent(typeof (Interactable))]
public class Television : MonoBehaviour, IInteractable {

	public bool state;
	public MeshRenderer screen;

	public Texture screenIdle;

	private AudioSource _audioSource;
	private Light _light;

	void Start(){
		_audioSource = gameObject.GetComponentInChildren<AudioSource>();
		_light = gameObject.GetComponentInChildren<Light>();
		SetRandomVideo(state);
	}

	/// <summary>
	/// IInteractable
	/// </summary>
	public void OnInteract(){
		SetRandomVideo(!state);
		state = !state;

	}

	public GameObject GetGameObject(){
		return gameObject;
	}

	void SetRandomVideo(bool on){
		if(on){
			TvShow tvShow = GameModel.Instance.GetRandomTvShow();
			screen.material.mainTexture = tvShow.screenVisual;
			screen.material.SetTexture("_EMISSION", tvShow.screenVisual);

			_light.enabled = true;
			_light.color = tvShow.lightColor;

			_audioSource.clip = tvShow.audioClip;
			_audioSource.time = Random.Range(0, _audioSource.clip.length);
			_audioSource.Play();

		} else {
			screen.material.mainTexture = screenIdle;
			_audioSource.Stop();

			_light.enabled = false;
		}

	}
}
