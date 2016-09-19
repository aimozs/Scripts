using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialACodeBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PressBtn(){
		if(GetComponentInChildren<Text>() != null)
			DialACode.Instance.AddNumberToCodeEntered(GetComponentInChildren<Text>().text);

		if(GameModel.Instance.DACPressBtn != null)
			SoundManager.PlaySound(GameModel.Instance.DACPressBtn);
	}
}
