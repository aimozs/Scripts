using UnityEngine;
using System.Collections;

public class MusicBtn : MonoBehaviour {

	private AudioClip _music;

	public AudioClip music{
		get {return _music;}
		set {
			_music = value;
		}
	}

	public void PlayMusic(){
		SoundManager.PlayMusic(_music);
	}
}
