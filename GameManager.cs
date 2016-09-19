using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {

	public bool restoreData = false;
	public CanvasGroup menu;
	public Button loadGameBtn;
	public CanvasGroup loading;
	public Image loadingImage;
	public Text loadingDescription;
	public Slider loadingTracker;

	private static bool _isLoading = false;
	private bool _reloading = false;

	private static GameManager instance;
	public static GameManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<GameManager>();
			}
			return instance;
		}
	}

	void Start(){

		DontDestroyOnLoad(gameObject);
		DisplayLoadingScreen(false);

		if(CurrentSceneName == "intro"){
			UIManager.DisplayCursor(true);
			DisplayStartMenu(true);
		} else {
			DisplayStartMenu(false);
		}

		#if UNITY_WEBGL
			loadGameBtn.interactable = false;
		#endif

	}

	void OnEnable(){
		SceneManager.sceneLoaded += AutoSave;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= AutoSave;
	}

	public static bool IsLoading{
		get { return _isLoading; }
	}

	void AutoSave(Scene scene, LoadSceneMode loadMode){
		if(!_reloading){
			if(scene.name != "intro")
				StartCoroutine(SaveAfterNewLevel());
		} else {
			_reloading = false;
		}
	}

	public void NewGame(){
		DisplayStartMenu(false);
		LoadScene ("Montreal", "busStation");
	}

	public void LoadFromMenu(){
		LoadFromFile();
		DisplayStartMenu(false);
	}

	public static string CurrentSceneName{
		get { return SceneManager.GetActiveScene().name; }
	}

	public static Scene CurrentScene{
		get {return SceneManager.GetActiveScene();}
	}

//	public static string GetSceneName(int level){
//		return SceneManager.GetSceneAt(level).name;
//	}

	public void Reload(){

		_reloading = true;
		LoadFromFile();
	}

	public void LoadFromFile() {
//		Debug.Log("LoadFromFile, _isLoading: " + _isLoading);
		if(!_isLoading){
			_isLoading = true;

			if(File.Exists(Application.persistentDataPath + "/playerInfo.dat")){
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
				Globals.Instance.playerData = (PlayerData)bf.Deserialize(file);
				file.Close();
			}

			LoadScene(Globals.Instance.playerData.levelName);

			StartCoroutine(ApplyPlayerData());
		}
	}

	public void LoadScene(string level, string spawn = "Start") {

		if(level != "") {
//			if(NPCManager.Instance != null)
//				NPCManager.Instance.KillNPCs();
			if(GameSettings.Instance != null)
				GameSettings.Instance._spawn = spawn;
			StartCoroutine(LoadAndTrack(level));
		}
	}

	IEnumerator LoadAndTrack(string levelName) {
		DisplayLoadingScreen(true);

		LoadingScreen newLoadingScreen = GameModel.Instance.GetRandomLoadingScreen();
		loadingImage.sprite = newLoadingScreen.screen;
		loadingDescription.text = newLoadingScreen.description;

		AsyncOperation async = SceneManager.LoadSceneAsync(levelName);
		while(!async.isDone){
			loadingTracker.value = async.progress;
			yield return async;
		}
		DisplayLoadingScreen(false);
	}



	IEnumerator SaveAfterNewLevel(){
		yield return new WaitForSeconds(1f);
		Save();
	}

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = Globals.Instance.playerData;

		BaseCharacter baseChar = GameSettings.Instance.player.GetComponent<BaseCharacter>();
		//Adding non-stats data
		data.levelName = SceneManager.GetActiveScene().name;
		data.positionX = baseChar.transform.position.x;
		data.positionY = baseChar.transform.position.y;
		data.positionZ = baseChar.transform.position.z;
		data.time = DayNightCycle.Instance.CurrentTime;
		data.charKind = baseChar.charKind.ToString();
//		data.charOrigin = GameSettings.Instance.player.GetComponent<BaseCharacter>().charOrigin.ToString();
		if(baseChar.spectrum1 != null)
			data.spectrum1 = baseChar.spectrum1.ToString();

		bf.Serialize(file, data);
		file.Close();
	}



	IEnumerator ApplyPlayerData(){
		yield return new WaitForSeconds(.5f);
		BaseCharacter baseChar = GameSettings.Instance.player.GetComponent<BaseCharacter>();

		//Applying data from save to playerGO
		baseChar.transform.position = new Vector3(Globals.Instance.playerData.positionX, Globals.Instance.playerData.positionY, Globals.Instance.playerData.positionZ);

		switch(Globals.Instance.playerData.charKind){
		case "vampire":
			baseChar.kind = BaseCharacter.Kind.vampire;
			UIManager.EnableBloodUI(true);
			break;
		case "ghoul":
			baseChar.kind = BaseCharacter.Kind.ghoul;
			break;
		default:
			baseChar.kind = BaseCharacter.Kind.human;
			UIManager.EnableBloodUI(false);
			break;
		}

		Discipline spectrum;
//		Debug.Log(Globals.Instance.playerData.spectrum1Level);
		if(Globals.Instance.playerData.spectrum1 != ""){
			spectrum = PowerManager.Instance.GetSpectrum(Globals.Instance.playerData.spectrum1);
			if(spectrum != null){
				baseChar.AddSpectrum(spectrum, Globals.Instance.playerData.spectrum1Level);}
		}

		if(Globals.Instance.playerData.spectrum2 != ""){
			spectrum = PowerManager.Instance.GetSpectrum(Globals.Instance.playerData.spectrum2);
			if(spectrum != null){
				baseChar.AddSpectrum(spectrum, Globals.Instance.playerData.spectrum2Level);}
		}

		if(Globals.Instance.playerData.spectrum3 != ""){
			spectrum = PowerManager.Instance.GetSpectrum(Globals.Instance.playerData.spectrum3);
			if(spectrum != null){
				baseChar.AddSpectrum(spectrum, Globals.Instance.playerData.spectrum3Level);}
		}

		switch(Globals.Instance.playerData.charOrigin){
		case "Egyptian":
			baseChar.charOrigin = OriginManager.OriginEnum.Egyptian;
			break;
		case "Greek":
			baseChar.charOrigin = OriginManager.OriginEnum.Greek;
			break;
		case "Hebrew":
			baseChar.charOrigin = OriginManager.OriginEnum.Hebrew;
			break;
		case "Indian":
			baseChar.charOrigin = OriginManager.OriginEnum.Indian;
			break;
		default:
			baseChar.charOrigin = OriginManager.OriginEnum.Romanian;
			break;
		}

//		UIManager.Instance.UpdateUI();
		_isLoading = false;

	}

	void DisplayStartMenu(bool on){
		if(menu != null){
			menu.alpha = on ? 1f : 0f;
			menu.interactable = menu.blocksRaycasts = on;
		}
	}

	void DisplayLoadingScreen(bool on){
		if(loading != null){
			loading.alpha = on ? 1f : 0f;
			loading.interactable = loading.blocksRaycasts = on;
		}
	}
}
