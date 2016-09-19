using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class Crate : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if(col.CompareTag("Water")) {
			GetComponent<Rigidbody>().useGravity = false;
		}
	}

	void OnTriggerExit(Collider col) {
		if(col.CompareTag("Water")) {
			GetComponent<Rigidbody>().useGravity = true;
		}
	}
}
