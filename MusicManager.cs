using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {


	void Start(){
		TvShow[] tvShows = GameObject.FindObjectsOfType<TvShow>();
		foreach(TvShow show in tvShows){
			UIManager.CreateMusicBtn(show);
		}
	}

	public void PlayMusic(string title){
		Debug.Log("Playing music " + title);
		AudioClip music = Resources.Load("music/" + title) as AudioClip;
		Debug.Log(music.name);
		SoundManager.PlayMusic(music);
	}
}
