using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

	public bool debugUI;

	public GameObject reticle;

	[Header("Interactions")]
	public Image actionUI;
	public Sprite basicSprite;
	public Sprite talk;
	public Sprite use;
	public Sprite open;
	public Sprite loadLevel;

	const float TIME_ACTION_DISPLAY = .5f;
	private static Color ACTION_FADE_OFF = new Color(0f, 0f, 0f, 0f);

	[Header("VitaeSlider")]
	public Image vitaeSliderImage;
	public Slider vitaeSlider;
	public Color MinHealthColor;
	public Color MaxHealthColor;

	[Header("Spectrums")]
	public Image disciplineUI;
	public GameObject timedSpectrumStack;
	public GameObject timedSpectrumPrefab;
	public GameObject phonePowerPanel;
	public GameObject phoneBeastPanel;
	public GameObject phoneHumanityPanel;
	public GameObject phoneShadowPanel;

	[Header("Weapon")]
	public Image weaponUI;

	[Header("Phone")]
	public GameObject mainAppScreen;
	public GameObject musicScreen;
	public GameObject musicBtnPrefab;
	public Text timeUI;

	[Header("Inventory")]
	public GameObject phoneInventoryPanel;
	public GameObject inventoryItemList;
	public GameObject containerItemList;

	[Header("Stats")]
	public GameObject phoneStatsPanel;
	public GameObject statList;
	public GameObject statPrefab;

	private static bool phoneDisplayed = false;
	private static bool convoDisplayed = false;
	private static float timer;

	private static Dictionary<GameObject, TimedPower> activePowers = new Dictionary<GameObject, TimedPower>();


	private static UIManager instance;
	public static UIManager Instance {
		get {
				if(instance == null) {
					instance = GameObject.FindObjectOfType<UIManager>();
					if(instance == null){
						Instantiate(GameSettings.Instance.mainUI);
						instance = GameObject.FindObjectOfType<UIManager>();
				}
			}
			return instance;
		}
	}

	#region 0.Basics
//	void Awake(){}

	void Start () {
		DontDestroyOnLoad(gameObject);
		UpdateSliderColor();
		EnableBloodUI(false);
	}

//	void OnEnable(){}

//	void OnDisable(){}

