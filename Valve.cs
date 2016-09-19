using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class Valve : MonoBehaviour, IInteractable {

	public GameObject water;
	public float delta = 5f;
	public bool up;
	public AudioClip sound;

	private float maxLevel;
	private float minLevel;
	private float waterLevelY;
	private AudioSource _audioSource;


	void Start(){
		_audioSource = GetComponent<AudioSource>();

		maxLevel = water.transform.position.y+delta;
		minLevel = water.transform.position.y-delta;
	}

	public void OnInteract(){
		if(sound != null)
			_audioSource.PlayOneShot(sound);

		Vector3 waterLevel = water.transform.position;
		waterLevelY = waterLevel.y;

		if(up)
			waterLevelY++;
		else
			waterLevelY--;

		waterLevelY = Mathf.Clamp(waterLevelY, minLevel, maxLevel);

		waterLevel = new Vector3(waterLevel.x, waterLevelY, waterLevel.z);

		water.transform.position = waterLevel;
	}

	public GameObject GetGameObject(){
		return gameObject;
	}
}
