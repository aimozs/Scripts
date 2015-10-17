using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class BrokenGlass : MonoBehaviour {

	public AudioClip sound;
	// Use this for initialization
	void Start ()
	{
		gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
		Destroy(this.gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
