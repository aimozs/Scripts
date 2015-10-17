using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]

public class NextLevel : MonoBehaviour
{

	public string level;
	public string spawn;
	public bool activateNextCheckpoint;
	public GameObject NextCheckpoint;

	public NextLevel GetNextLevelInfo() {
		return this;
	}

	public void Load() {
		if( level != null && spawn != null) {
			Debug.Log("Lead to another level: " + level + ", and we're spawing at " + spawn);
			Application.LoadLevel(level);
		} else {
			Debug.Log("we are missing one of those information, level is: " + level + ", and spawn is: " + spawn);
		}
	}

	//load on trigger enter
	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.CompareTag("Player")) {
			collider.gameObject.GetComponent<GameSettings>().LoadNextLevel = this;
			Load();
		}
	}

	//load on interact
	void OnInteract() {
		Load();
	}
}