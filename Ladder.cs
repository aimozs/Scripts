using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]
public class Ladder : MonoBehaviour {

	public static int playerTriggerBelonging = 0;

	void Start () {
		gameObject.GetComponentInChildren<BoxCollider>().isTrigger = true;
	}

//	void OnTriggerStay(Collider other){
//		if(other.CompareTag("Player") && Input.GetAxis("Vertical") > 0.1f){
//			other.transform.Translate(Vector3.up * Input.GetAxis("Vertical"));
//		}
//
//	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			playerTriggerBelonging++;
			other.GetComponent<BaseCharacter>().Climb(true);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.CompareTag("Player")){
			playerTriggerBelonging--;
			if(playerTriggerBelonging == 0)
				other.GetComponent<BaseCharacter>().Climb(false);
		}
	}
}
