using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

[Serializable]
public class Globals : MonoBehaviour {

	public enum Conviction {anger, envy, gluttony, greed, lust, pride, sloth}
	public enum Mood {afraid, agressive, angry, bitter, calm, compassionate, confused, conservative, daydreaming, depressed, diablerist, distrustful, dominated, envious, excited, frenzied, generous, happy, hateful, idealistic, innocent, lovestruck, lustful, obsessed, psychotic, sad, spiritual, suspicious};


	public PlayerData playerData;

	private static Globals instance;
	public static Globals Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<Globals>();
			}
			return instance;
		}

		set {
			instance = value;
		}
	}

	public static int GetConvictionProgress(Conviction sin = Conviction.sloth){
		switch(sin){
		case Conviction.anger:
			return instance.playerData.progAnger;
		case Conviction.envy:
			return instance.playerData.progEnvy;
		case Conviction.gluttony:
			return instance.playerData.progGluttony;
		case Conviction.greed:
			return instance.playerData.progGreed;
		case Conviction.lust:
			return instance.playerData.progLust;
		case Conviction.pride:
			return instance.playerData.progPride;
		default:
			return instance.playerData.progSloth;
		}
	}

	public static void IncrementProgress(string conviction, int step = 0){
		IncrementProgress((Conviction) System.Enum.Parse(typeof(Conviction), conviction), step);
	}

	Conviction GetSin(string metadata){
		return (Globals.Conviction) System.Enum.Parse(typeof(Globals.Conviction), metadata);
	}

	public static void IncrementProgress(Conviction conviction, int step = 0){
		switch(conviction){
		case Conviction.anger:
			if(step == 0)
				instance.playerData.progAnger++;
			else 
				instance.playerData.progAnger = step;
			break;
		case Conviction.envy:
			if(step == 0)
				instance.playerData.progEnvy++;
			else 
				instance.playerData.progEnvy = step;
			break;
		case Conviction.gluttony:
			if(step == 0)
				instance.playerData.progGluttony++;
			else 
				instance.playerData.progGluttony = step;
			break;
		case Conviction.greed:
			if(step == 0)
				instance.playerData.progGreed++;
			else 
				instance.playerData.progGreed = step;
			break;
		case Conviction.lust:
			if(step == 0)
				instance.playerData.progLust++;
			else 
				instance.playerData.progLust = step;
			break;
		case Conviction.pride:
			if(step == 0)
				instance.playerData.progPride++;
			else 
				instance.playerData.progPride = step;
			break;
		default:
			if(step == 0)
				instance.playerData.progSloth++;
			else 
				instance.playerData.progSloth = step;
			break;
		}

//		UIManager.Notify(conviction.ToString() + "(" + GetConvictionProgress(conviction) + ")", conviction);
//		MailManager.Instance.DisplayEmail();
	}

	public static void SetConvictionProgress(Conviction sin, int newProg = 0){
		switch(sin){
		case Conviction.anger:
			instance.playerData.progAnger = newProg;
			Debug.Log("incremented anger to " + instance.playerData.progAnger);
			break;
		case Conviction.envy:
			instance.playerData.progEnvy = newProg;
			Debug.Log("incremented envy to " + instance.playerData.progEnvy);
			break;
		case Conviction.gluttony:
			instance.playerData.progGluttony = newProg;
			Debug.Log("incremented gluttony to " + instance.playerData.progGluttony);
			break;
		case Conviction.greed:
			instance.playerData.progGreed = newProg;
			Debug.Log("incremented greed to " + instance.playerData.progGreed);
			break;
		case Conviction.lust:
			instance.playerData.progLust = newProg;
			Debug.Log("incremented lust to " + instance.playerData.progLust);
			break;
		case Conviction.pride:
			instance.playerData.progPride = newProg;
			Debug.Log("incremented pride to " + instance.playerData.progPride);
			break;
		default:
			instance.playerData.progSloth = newProg;
			Debug.Log("incremented sloth to " + instance.playerData.progSloth);
			break;
		}
	}

	public static bool TestConvictionProgress(Conviction sin, int step){
		bool result = false;
		switch(sin){
		case Conviction.anger:
			result = instance.playerData.progAnger >= step;
			break;
		case Conviction.envy:
			result = instance.playerData.progEnvy >= step;
			break;
		case Conviction.gluttony:
			result = instance.playerData.progGluttony >= step;
			break;
		case Conviction.greed:
			result = instance.playerData.progGreed >= step;
			break;
		case Conviction.lust:
			result = instance.playerData.progLust >= step;
			break;
		case Conviction.pride:
			result = instance.playerData.progPride >= step;
			break;
		default:
			result = instance.playerData.progSloth >= step;
			break;
		}
		return result;
	}

}

[Serializable]
public class PlayerData {

	public string levelName = "Montreal";
	public float positionX = 0f;
	public float positionY = 0f;
	public float positionZ = 0f;

	public string charName = "";
	public string charKind = "";
	public string charOrigin = "";
	public string spectrum1 = "";
	public int spectrum1Level = 1;
	public string spectrum2 = "";
	public int spectrum2Level = 1;
	public string spectrum3 = "";
	public int spectrum3Level = 1;

	public float health = 1f;
	public float vitae = 1f;

	public float time = .75f;

	public int spectrumSelectionLevel = 0;

	public int progEgyptian = 0;
	public int progGreek = 0;
	public int progHebrew = 0;
	public int progIndian = 0;
	public int progRomanian = 0;

	public int progAnger = 0;
	public int progEnvy = 0;
	public int progGluttony = 0;
	public int progGreed = 0;
	public int progLust = 0;
	public int progPride = 0;
	public int progSloth = 0;


	//Attributes
	public int strength = 1;
	public int dexterity = 2;
	public int stamina = 1;

	public int intelligence = 1;
	public int wit = 1;
	public int resolve = 1;

	public int presence = 1;
	public int manipulation = 1;
	public int composure = 1;

	//Skills
	public int athletics = 1;
	public int brawl = 1;
	public int drive = 1;
	public int firearms = 1;
	public int larceny = 1;
	public int stealth = 1;
	public int survival = 1;
	public int weaponry = 1;

	public int academics = 1;
	public int computer = 1;
	public int crafts = 1;
	public int investigation = 1;
	public int medicine = 1;
	public int occult = 1;
	public int politics = 1;
	public int science = 1;

	public int animalKen = 1;
	public int empathy = 1;
	public int expression = 1;
	public int intimidation = 1;
	public int persuasion = 1;
	public int socialize = 1;
	public int streetwise = 1;
	public int subterfuge = 1;


	public int GetAttributeValue(string attributeName){
		switch(attributeName){
			//Physical
		case("strength"):
			return strength;
		case("dexterity"):
			return dexterity;
		case("stamina"):
			return stamina;
		
			//Mental
		case("intelligence"):
			return intelligence;
		case("wit"):
			return wit;
		case("resolve"):
			return resolve;

			//Social
		case("presence"):
			return presence;
		case("manipulation"):
			return manipulation;
		case("composure"):
			return composure;

		default:
			return 0;
		}
	}


}
