using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]
public class Water : MonoBehaviour {


	void Start () {
		GetComponent<BoxCollider>().isTrigger = true;
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			other.GetComponent<BaseCharacter>().Swim(true);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.CompareTag("Player")){
			other.GetComponent<BaseCharacter>().Swim(false);
		}
	}

}
