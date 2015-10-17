using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Power : MonoBehaviour {

	public GameObject player;
	public Sprite iconDiscipline;

	private Light highSense;
	private int _costVitae = 5;

	public delegate void SwitchAura();
	public static event SwitchAura OnSwitchAura;

	void Start(){
		player = GameObject.FindWithTag("Player");
	}

	public void Attack(){
		switch(gameObject.name){
		//ANIMALISM
		case "Feral Whispers":
			StartCoroutine(StartFeralWhispers(10f));
			break;

		// AUSPEX
		case "Heightened Senses":
			StartCoroutine(StartHighSense(10f));
			break;
		case "Aura Perception":
			StartCoroutine(StartAura(10f));
			break;
		case "Spirit Touch":
			StartCoroutine(StartSpirit(10f));
			break;

		// CELERITY
		case "Celerity":
			StartCoroutine(StartCelerity(10f, .5f));
			break;

		// DOMINATE
		case "Command":
			StartCoroutine(StartCommand(10f));
			break;

		// PROTEAN
		case "Predatory Aspect":
			StartCoroutine(StartPredatory(10f));
			break;

		// OBFUSCATE
		case "Touch of Shadow":
			StartCoroutine(StartTouchShadow(10f));
			break;

		case "Face in the Crowd":
			StartCoroutine(StartFaceCrowd(10f));
			break;

		// RESILIENCE
		case "Resilience":
			StartCoroutine(StartResilience(10f));
			break;

		// VIGOR
		case "Vigor":
			StartCoroutine(StartVigor(10f));
			break;
		
		default:
			Debug.LogWarning ("Could not find which discipline to trigger");
			break;
		}
		UIManager.Instance.UseVitae((float)_costVitae);
	}
	
	IEnumerator StartSpirit(float time){
		yield return new WaitForSeconds(time);
		StopSpirit();
	}
	
	void StopSpirit(){
	}

	IEnumerator StartFaceCrowd(float time){
		yield return new WaitForSeconds(time);
		StopFaceCrowd(player.GetComponent<GameSettings>().interactable);
	}
	
	void StopFaceCrowd(GameObject interactable){
	}

	IEnumerator StartTouchShadow(float time){
		GameObject interactable = player.GetComponent<GameSettings>().interactable;
		if(interactable.GetComponent<Item>()){
			interactable.GetComponent<MeshRenderer>().enabled = false;
			yield return new WaitForSeconds(time);
			StopTouchShadow(interactable);
		}
	}
	
	void StopTouchShadow(GameObject interactable){
		interactable.GetComponent<MeshRenderer>().enabled = true;
	}

	IEnumerator StartPredatory(float time){
		yield return new WaitForSeconds(time);
		StopPredatory();
	}

	void StopPredatory(){
	}

	IEnumerator StartResilience(float time){
		yield return new WaitForSeconds(time);
		StopResilience();
	}
	
	void StopResilience(){
	}

	IEnumerator StartAura(float time){
		if(OnSwitchAura != null)
			OnSwitchAura();
		yield return new WaitForSeconds(time);
		StopAura();
	}
	
	void StopAura(){
		if(OnSwitchAura != null)
			OnSwitchAura();
	}

	IEnumerator StartCommand(float time){
		if(GameSettings.Instance.interactable.CompareTag("NPC")){
			if(OnSwitchAura != null)
				OnSwitchAura();
		}
		yield return new WaitForSeconds(time);
		StopCommand();
	}
	
	void StopCommand(){
		if(OnSwitchAura != null)
			OnSwitchAura();
	}

	IEnumerator StartVigor(float time){
		player.GetComponent<CharacterMotor>().jumping.baseHeight = 10f;
		yield return new WaitForSeconds(time);
		StopVigor();
	}
	
	void StopVigor(){
		player.GetComponent<CharacterMotor>().jumping.baseHeight = 1f;
	}

	IEnumerator StartFeralWhispers(float time){
		if(!player.GetComponent<BaseCharacter>().FeralWhispers)
			player.GetComponent<BaseCharacter>().FeralWhispers = true;
		yield return new WaitForSeconds(time);
		StopFeralWhispers();
	}
	
	void StopFeralWhispers(){
		player.GetComponent<BaseCharacter>().FeralWhispers = false;
	}

	IEnumerator StartHighSense(float time){
		if(highSense == null)
			highSense = GameObject.Find("heightenedSenses").GetComponent<Light>();
		highSense.intensity = 1f;
		yield return new WaitForSeconds(time);
		StopHighSense();
	}
	
	void StopHighSense(){
		highSense.intensity = 0f;
	}

	IEnumerator StartCelerity(float time, float speed = 1f){
		Time.timeScale = speed;
		yield return new WaitForSeconds(time);
		StopCelerity();
	}

	void StopCelerity(){
		Time.timeScale = 1f;
	}

}
