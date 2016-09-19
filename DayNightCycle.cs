using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class DayNightCycle : MonoBehaviour {

	public enum DayPeriod {day, dawn, night, morning}

	[Header("Time")]
	public DayPeriod currentPeriod = DayPeriod.day;
	private int _currentTime = 0;

	public Material morning;
	public Material sunny;
	public Material cloudy;
	public Material rainy;
	public Material stormy;
	public Material dawn;
	public Material night;

	public Light sunLight;

	private Animator timeAnim;

	public delegate void SetTimeD(float time);
	public static event SetTimeD SetTimeE;

	private static DayNightCycle instance;
	public static DayNightCycle Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<DayNightCycle>();
			}
			return instance;
		}
	}

	#region 0.Basics
	void Awake (){
		_currentTime = StartGame.Instance.startTime;
		timeAnim = GetComponent<Animator>();
	}

	void Start () {
		
//		SetAnimBasedOnCurrentTime(_currentTime);

		InvokeRepeating("IncrementTime", 1f, 60f);

		if(SetTimeE != null)
			SetTimeE(_currentTime);
	}

	void OnEnable(){
		ClimateManager.OnTriggerClimate += UpdateClimateSkybox;
		SceneManager.sceneLoaded += UpdateDayNight;
	}

	void OnDisable(){
		ClimateManager.OnTriggerClimate -= UpdateClimateSkybox;
		SceneManager.sceneLoaded -= UpdateDayNight;
	}

	#endregion

	#region 1.Statics
	public static Vector3 SunPosition{
		get { return Instance.sunLight.transform.position; }
	}

	#endregion

	#region 2. Publics

	public int CurrentTime{
		get{ return _currentTime; }
		set{
			_currentTime = value;
			UIManager.UpdateTimeUI(_currentTime);
		}
	}


	/// <summary>
	/// Handle for animator
	/// </summary>
	public void RefreshSkybox(){
		SetSkyboxFromTime(_currentTime);
	}

	public void SetSkybox(DayPeriod period){
		currentPeriod = period;
		switch(currentPeriod){
		case DayPeriod.day:
			RenderSettings.skybox = sunny;
			break;
		case DayPeriod.dawn:
			RenderSettings.skybox = dawn;
			break;
		case DayPeriod.night:
			RenderSettings.skybox = night;
			break;
		default:
			RenderSettings.skybox = morning;
			break;
		}
	}

	public void SetSkybox(Climate.ClimateType climateType){

//		if(currentPeriod == DayPeriod.day){
//			switch(climateType){
//			case Climate.ClimateType.cloudy:
//				RenderSettings.skybox = cloudy;
//				break;
//			case Climate.ClimateType.rainy:
//				RenderSettings.skybox = rainy;
//				break;
//			case Climate.ClimateType.storm:
//				RenderSettings.skybox = stormy;
//				break;
//			case Climate.ClimateType.sunny:
//				RenderSettings.skybox = sunny;
//				break;
//			case Climate.ClimateType.snowy:
//				RenderSettings.skybox = cloudy;
//				break;
//			default:
//				break;
//			}
//		}
	}

	#endregion

	#region 3.Privates

	void SetSunLight(Climate.ClimateType currentClimate){
		sunLight.intensity = .3f;

		if(GameManager.CurrentSceneName == "Montreal"){
			if(currentPeriod != DayPeriod.night){
				switch(currentClimate){
					case Climate.ClimateType.sunny:
						sunLight.intensity = 1f;
						break;
					default:
						sunLight.intensity = .8f;
					break;
				}
			} else {
				sunLight.intensity = .6f;
			}
		}
	}

	void SetAnimBasedOnCurrentTime(int time){
		Debug.Log(time / 24f);
		timeAnim.Play("cycle", 0, time / 24f);
		SetSkyboxFromTime(time);
	}


	void IncrementTime(){
		if(_currentTime >= 23)
			_currentTime = 0;
		Globals.Instance.playerData.time = _currentTime++;
		SetSunLight(ClimateManager.CurrenClimate);
		RefreshSkybox();
		UIManager.UpdateTimeUI(_currentTime);
		if(SetTimeE != null)
			SetTimeE(_currentTime);
	}

	void SetSkyboxFromTime(int time){
		if(7 <= time && time < 9){
			SetSkybox(DayPeriod.morning);
		} else {
			if(9 <= time && time < 16){
				SetSkybox(DayPeriod.day);
			} else {
				if(16 <= time && time < 20){
					SetSkybox(DayPeriod.dawn);
				} else {
					SetSkybox(DayPeriod.night);
				}
			}
		}
	}

	void UpdateDayNight(Scene scene, LoadSceneMode loadMode){
		SetAnimBasedOnCurrentTime(_currentTime);
		RefreshSkybox();
	}

	void UpdateClimateSkybox(Climate climate){
		SetSkybox(climate.climateType);
	}

	#endregion

}
