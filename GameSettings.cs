using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameSettings : MonoBehaviour {

	public enum actionType {use, talk, open, loadLevel, hide};
	public enum SceneType { publique, privee };

	//Player stuff
	public bool debugSettings;
	public bool debugAI;
	public bool debugInput = true;
	public bool restoreGame = false;
	public GameObject playerPrefab;

	public string _nextLevel;
	public string _spawn;

	public GameObject player;
	public GameObject mainUI;

	//interactable
	public GameObject interactable;

	public RaycastHit hit;
	private LayerMask interactLayers = -1;
	public float interactRange = 3f;


	private GameObject cursor;
	
	//UI stuff

	public bool pause = false;

	public static Interactable[] targets;

	private static GameSettings instance;
	public static GameSettings Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<GameSettings>();
			}
			return instance;
		}
	}

	public delegate void UpdateGS();
	public static event UpdateGS updateGS;


	#region 0.Baasics
	void Awake() {
		InitPlayer();
		Instantiate(mainUI);
	}

	void Start () {

		DontDestroyOnLoad(player);

		#if UNITY_WEBGL && !UNITY_EDITOR
			DeactivateAllLevel();
		#endif

	}

	void OnEnable(){
//		Dialoguer.events.onMessageEvent += DialogueEvent;
		SceneManager.sceneLoaded += InitScene;
	}

	void OnDisable(){
//		Dialoguer.events.onMessageEvent -= DialogueEvent;
		SceneManager.sceneLoaded -= InitScene;
	}

	#endregion

	void InitScene(Scene scene, LoadSceneMode loadMode){
		FindInteractable();

		pause = false;

		PixelCrushers.DialogueSystem.DialogueLua.SetVariable("location", scene.name);

		ManageFixedUpdate();
		UIManager.CloseContainer();
	}

	public void AddPower(string power){
		PlayerBC.AddSpectrum(PowerManager.Instance.GetSpectrum(power), 3);
	}

	void DialogueEvent(string message, string metadata){

		switch(message){

		case "addPower":
			player.GetComponent<BaseCharacter>().AddSpectrum(PowerManager.Instance.GetSpectrum(metadata));
			break;

		case "AddObjectToInventory":
			Debug.Log("Looking for " + metadata);
			GameObject item = GameObject.Find(metadata);
			if(item != null)
				InventoryManager.AddToInventory(item);
			else
				UIManager.Notify("Couldnt find " + metadata + " in scene");
			break;

		case "incrementProg":
			int prog = int.Parse(metadata.Substring(0, 1));
			int metalen = metadata.Length - 1;
			metadata = metadata.Substring(1, metalen);
			Globals.IncrementProgress(metadata, prog);
			break;
		}
	}

	public void IncrementProgress(string metadata){
		int prog = int.Parse(metadata.Substring(0, 1));
		int metalen = metadata.Length - 1;
		metadata = metadata.Substring(1, metalen);
		Globals.IncrementProgress(metadata, prog);
	}