//	void Update(){}

	void FixedUpdate(){
		if(timer > 0){
			timer -= Time.deltaTime;
		} else {
			ShowAction(false);
		}
	}

	#endregion

	#region 1.Statics

	public static void AddTimedPower(GameObject power, float time){
		if(activePowers.ContainsKey(power)){
			activePowers[power].ResetTimer(time);
		} else {
			GameObject timer = Instantiate(Instance.timedSpectrumPrefab);
			timer.transform.SetParent(Instance.timedSpectrumStack.transform);
			timer.GetComponent<TimedPower>().StartTimer(power, time);
			activePowers.Add(power, timer.GetComponent<TimedPower>());
		}
	}

	public static void CloseContainer(){
		Debug.Log("closing container");
		DisplayCursor(false);
		InventoryManager.SetCurrentContainer();
		//		CanvasManager.Instance.DisplayContainer(false);
		CloseInventory();
		ShowPhone(false);
	}

	public static void CloseInventory(){

		for (int i = InventoryPanel.transform.childCount - 1; i>=0; --i){
			Destroy(InventoryPanel.transform.GetChild(i).gameObject);
		}

		for (int i = ContainerPanel.transform.childCount - 1; i>=0; --i){
			Destroy(ContainerPanel.transform.GetChild(i).gameObject);
		}

		DisplayHomeScreen();

		CanvasManager.Instance.DisplayContainer(false);
		InventoryManager.SetCurrentContainer();

	}

	public static GameObject ContainerPanel {
		get {return Instance.containerItemList; }
	}

	public static void CreateItemUI(string location, GameObject item){
		switch(location){
		case "inventory":
			GameObject itemInventoryUI = Instantiate(GameModel.Instance.inventoryBtn);
			itemInventoryUI.transform.SetParent(Instance.inventoryItemList.transform, false);
			itemInventoryUI.GetComponentInChildren<Text>().text = item.name;
			itemInventoryUI.GetComponent<ItemUI>().itemGO = item;
			break;
		case "container":
			GameObject itemContainerUI = Instantiate(GameModel.Instance.containerBtn);
			itemContainerUI.transform.SetParent(Instance.containerItemList.transform, false);
			itemContainerUI.GetComponentInChildren<Text>().text = item.name;
			itemContainerUI.GetComponent<ItemUI>().itemGO = item;
			break;
		}
	}

	public static void CreateMusicBtn(TvShow show){
		GameObject newMusicBtnPrefab = Instantiate(Instance.musicBtnPrefab);
		newMusicBtnPrefab.transform.SetParent(Instance.musicScreen.transform);
		newMusicBtnPrefab.GetComponentInChildren<Text>().text = show.audioClip.name;
		newMusicBtnPrefab.GetComponent<MusicBtn>().music = show.audioClip;
	}

	public static void DisplayCursor(bool on){
		Cursor.visible = on;
	}

	public static void DisplayHomeScreen(){
		foreach (Transform child in Instance.mainAppScreen.transform) {
			child.gameObject.SetActive(false);
		}
	}

	public static void EnableBloodUI(bool on){
		CanvasManager.Instance.EnableBloodCanvas(on);

		if(on)
			VitaeSlider = GameSettings.PlayerBC.vitae;
	}

	public static bool InConvo {
		get { return convoDisplayed; }
		set { convoDisplayed = value; }
	}

	public static GameObject InventoryPanel {
		get {return Instance.inventoryItemList; }
	}


	public static void OpenContainer(List<GameObject> contents){
		DisplayCursor(true);
		CanvasManager.Instance.DisplayContainer();
		OpenInventory();

		foreach(GameObject item in contents){
			if(item != null)
				CreateItemUI("container", item);
		}
	}

	public static bool PhoneVisible {
		get { return phoneDisplayed; }
		set { phoneDisplayed = value; }
	}

	/// <summary>
	/// Remove passed power from the list of active powers
	/// </summary>
	/// <param name="powerToRemove">Power to remove.</param>
	public static void RemoveFromActivePowers(GameObject powerToRemove){
		activePowers.Remove(powerToRemove);
	}

	/// <summary>
	/// Change reticle's color to the one provided
	/// </summary>
	/// <param name="color">Color.</param>
	public static void SetReticleColor(Color color){
		Instance.reticle.GetComponentInChildren<Image>().color = color;
	}

	/// <summary>
	/// Shows the action icon based on boolean.
	/// </summary>
	/// <param name="on">If set to <c>true</c> on.</param>
	public static void ShowAction(bool on){
		Instance.actionUI.color = on ? Color.white : ACTION_FADE_OFF;
	}

	static void ShowPhone(bool on = true){
		if(!InConvo){
			phoneDisplayed = on;
			DisplayCursor(phoneDisplayed);
			GameSettings.PlayerBC.EnableControls(!phoneDisplayed);

			if(!phoneDisplayed){
				CloseInventory();
			}

			CanvasManager.Instance.DisplayPhoneCanvas(phoneDisplayed);
		}
	}

	public static void TogglePhone(){
		if(!InConvo){
			phoneDisplayed = !phoneDisplayed;
			ShowPhone(phoneDisplayed);
		}
	}

	public static void ToogleReticle(bool on){
		if(instance.reticle != null)
			instance.reticle.SetActive(on);
	}


	/// <summary>
	/// Pass an actionType to update the main interaction icon
	/// </summary>
	/// <param name="actionType">Action type.</param>
	public static void UpdateAction(GameSettings.actionType actionType = GameSettings.actionType.hide){
		ShowAction(true);
		SetReticleColor(Color.white);
		switch(actionType){
		case GameSettings.actionType.talk:
			Instance.actionUI.sprite = Instance.talk;
			SetReticleColor(Color.red);
			break;
		case GameSettings.actionType.open:
			Instance.actionUI.sprite = Instance.open;
			break;
		case GameSettings.actionType.use:
			Instance.actionUI.sprite = Instance.use;
			break;
		case GameSettings.actionType.loadLevel:
			Instance.actionUI.sprite = Instance.loadLevel;
			break;
		default:
			ShowAction(false);
			break;
		}
		timer = TIME_ACTION_DISPLAY;
	}

	/// <summary>
	/// Updates the UI: powerIcon, and timeUI
	/// </summary>
