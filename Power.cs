using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Power : MonoBehaviour {

	public int _costVitae = 5;
	public int level = 1;
	public Sprite iconPower;
	public GameObject effect;
	public AudioClip audioFx;

	private Animator visual;
	private Light highSense;
	private static BaseCharacter playerChar;

	const float BASIC_TIME = 10f;

	public delegate void SwitchAura();
	public static event SwitchAura OnSwitchAura;

	void Start(){
		GetPlayerChar();
	}

	public float CostVitae{
		get{ return (float)_costVitae / 100f; }
		set{ _costVitae = (int)value; }
	}

	void GetPlayerChar(){
		playerChar = GameSettings.Instance.player.GetComponent<BaseCharacter>();
	}

	public void Attack(){

		if(playerChar == null)
			GetPlayerChar();
		
		if(visual == null)
			visual = GameSettings.Instance.player.GetComponent<BaseCharacter>().powerVis;

		if(visual != null) {
//			visual.SetTrigger(gameObject.name);
			switch(gameObject.name){
			//ANIMALISM
			case "Feral Whispers":
				StartCoroutine(StartFeralWhispers(BASIC_TIME * level));
				break;

				// AUSPEX
			case "Heightened Senses":
				StartCoroutine(StartHighSense(BASIC_TIME * level));
				break;
			case "Aura Perception":
				StartCoroutine(StartAura(BASIC_TIME * level));
				break;
			case "Spirit Touch":
				StartCoroutine(StartSpirit(BASIC_TIME * level));
				break;

				// CELERITY
			case "CelerityP":
				StartCoroutine(StartCelerity(.5f));
				break;

				// DOMINATE
			case "Command":
				StartCoroutine(StartCommand(BASIC_TIME * level));
				break;

				// PROTEAN
			case "Predatory Aspect":
				StartCoroutine(StartPredatory(BASIC_TIME * level));
				break;

				// OBFUSCATE
			case "Touch of Shadow":
				StartCoroutine(StartTouchShadow(BASIC_TIME * level));
				break;

			case "Face in the Crowd":
				StartCoroutine(StartFaceCrowd(BASIC_TIME * level));
				break;

				// RESILIENCE
			case "ResilienceP":
				StartCoroutine(StartResilience(BASIC_TIME * level));
				break;

				// VIGOR
			case "VigorP":
				StartCoroutine(StartVigor(BASIC_TIME * level));
				break;

			case "Faith":
				StartCoroutine(StartFaith(BASIC_TIME * level));
				break;

			default:
				Debug.LogWarning ("Could not find which discipline to trigger");
				break;
			}
		}
	}
	
	IEnumerator StartSpirit(float time){
		yield return new WaitForSeconds(time);
	}
	
	IEnumerator StartFaith(float time){
		
		yield return new WaitForSeconds(time);
	}

	IEnumerator StartFaceCrowd(float time){
		yield return new WaitForSeconds(time);
	}
	
	IEnumerator StartTouchShadow(float time){
		GameObject interactable = GameSettings.Instance.interactable;
		if(interactable != null && interactable.GetComponent<Item>() != null){
			GameObject shadowed = (GameObject) Instantiate(GameModel.Instance.shadowPrefab, interactable.transform.position, Quaternion.identity);
			UIManager.AddTimedPower(gameObject, time);
			interactable.SetActive(false);

			yield return new WaitForSeconds(time);
			shadowed.GetComponent<ParticleSystem>().Stop();
			interactable.SetActive(true);

			yield return new WaitForSeconds(time);
			Destroy(shadowed);
		}
	}
	
	IEnumerator StartPredatory(float time){
		yield return new WaitForSeconds(time);
	}

	IEnumerator StartResilience(float time){
		
		GameSettings.PlayerBC.sunProtected = true;
		GameSettings.PlayerBC.staminaBonus += level;
		Debug.Log(GameSettings.PlayerBC.staminaBonus);
		UIManager.AddTimedPower(gameObject, time);

		if(effect != null)
			GameSettings.PlayerBC.ActivePowerEffect(effect, true, time);
		
		yield return new WaitForSeconds(time);

		GameSettings.PlayerBC.sunProtected = false;
		GameSettings.PlayerBC.staminaBonus -= level;
	}
	
	IEnumerator StartAura(float time){
		if(OnSwitchAura != null)
			OnSwitchAura();
		yield return new WaitForSeconds(time);

		if(OnSwitchAura != null)
			OnSwitchAura();
	}

	IEnumerator StartCommand(float time){
		if(GameSettings.Instance.interactable.CompareTag("NPC")){
			if(OnSwitchAura != null)
				OnSwitchAura();
		}
		yield return new WaitForSeconds(time);

		if(OnSwitchAura != null)
			OnSwitchAura();
	}
	
	IEnumerator StartVigor(float time){
		
		GameSettings.PlayerBC.strengthBonus += level;
		GameSettings.PlayerBC.UpdateJumpSpeed();
		UIManager.AddTimedPower(gameObject, time);

		if(effect != null)
			GameSettings.PlayerBC.ActivePowerEffect(effect, true, time);

		yield return new WaitForSeconds(time);

		GameSettings.PlayerBC.strengthBonus -= level;
		GameSettings.PlayerBC.UpdateJumpSpeed();
	}
	
	IEnumerator StartFeralWhispers(float time){
		GameSettings.PlayerBC.FeralWhispers = true;

		yield return new WaitForSeconds(time);

		GameSettings.PlayerBC.FeralWhispers = false;
	}
	
	IEnumerator StartHighSense(float time){
		if(highSense == null)
			highSense = GameObject.Find("heightenedSenses").GetComponent<Light>();

		if(highSense != null){
			highSense.intensity = .3f;
			UIManager.AddTimedPower(gameObject, time);
			if(effect != null)
				GameSettings.PlayerBC.ActivePowerEffect(effect, true, time);
		}
		yield return new WaitForSeconds(time);
		highSense.intensity = 0f;
	}

	IEnumerator StartCelerity(float speed = 1f){
		float time = BASIC_TIME * level;
		GameSettings.PlayerBC.dexterityBonus += level;
		UIManager.AddTimedPower(gameObject, time);
		Time.timeScale = speed;
		GameSettings.PlayerBC.ActivePowerEffect(effect, true, time);
		yield return new WaitForSeconds(time);
		GameSettings.PlayerBC.dexterityBonus -= level;
		Time.timeScale = 1f;
	}

}
