using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartGame : MonoBehaviour {


	public GameObject gameManagerPrefab;
	public GameObject gameSettingsPrefab;
	public GameObject robot;
	public GameObject cat;
	[Range(0, 23)]
	public int startTime;

	[Header("Scene settings")]
	public GameSettings.SceneType sceneType;
	public int capacity = 2;
	public List<NPC> residents = new List<NPC>();
	public List<NPC> guests = new List<NPC>();


	private Vector3 _startPosition;

	private static StartGame instance;
	public static StartGame Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<StartGame>();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Awake(){
		if(GameObject.FindObjectOfType<GameManager>() == null){
			if(gameManagerPrefab != null)
				Instantiate(gameManagerPrefab);
			else{
				Debug.LogWarning("Missing gameManagerPrefab");
			}
		} else {
			robot.SetActive(false);
			cat.SetActive(false);
		}

		if(GameObject.FindObjectOfType<GameSettings>() == null){
			if(gameSettingsPrefab != null)
				Instantiate(gameSettingsPrefab);
			else
				Debug.LogWarning("Missing gameSettingsPrefab");
		}
	}

	void Start (){
		_startPosition = gameObject.transform.position;

		if(GameSettings.Instance._spawn != "" && GameSettings.Instance._spawn != null){
			GameObject spawn = GameObject.Find(GameSettings.Instance._spawn);

			if(spawn != null){
				_startPosition = spawn.transform.position;
			}
		}


		if(GameSettings.Instance.player != null){
			GameSettings.Instance.player.transform.position = _startPosition;
		}
	}

}
