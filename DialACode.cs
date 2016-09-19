using UnityEngine;
using System.Collections;

public class DialACode : MonoBehaviour {

	public string codeToFind;
	public string codeEntered;

	public GameObject GOtoUnlock;

	private static DialACode instance;
	public static DialACode Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<DialACode>();
			}
			return instance;
		}
	}

	public void Init(GameObject go, string code){
		GameSettings.PlayerBC.EnableControls(false);
		codeEntered = "";
		CanvasManager.Instance.SetVisible(GetComponent<CanvasGroup>(), true);
		codeToFind = code;
		GOtoUnlock = go;
	}

	public void AddNumberToCodeEntered(string number){
		codeEntered += number;
		if(codeEntered.Length == codeToFind.Length){
			if(TestCode()){
				GameSettings.PlayerBC.EnableControls(true);
				if(GOtoUnlock.GetComponent<Container>() != null){
					GOtoUnlock.GetComponent<Container>().Unlock();
					GOtoUnlock.GetComponent<Container>().Open();
				}
				SoundManager.PlaySound(GameModel.Instance.DACSuccess);
				CanvasManager.Instance.SetVisible(GetComponent<CanvasGroup>(), false);

			} else {
				SoundManager.PlaySound(GameModel.Instance.DACError);
				CanvasManager.Instance.SetVisible(GetComponent<CanvasGroup>(), false);
			}
		}
	}

	public bool TestCode(){
		if(codeToFind == codeEntered)
			return true;
		else
			return false;
	}
}
