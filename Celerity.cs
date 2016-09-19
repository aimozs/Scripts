using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Celerity : MonoBehaviour, IPower {

	public float costVitae = 5f;
	public float speed = .8f;

	public int level = 1;
	public Sprite icon;

	const float BASIC_TIME = 10f;

	public float CostVitae{
		get{
			return costVitae;
		}

		set{
			costVitae = value;
		}
	}

	public int Level{
		get{
			return level;
		}

		set{
			level = value;
		}
	}



	public Sprite Icon{
		get{
			return icon;
		}

		set{
			icon = value;
		}
	}

	public void StartPower () {
//		float time = BASIC_TIME * level;
		GameSettings.Instance.player.GetComponent<BaseCharacter>().dexterityBonus += level;
//		UIManager.Instance.AddTimedPower(this, time);
		Time.timeScale = speed;

	}
	
	public void StopPower(){
		GameSettings.Instance.player.GetComponent<BaseCharacter>().dexterityBonus -= level;
		Time.timeScale = 1f;
	}
}
