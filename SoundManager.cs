using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	private GameObject player;
	private AudioSource audioSource;
	// Use this for initialization

	private static SoundManager instance;
	public static SoundManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType<SoundManager>();
			}
			return instance;
		}
	}

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		audioSource = player.GetComponentInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void PlaySound(AudioClip sound)
	{
		audioSource.PlayOneShot(sound);
	}
}
