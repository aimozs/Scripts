using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ClimateManager : MonoBehaviour {

	public bool debugClimate = true;
	public float climateInterval = 80f;
	public int days_forecast = 3;
	public Climate.ClimateType previousWeather = Climate.ClimateType.sunny;
	public Dictionary<string, Climate> climatesDict = new Dictionary<string, Climate>();
	public Light sunLight;
	public Flare sunFlare;

	public List<Climate> forecast = new List<Climate>();

	public GameObject direction;
	public float dir = 0f;
	public int strength = 5;
	public int temperature = 5;
	public GameObject windZone;
	public Light thunder;

	public AudioSource audioSource;
	public AudioClip rainAC;
	public AudioClip thunderAC;
	public AudioClip wind;
	public AudioClip cicadas;
	public AudioClip birds;

	private bool _initialized = false;
	private float timer = 0f;


	public delegate void EmitClimate(Climate climate);
	public static event EmitClimate OnTriggerClimate;
  
	private static ClimateManager instance;
	public static ClimateManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<ClimateManager>();
			}
			return instance;
		}
	}

	#region 0.Basics
	void Awake(){
		InitClimate();
	}

//	void Start(){}

	void OnEnable(){
		SceneManager.sceneLoaded += StartClimate;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= StartClimate;
	}

//	void Update(){}
	#endregion

	public static Climate.ClimateType CurrenClimate{
		get { return Instance.forecast[0].climateType; }
	}

	public bool IsSunny(){
		bool _isSunny = false;
		if(forecast.Count > 0)
			_isSunny = forecast[0].climateType == Climate.ClimateType.sunny;
		return _isSunny;
	}

	void ActivateParticles(bool on){
		CustomPlayerFX.StartSnow(on);
		CustomPlayerFX.StartRain(on);
		CustomPlayerFX.StartClouds(on);
		CustomPlayerFX.StartFireflies(on);
	}

	public void InitClimate () {
		
		Climate[] climates = GameObject.FindObjectsOfType<Climate>();

		foreach(Climate climate in climates){
			climatesDict.Add(climate.climateType.ToString(), climate);
		}

		for(int f = 0; f < days_forecast; f++){
			AddClimateToForecast();
		}
		_initialized = true;
	}

	void AddClimateToForecast(){
		Climate climate = GetRandomClimate();
		if(climate != null)
			forecast.Add(climate);
		if(_initialized)
			UpdateTemperature(forecast[0]);
	}

	public void RenewCLimate(){
		previousWeather = forecast[0].climateType;
		forecast.RemoveAt(0);
		AddClimateToForecast();
	}

	void StartClimate(Scene scene, LoadSceneMode mode){
		CancelInvoke();

		if(scene.name == "Montreal"){
			InvokeRepeating("UpdateClimate", 5f, climateInterval);
		} else {
			UpdateTemperature(climatesDict["sunny"]);
		}
	}

	void UpdateClimate(){
		if(OnTriggerClimate != null)
			OnTriggerClimate(forecast[0]);

		RenewCLimate();	
	}

	public Climate GetRandomClimate(){
		int rc = UnityEngine.Random.Range(0, climatesDict.Count);

		int i = 0;

		foreach(KeyValuePair<string, Climate> climate in climatesDict){
			if(i == rc){
				return climate.Value;
			} else{
				i++;
			}
		}
		return climatesDict["sunny"];
	}

	public void StartTimerForecast(){
		StartCoroutine(UpdateTimer());
	}

	IEnumerator UpdateTimer(){
		RenewCLimate();
		timer = Random.Range(240f, 360f);

		yield return new WaitForSeconds(timer);
		StartCoroutine(UpdateTimer());
	}

	public void UpdateTemperature(Climate climate){
		dir = UnityEngine.Random.Range(0f, 359f);
		windZone.transform.rotation = Quaternion.Euler(0f, -dir, 0f);
		ActivateParticles(false);
		Debug.Log(climate.climateType.ToString());
		switch(climate.climateType){
		case Climate.ClimateType.storm:
			strength = UnityEngine.Random.Range(5, 7);
			CustomPlayerFX.StartRain(true);
			PlayStorm();
			PlayRain();
			StartCoroutine(FlashThunder());
			break;

		case Climate.ClimateType.rainy:
			strength = UnityEngine.Random.Range(4, 6);
			CustomPlayerFX.StartRain(true);
			PlayRain();
			break;

		case Climate.ClimateType.cloudy:
			strength = UnityEngine.Random.Range(3, 5);
			CustomPlayerFX.StartClouds(true);
			PlayBirds();
			break;

		case Climate.ClimateType.snowy:
			strength = UnityEngine.Random.Range(3, 6);
			CustomPlayerFX.StartSnow(true);
			break;

		default:
			ActivateParticles(false);
			strength = UnityEngine.Random.Range(1, 2);
			CustomPlayerFX.StartFireflies(true);
			if(temperature > 22)
				PlayCicadas();
			else if(temperature > 10)
				PlayBirds();
			break;
		}
		StartCoroutine(SetWindStrength(strength * .1f));
		StartCoroutine(TransitionToVolume(strength /50f));

	}

	public IEnumerator TransitionToVolume(float volume){
		float elapsedTime = 0;

		while (elapsedTime < 3f) {
			if(audioSource != null)
				audioSource.volume = Mathf.Lerp(audioSource.volume, volume, elapsedTime / 3f);
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	public void PlaySound(AudioClip sound) {
		if(audioSource != null)
			audioSource.PlayOneShot(sound);
	}

	public void PlayRain(){
		if(rainAC != null)
			PlaySound(rainAC);
	}

	public void PlayStorm(){
		if(thunderAC != null)
			PlaySound(thunderAC);
	}

	public void PlayCicadas(){
		if(cicadas != null)
			PlaySound(cicadas);
	}

	public void PlayBirds(){
		if(birds != null)
			PlaySound(birds);
	}

	IEnumerator SetWindStrength(float newStrength){

		float elapsedTime = 0;

		while (elapsedTime < 3f) {
			windZone.GetComponentInChildren<WindZone>().windMain = Mathf.Lerp(windZone.GetComponentInChildren<WindZone>().windMain, newStrength, elapsedTime / 3f);
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator SetSun(bool on){
		float elapsedTime = 0;

		if(on){
			while (elapsedTime < 3f)
			{
				sunLight.intensity = Mathf.Lerp(sunLight.intensity, 1f, elapsedTime / 3f);
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		} else {
			while (elapsedTime < 3f)
			{
				sunLight.intensity = Mathf.Lerp(sunLight.intensity, 0f, elapsedTime / 3f);
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}
		
	}

	public IEnumerator FlashThunder(){

		thunder.intensity = 8f;
		yield return new WaitForSeconds(.1f);
		thunder.intensity = 0f;
		yield return new WaitForSeconds(.1f);
		thunder.intensity = 8f;
		yield return new WaitForSeconds(.1f);
		thunder.intensity = 0f;
		yield return new WaitForSeconds(.1f);

	}
}
