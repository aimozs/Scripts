using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel : MonoBehaviour {



	public AudioClip DACError;
	public AudioClip DACPressBtn;
	public AudioClip DACSuccess;
	public AudioClip drinkClip;
	public AudioClip deathMusicClip;

	public GameObject containerBtn;
	public GameObject inventoryBtn;

	public GameObject shadowPrefab;

	public List<Material> windowMaterials = new List<Material>();
	public List<Material> carMaterials = new List<Material>();
	public List<Material> posterMaterials = new List<Material>();

	public LoadingScreen[] loadingScreens;
	public TvShow[] tvShows;


	private static GameModel instance;
	public static GameModel Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<GameModel>();
			}
			return instance;
		}
	}

	void Awake(){
		loadingScreens = GameObject.FindObjectsOfType<LoadingScreen>();
		tvShows = GameObject.FindObjectsOfType<TvShow>();
	}

	public TvShow GetRandomTvShow(){
		return tvShows[Random.Range(0, tvShows.Length)];
	}

	public LoadingScreen GetRandomLoadingScreen(){
//		Debug.Log("Getting random loading screen from " + loadingScreens.Length);
		return loadingScreens[Random.Range(0, loadingScreens.Length-1)];
	}

	public Material GetRandomMaterial(string type){
		Material mat = null;

		switch(type){
		case "window":
			if(windowMaterials.Count > 1){
				int random = Random.Range(0, windowMaterials.Count);
				mat = windowMaterials[random];
			}
			break;
		case "car":
			if(carMaterials.Count > 1){
				int random = Random.Range(0, carMaterials.Count);
				mat = carMaterials[random];
			}
			break;
		case "poster":
			if(posterMaterials.Count > 1){
				int random = Random.Range(0, posterMaterials.Count);
				mat = posterMaterials[random];
			}
			break;

		}

//		Debug.Log("delivering random material " + mat.name);
		return mat;
	}

	public Color GetRandomColor(){
		Color color = new Color(Random.Range(0, 255),Random.Range(0, 255),Random.Range(0, 255));
//		Debug.Log("returning random color " + color.ToString());
		return color;
	}
		
	public static Color GetConvictionColor(Globals.Conviction conviction){
		Color color = Color.white;
		switch(conviction){
		case Globals.Conviction.anger:
			color = GetMoodColor(Globals.Mood.lustful);
			break;
		case Globals.Conviction.envy:
			color = GetMoodColor(Globals.Mood.obsessed);
			break;
		case Globals.Conviction.gluttony:
			color = GetMoodColor(Globals.Mood.bitter);
			break;
		case Globals.Conviction.greed:
			color = GetMoodColor(Globals.Mood.idealistic);
			break;
		case Globals.Conviction.lust:
			color = Color.blue;
			break;
		case Globals.Conviction.pride:
			color = GetMoodColor(Globals.Mood.conservative);
			break;
		case Globals.Conviction.sloth:
			color = GetMoodColor(Globals.Mood.lovestruck);
//			Debug.Log(color);
			break;
		}

//		Debug.Log("getting color " + color + " for conviction " + conviction.ToString());
		return color;
	}

	public static Color GetMoodColor(Globals.Mood _mood){
		switch(_mood){
		case Globals.Mood.afraid:
			return new Color(1f, .35f, 0f, .5f);
		case Globals.Mood.agressive:
			return new Color(.4f, 0f, .3f, .5f);
		case Globals.Mood.angry:
			return new Color(1f, .2f, .2f, .5f);
		case Globals.Mood.bitter:
			return new Color(.4f, .2f, 0f, .5f);
		case Globals.Mood.calm:
			return new Color(.4f, 1f, 1f, .5f);
		case Globals.Mood.compassionate:
			return new Color(1f, .2f, .8f, .5f);
		case Globals.Mood.conservative:
			return new Color(.6f, 0f, 1f, .5f);
		case Globals.Mood.depressed:
			return new Color(.2f, .2f, .2f, .5f);
		case Globals.Mood.diablerist:
			return new Color(5f, 5f, 5f, .5f);
		case Globals.Mood.distrustful:
			return new Color(.6f, 1f, .6f, .5f);
		case Globals.Mood.envious:
			return new Color(0f, .4f, 0f, .5f);
		case Globals.Mood.excited:
			return new Color(.6f, .2f, .8f, .5f);
		case Globals.Mood.generous:
			return new Color(1f, 0f, .4f, .5f);
		case Globals.Mood.happy:
			return new Color(1f, .2f, .2f, .5f);
		case Globals.Mood.idealistic:
			return new Color(1f, 1f, .2f, .5f);
		case Globals.Mood.innocent:
			return new Color(1f, 1f, 1f, .5f);
		case Globals.Mood.lovestruck:
			return new Color(.6f, 1f, 1f, .5f);
		case Globals.Mood.lustful:
			return new Color(.6f, 0 ,0, .5f);
		case Globals.Mood.obsessed:
			return new Color(.2f, 1f, .2f, .5f);
		case Globals.Mood.sad:
			return new Color(.8f, .8f, .8f, .5f);
		case Globals.Mood.spiritual:
			return new Color(1f, .8f, 0, .5f);
		case Globals.Mood.suspicious:
			return new Color(0, 0, .25f, .5f);
		default:
			return Color.black;

		}
	}
}
