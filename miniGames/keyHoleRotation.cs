using UnityEngine;
using System.Collections;


public class keyHoleRotation : MonoBehaviour {
	
	private int speed = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	//rotate the gameobject at speed's speed
	if (Input.GetKey(KeyCode.G)) {
			transform.Rotate(0, 0, speed * Time.deltaTime);
		}
	if (Input.GetKey(KeyCode.H)) {
			transform.Rotate(0, 0, -speed * Time.deltaTime);
		}
	
	}
	void OnCollisionEnter(Collision lockpick) {
        foreach (ContactPoint contact in lockpick.contacts) {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
	}
}