using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class clanSelection : MonoBehaviour {

	public BaseCharacter.Clan newClan;

	private GameObject player;

	private static clanSelection instance;
	public static clanSelection Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<clanSelection>();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		if(player.GetComponent<BaseCharacter>().kind != BaseCharacter.Kind.human) {
//			Debug.Log ("Clan selection turned off");
			transform.parent.gameObject.SetActive(false);
		}
	}
	
	void OnTriggerEnter() {
		player.GetComponent<BaseCharacter>().kind = BaseCharacter.Kind.vampire;
		player.GetComponent<BaseCharacter>().clan = newClan;
		AddPowers(newClan);
	}

	public void AddPowers(BaseCharacter.Clan newClan){
		player.GetComponent<BaseCharacter>().disciplines.Clear();
		foreach(Discipline disc in DisciplineManager.Instance.GetClanRelatedDisciplines(newClan)){
			foreach(Power power in disc.powers) {
				if(power != null && !player.GetComponent<BaseCharacter>().disciplines.Contains(power))
					player.GetComponent<BaseCharacter>().disciplines.Add(power);
			}
			
		}
		UIManager.Instance.UpdateUI();
	}
}