//	Globals.Conviction GetSin(string metadata){
//		return (Globals.Conviction) System.Enum.Parse(typeof(Globals.Conviction), metadata);
//	}

	public static bool TestInventory(string condition){
		return Instance.PlayerInventory.Find(obj=>obj.name==condition);
	}

	public static bool TestInventory(GameObject condition){
		return Instance.PlayerInventory.Contains(condition);
	}

	public static BaseCharacter PlayerBC{
		get { return Instance.player.GetComponent<BaseCharacter>() ;}
	}

	public static string currentSceneName {
		get { return SceneManager.GetActiveScene().name; }
	}

	void ManageFixedUpdate(){
		if(currentSceneName == "Montreal"){
//			Debug.Log("Starting FIXEDUPDATEGS");
			InvokeRepeating("FixedUpdateGS", 2f, 5f);
		} else {
			CancelInvoke("FixedUpdateGS");
		}
	}

	void FixedUpdateGS(){
		if(updateGS != null){
//			Debug.Log("Updating");
			updateGS();
		}
	}

	void FindTargets(){
		FindInteractable();
//		targets = null;
//		targets = GameObject.FindObjectsOfType<NextLevel>();
	}

	void FindInteractable(){
		targets = null;
		targets = GameObject.FindObjectsOfType<Interactable>();
		Debug.Log("Found interactables: " + targets.Length);
	}
	
	void FixedUpdate () {

		if(!pause) {
			GetInteractable();
		}
	}

	public static void InitPlayer() {
		Instance.player = GameObject.FindGameObjectWithTag("Player");

		if(Instance.player != null){
//			if(Instance.debugSettings)
//				Debug.Log("[GS] Found player " + Instance.player.name);

		} else {
			if(Instance.playerPrefab != null){
				Instance.player = Instantiate(Instance.playerPrefab);
//				if(Instance.debugSettings)
//					Debug.Log("[GS] Player instantiated " + Instance.player.name);
			}
//			else {
//				Debug.LogWarning("Player Object not found, and no playerPrefab to instatiate");
//			}
		}
	}

	public List<GameObject> PlayerInventory{
		get { return player.GetComponent<BaseCharacter>().inventory; }
	}

	public static GameObject GetRandomTarget(){
//		if(NPCManager.Instance.debugNPC)
//			Debug.Log ("Getting target from " + targets.Length);
		
		GameObject target = targets[UnityEngine.Random.Range(0, targets.Length-1)].GetGameObject();
		
		return target;
	}

	public GameObject GetInteractable() {
		if(cursor == null) {
			cursor = GameObject.Find("cursor");
		}

		if(cursor != null){
//			Debug.DrawRay(cursor.transform.position, -cursor.transform.up, Color.red, interactRange);

			interactable = null;

			if(Physics.Raycast (cursor.transform.position, -cursor.transform.up, out hit, interactRange, interactLayers)) {
//				interactable = hit.collider.gameObject;
				if(hit.collider != null){
					if(hit.collider.GetComponent<BaseCharacter>()){
						interactable = hit.collider.gameObject;
						UIManager.UpdateAction(actionType.talk);

					}

					if(hit.collider.GetComponent<Container>()){
						interactable = hit.collider.gameObject;
						UIManager.UpdateAction(actionType.open);
					}

					if(hit.collider.GetComponent<NextLevel>()) {
						interactable = hit.collider.gameObject;
						UIManager.UpdateAction(actionType.loadLevel);
					}

					if(hit.collider.GetComponent<IInteractable>() != null){// || hit.collider.GetComponent<Valve>() ||hit.collider.GetComponent<Switch>()|| hit.collider.GetComponent<Door>()){
						interactable = hit.collider.gameObject;
						UIManager.UpdateAction(actionType.use);
					}
				}
			}

//			if(interactable == null)
//				UIManager.Instance.HideAction();
		}
			
//		if(interactable != null) {
//
//			if(interactable.GetComponent<BaseCharacter>()){
//				UIManager.Instance.UpdateAction(actionType.talk);
//			}
//
//			if(interactable.GetComponent<Container>()){
//				UIManager.Instance.UpdateAction(actionType.open);
//			}
//
//			if(interactable.GetComponent<NextLevel>()) {
//				UIManager.Instance.UpdateAction(actionType.loadLevel);
//			}
//
//			if(interactable.GetComponent<Item>() || interactable.GetComponent<Valve>() || interactable.GetComponent<Door>()){
//				UIManager.Instance.UpdateAction(actionType.use);
//			}
//
//		} else {
//			UIManager.Instance.HideAction();
//		}

		return interactable;
	}

	public static void Interact(){
		GameObject interactable = Instance.GetInteractable();
		if(interactable != null){
			interactable.SendMessage("OnInteract", SendMessageOptions.DontRequireReceiver);
//			if(interactable.GetComponent<BaseCharacter>() != null){
//				Instance.StartDialogue(true);
			if(!UIManager.PhoneVisible)
				interactable.SendMessage("OnUse", Instance.player.transform, SendMessageOptions.DontRequireReceiver);
//			}
		}
	}

	public void StartDialogue(bool on){
		UIManager.InConvo = on;
		GameSettings.PlayerBC.EnableControls(!on);
		UIManager.DisplayCursor(on);
	}

	public void Pause() {
		UIManager.DisplayCursor(true);
		Time.timeScale = 0;
	}

	public void Resume() {
		Time.timeScale = 1;
		UIManager.DisplayCursor(false);
	}

	public static NextLevel StoreNextLevelData {
		set {
			instance._nextLevel = value.level;
			instance._spawn = value.spawn;
		}
	}

//	public void AddToInventoryFromContainer(GameObject item) {
//		PlayerInventory().Add(item);
////		item.transform.parent = GameSettings.Instance.player.transform;
////		item.transform.localPosition = Vector3.zero;
////		item.transform.localScale = Vector3.zero;
//		InventoryManager.AddToInventory(item);
//		Debug.Log (item.name + " has been added to the inventory");
//		GameSettings.Instance.RemoveFromCurrentContainer(item);
//	}



//	public void DepositItemInContainer(GameObject item){
//		currentContainer.Deposit(item);
//	}

	void DeactivateAllLevel(){
		NextLevel[] levels = GameObject.FindObjectsOfType<NextLevel>();
		foreach(NextLevel level in levels){
			level.Lock();
		}
	}
}
