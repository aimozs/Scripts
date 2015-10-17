using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayNightCycle : MonoBehaviour {

	public enum Mode {Sun, Moon};

	public List<Material> skyboxes;
	public Mode mode;

	private static int currentSkybox = 0;
	Vector3 position;

	void Start () {
		position = transform.eulerAngles;
		StartCoroutine(IncrementCycle());
	}

	IEnumerator IncrementCycle(){
		Debug.Log ("incrementing day/night cyccle");
		if(mode == Mode.Moon){
			position = new Vector3(position.x, position.y, position.z+1);
			transform.eulerAngles = position;

		} else {
			position = new Vector3(position.x, position.y, position.z+1);
			transform.eulerAngles = position;
//			if(transform.eulerAngles.z < 0){

			if(currentSkybox < skyboxes.Count-1)
				currentSkybox++;
			else
				currentSkybox = 0;

			RenderSettings.skybox = skyboxes[currentSkybox];
//			}
		}
		yield return new WaitForSeconds(60f);
		StartCoroutine(IncrementCycle());
	}
}
