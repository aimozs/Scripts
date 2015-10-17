using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisciplineManager : MonoBehaviour {

	public enum DisciplineName {Animalism, Auspex, Celerity, Dominate, Majesty, Nightmare, Obfuscate, Protean, Resilience, Vigor, Faith};

	public List<Discipline> disciplinesDaeva = new List<Discipline>();
	public List<Discipline> disciplinesGangrel = new List<Discipline>();
	public List<Discipline> disciplinesMekhet = new List<Discipline>();
	public List<Discipline> disciplinesNosferatu = new List<Discipline>();
	public List<Discipline> disciplinesVentrue = new List<Discipline>();
	public List<Discipline> disciplinesHuman = new List<Discipline>();

	private static DisciplineManager instance;
	public static DisciplineManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<DisciplineManager>();
			}
			return instance;
		}
	}

	public List<Discipline> GetClanRelatedDisciplines(BaseCharacter.Clan clan) {
		Debug.Log ("Getting disciplines");
		switch(clan) {
		case BaseCharacter.Clan.Daeva:
			return disciplinesDaeva;
			break;
		case BaseCharacter.Clan.Gangrel:
			return disciplinesGangrel;
			break;
		case BaseCharacter.Clan.Mekhet:
			return disciplinesMekhet;
			break;
		case BaseCharacter.Clan.Nosferatu:
			return disciplinesNosferatu;
			break;
		case BaseCharacter.Clan.Ventrue:
			return disciplinesVentrue;
			break;
		default:
			Debug.Log ("getting human disc");
			return disciplinesHuman;
			break;
		}
	}
}
