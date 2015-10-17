using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameSettings : MonoBehaviour
{
	//Player stuff
	public string _nextLevel;
	public string _spawn;
	public GameObject player;
	public GameObject managers;

	//interactable
	public GameObject interactable;

	public RaycastHit hit;
	public LayerMask interactLayers = -1;
	public float interactRange = 4.0f;


	private GameObject cursor;
	
	//UI stuff
	public GameObject _dashboard;
	public bool pause = false;
	public BaseCharacter playerStats;
	public InputField textField;
	public Text UIAction;
	public Image UIActionBG;

	public static int interactCount = 0;

	private static GameSettings instance;
	public static GameSettings Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<GameSettings>();
			}
			return instance;
		}
	}

	void Awake() {
		Dialoguer.Initialize();
		//setup UI
		_dashboard = GameObject.Find("Dashboard");
		UIAction = GameObject.Find("UIAction").GetComponent<Text>();
		UIActionBG = GameObject.Find("UIActionBG").GetComponent<Image>();

		if(GameObject.FindObjectOfType<DataManager>() == null)
			Instantiate(managers);
	}

	void Start () {
		SwitchDashboardOn(false);
		//player = GameObject.Find("First Person Controller");
		player = GameObject.FindGameObjectWithTag("Player");
		playerStats = player.GetComponent<BaseCharacter>();
		DontDestroyOnLoad(player);

	}
	
	void Update () {
		if(!pause) {
			if(cursor == null)
				cursor = GameObject.Find("Main Camera");

			//Bring the menu if cancel is pressed
			if(Input.GetButtonDown("Cancel")) {
				pause = !pause;
				if(pause) {
					Pause();
				} else {
					Resume();
				}
			}

			//Get info about the object in front of cursor
			GetInteractable();

			//Get info from the object in front of the player and display details if any
			if(interactable != null) {
				if(interactable.GetComponent<NextLevel>()) {
					_nextLevel = interactable.GetComponent<NextLevel>().GetNextLevelInfo().level;
					_spawn = interactable.GetComponent<NextLevel>().GetNextLevelInfo().spawn;
				}
				//Debug.Log(interactable.name);
				UpdateUI();

				if(Input.GetButtonDown("interact")) {
					//Get information from NextLevel object and keep it through LoadLevel
					interactCount++;
					interactable.SendMessage("OnInteract", gameObject, SendMessageOptions.DontRequireReceiver);
					Debug.Log (interactCount + ": Interacting with " + interactable.name + " go:" + gameObject);

					if(interactable.GetComponent<NextLevel>())
						pause = true;
				}
			}
		}
	}

	void GetInteractable() {
		Debug.DrawRay(cursor.transform.position, cursor.transform.forward, Color.red, 1f);
		if(Physics.Raycast (cursor.transform.position, cursor.transform.forward, out hit, interactRange, interactLayers)) {
			interactable = hit.collider.gameObject;
		}
	}

	void UpdateUI() {
		switch (interactable.tag) {
		case "Interact":
			DisplayAction();
			UIAction.text = (interactable.name + "(E)");
			break;
		case "carry":
			DisplayAction();
			UIAction.text = ("Grab" + "(E)");
			break;
		case "Level":
			DisplayAction();
			UIAction.text = ("Go to " + _nextLevel + " (E)");
			break;
		case "Clan":
			DisplayAction();
			UIAction.text = ("Choose that clan (E)");
			break;
		case "NPC":
			DisplayAction();
			UIAction.text = ("Talk to " + interactable.name + " (E)");
			break;
		case "Enemy":
			DisplayAction();
			UIAction.text = (interactable.name);
			break;
		case "Animal":
			DisplayAction();
			UIAction.text = ("Talk to " + interactable.name + " (E)");
			break;
		default:
			UIActionBG.CrossFadeAlpha(0.0f, 0.1f, true);
			break;
		}
	}

	void DisplayAction() {
		UIActionBG.CrossFadeAlpha(1.0f, 0.1f, true);
	}
		
	//detect collisions with player
	void OnControllerColliderHit(ControllerColliderHit collider) {
//		if(collider.transform.root.gameObject.GetComponentInChildren<Item>())
//		{
//			AddToInventory(collider.transform.root.gameObject);
//		}
	}



	public void Pause() {
		Time.timeScale = 0;
		SwitchDashboardOn(true);
//		InputField textField = GameObject.Find("nameField").GetComponent<InputField>();
//		textField.text = playerStats.GetCharacterName();
	}

	public void Resume() {
		Time.timeScale = 1;
		SwitchDashboardOn(false);
	}

	void SwitchDashboardOn(bool value) {
		_dashboard.GetComponent<CanvasGroup>().alpha = value ? 1f : 0f;
	}


	public NextLevel LoadNextLevel {
		set {
			_nextLevel = value.level;
			_spawn = value.spawn;
		}
	}

	void OnLevelWasLoaded() {
		player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = GameObject.Find(_spawn).transform.position;
		pause = false;
	}

	//LOAD AND SAVE DATA
	public void SaveCharacterData() {
		//Clearing
//		PlayerPrefs.DeleteAll();

		PlayerPrefs.SetString("Level", Application.loadedLevelName.ToString());
		PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
		PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
		PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
		
	//use that line below once if you change the way you save playerPrefs to clean it

//		PlayerPrefs.SetString("PlayerName", playerStats.GetCharacterName());


	//saving the attribute
		/*for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
		{
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " baseVal", pcClass.GetPrimaryAttribute(cnt).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " xpToLvl", pcClass.GetPrimaryAttribute(cnt).ExpToLevel);
		}*/

	//saving the vitals
//		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
//		{
//			PlayerPrefs.SetFloat(((VitalName)cnt).ToString(), playerStats.GetVital(cnt).CurValue);
//		}

	//saving the skills
//		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
//		{
//			PlayerPrefs.SetInt(((SkillName)cnt).ToString(), playerStats.GetSkill(cnt).BaseValue);
//		}
		PlayerPrefs.Save();
		Debug.Log ("Data Saved");
	}

	public void LoadCharacterData() {
		player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPosX"),PlayerPrefs.GetFloat("PlayerPosY"),PlayerPrefs.GetFloat("PlayerPosZ"));
//		playerStats.SetCharacterName(PlayerPrefs.GetString("PlayerName"));

	}
}