//	public static void UpdateUI() {
//
//		if(GameSettings.PlayerBC.powers.Count >= 1){
//			Sprite currentDisciplineSprite = GameSettings.PlayerBC.powers[GameSettings.PlayerBC.currentPower].iconPower;
//			if(currentDisciplineSprite != null){
//				disciplineUI.sprite = currentDisciplineSprite;
//			} else {
//				disciplineUI.sprite = basicSprite;
//			}
//		}
//	}

	public static Sprite PowerIcon {
		set { Instance.disciplineUI.sprite = value; }
	}



	public static void UpdateTimeUI(int newTime){
		Instance.timeUI.text = (newTime).ToString("00") + ":00";
	}

	public static void UpdateWeaponUI(Sprite newSprite){
		Instance.weaponUI.sprite = newSprite;
	}

	public static float VitaeSlider {
		set{ Instance.vitaeSlider.value = value; }
	}

	#endregion

	#region 2.Publics //Used in editor, offers function on button to static

	public void DisplayDialACode(bool on){
		CanvasManager.Instance.DisplayDialACode(on);
	}

	/// <summary>
	/// Handle for Opening the inventory
	/// </summary>
	public void DisplayInventory(){
		OpenInventory();
	}

	public void Load(){
		GameManager.Instance.LoadFromFile();
	}

	public void ReturnToIntro(){
		//		GameManager.LoadScene("Intro");
	}

	public void Save(){
		GameManager.Instance.Save();
	}

	public void ShowStats(){

		phoneStatsPanel.SetActive(true);

		string[] attributes = BaseCharacter.Attributes.GetNames(typeof(BaseCharacter.Attributes)) as string[];
		foreach(string attribute in attributes){
			statList.transform.FindChild(attribute).GetComponent<StatPrefab>().statValue.text = Globals.Instance.playerData.GetAttributeValue(attribute).ToString();
		}
	}

	public void DisplayPowerPanel(){

		phonePowerPanel.SetActive(true);

	}

	public void DisplayBeastPanel(){
		HideAllSpectrums();
		phoneBeastPanel.SetActive(true);
	}

	public void DisplayHumanityPanel(){
		HideAllSpectrums();
		phoneHumanityPanel.SetActive(true);
	}

	public void DisplayShadowPanel(){
		HideAllSpectrums();
		phoneShadowPanel.SetActive(true);
	}

	void HideAllSpectrums(){
		phoneBeastPanel.SetActive(false);
		phoneHumanityPanel.SetActive(false);
		phoneShadowPanel.SetActive(false);
	}

	public void UpdateSliderColor(){
		vitaeSliderImage.color = Color.Lerp(MinHealthColor, MaxHealthColor, vitaeSlider.value);
	}

	#endregion

	#region 3.Privates

	static void OpenInventory(){

		ShowPhone();

		List<GameObject> inventoryItems = InventoryManager.PlayerInventory;
		Instance.phoneInventoryPanel.SetActive(true);
		foreach(GameObject item in InventoryManager.PlayerInventory){
			if(item != null)
				CreateItemUI("inventory", item);
		}
	}

	#endregion

	#region Deprecated
	public static void Notify(string message){
//		CanvasManager.Instance.ShowNotification(message);
	}

//	public static void Notify(string message, Globals.Conviction sin, int step = 0){
//		CanvasManager.Instance.ShowNotification(message, sin, step);
//	}
	#endregion
}