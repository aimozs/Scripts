using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsManager : MonoBehaviour {

	public Slider mainVolume;
	public Slider displayReticle;

	public InputField levelSelect;

	// Use this for initialization
	void Start () {
	
	}
	
	public void AdjustVolume(){
		if(mainVolume != null)
			SoundManager.Instance.fxSource.volume = mainVolume.value;
	}

	public void DisplayReticle(){
		if(displayReticle.value == 0)
			UIManager.ToogleReticle(false);
		else
			UIManager.ToogleReticle(true);
	}

//	public void GetLevelName(){
//		levelName.text = GameManager.GetSceneName(int.Parse(levelSelect.text));
//	}

	public void LoadLevel(){
		GameManager.Instance.LoadScene(levelSelect.text);
	}
}
