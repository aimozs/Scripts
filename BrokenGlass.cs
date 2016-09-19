using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class BrokenGlass : MonoBehaviour {

	public AudioClip sound;

	void Start () {
		gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
		Destroy(this.gameObject, 5f);
	}
}
