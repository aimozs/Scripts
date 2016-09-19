using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class Lift : MonoBehaviour, IInteractable {

	public GameObject objectToMove;
	public GameObject target;
	public float speed = .01f;
	public bool up;
	public AudioClip sound;

	private AudioSource _audioSource;

	private bool move = false;

	void Start(){
		_audioSource = GetComponent<AudioSource>();
	}

	void FixedUpdate(){
		if(move && objectToMove.transform.position != target.transform.position)
			objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, target.transform.transform.position, speed);
		
		if(Vector3.Distance(objectToMove.transform.position, target.transform.position) < .5f)
			move = false;
	}

	public void OnInteract(){
		if(sound != null)
			_audioSource.PlayOneShot(sound);
		move = true;
//		Vector3 waterLevel = objectToMove.transform.position;
//		if(up)
//			objectToMove.transform.position = new Vector3(waterLevel.x, waterLevel.y+1, waterLevel.z);
//		else
//			objectToMove.transform.position = new Vector3(waterLevel.x, waterLevel.y-1, waterLevel.z);
	}

	public GameObject GetGameObject(){
		return gameObject;
	}
}
