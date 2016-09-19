using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]
public class Tip : MonoBehaviour {

	public string tip;
	public AudioClip clip;
	public AudioSource targetSource;

	public bool displayOnlyOnce = true;
	public Globals.Conviction conviction;
	public int maxConvictionStep;

	private bool displayed = false;

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			if(displayOnlyOnce){
				if(!displayed){
					displayed = true;
					DisplayTip();
				}
			} else {
				DisplayTip();
			}
		}
	}

	void DisplayTip(){
		if(tip != "" && Globals.GetConvictionProgress(conviction) <= maxConvictionStep){
			UIManager.Notify(tip);

			if(clip != null){
				if(targetSource != null){
					targetSource.clip = clip;
					targetSource.Play();
				} else {
					GameSettings.Instance.player.GetComponent<BaseCharacter>().PlayFxSound(clip);
				}
			}
		}
	}
}
