using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerManager : MonoBehaviour {

	public enum SpectrumName {Animalism, Auspex, Celerity, Dominate, Majesty, Nightmare, Obfuscate, Protean, Resilience, Vigor, Faith};

	public Discipline animalism;
	public Discipline auspex;
	public Discipline celerity;
	public Discipline dominate;
	public Discipline majesty;
	public Discipline nightmare;
	public Discipline obfuscate;
	public Discipline protean;
	public Discipline resilience;
	public Discipline vigor;

	public List<Discipline> spectrumEgyptian = new List<Discipline>();
	public List<Discipline> spectrumGreek = new List<Discipline>();
	public List<Discipline> spectrumHebrew = new List<Discipline>();
	public List<Discipline> spectrumIndian = new List<Discipline>();
	public List<Discipline> spectrumRomanian = new List<Discipline>();
	public List<Discipline> spectrumHuman = new List<Discipline>();

	private static PowerManager instance;
	public static PowerManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<PowerManager>();
			}
			return instance;
		}
	}

//	public List<Power> GetFirstSetPowers(OriginManager.OriginEnum origin) {
////		Debug.Log ("Getting disciplines");
//		List<Power> firstSet = new List<Power>();
//
//		switch(origin) {
//		case OriginManager.OriginEnum.Egyptian:
//			return spectrumEgyptian[0].powers;
//		case OriginManager.OriginEnum.Greek:
//			return spectrumGreek[0].powers;
//		case OriginManager.OriginEnum.Hebrew:
//			return spectrumHebrew[0].powers;
//		case OriginManager.OriginEnum.Indian:
//			return spectrumIndian[0].powers;
//		case OriginManager.OriginEnum.Romanian:
//			return spectrumRomanian[0].powers;
//		default:
////			Debug.Log ("getting human disc");
//			return spectrumHuman[0].powers;
//		}
//	}

	public Discipline GetSpectrum(string spectrumName){
//		Debug.Log(spectrumName);
		spectrumName = spectrumName.Substring(0,3);
		spectrumName = spectrumName.ToLower();
//		Debug.Log(spectrumName);
		switch(spectrumName){
		case "ani":
			return animalism;
		case "aus":
			return auspex;
		case "cel":
			return celerity;
		case "dom":
			return dominate;
		case "maj":
			return majesty;
		case "nig":
			return nightmare;
		case "obf":
			return obfuscate;
		case "pro":
			return protean;
		case "res":
			return resilience;
		case "vig":
			return vigor;
		default:
			return null;
		}
	}

}
