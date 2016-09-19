using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : MonoBehaviour, IInteractable {

	public enum Options {rats, flies}
	public Options option;
	public int number;
	public GameObject ratPrefab;
	public GameObject flyPrefab;

	public void OnInteract(){
		switch(option){
		case Options.rats:
			for(int n = 0; n < number; n++){
				GameObject newSpawn = (GameObject)Instantiate(ratPrefab, transform.position, Quaternion.identity);
				Destroy(newSpawn, 15f);
			}
			break;
		case Options.flies:
			for(int n = 0; n < number; n++){
				GameObject newSpawn = (GameObject)Instantiate(flyPrefab, transform.position, transform.rotation);
				Destroy(newSpawn, 5f);
			}
			break;
		}
	}

	public GameObject GetGameObject(){
		return gameObject;
	}
}
