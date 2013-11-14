using UnityEngine;
using System.Collections;

public class CatLeading : MonoBehaviour {
	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
	public float catDist;

	private Transform playerTransform;
	private Transform catTransform;


	// Use this for initialization when the script is enabled (awake() is called when the script is loaded even if not enabled)
	void Start () {
		moveSpeed = 20;
		rotationSpeed = 20;
		//defining cat's target's position
		target = (GameObject.FindGameObjectWithTag("WayPoint")).transform;
		//definin cat's position
		catTransform = (GameObject.FindGameObjectWithTag("Siren")).transform;
		//defining player's position
		playerTransform = (GameObject.FindGameObjectWithTag("Player")).transform;

	
	}
	
	// Update is called once per frame
	void Update () {
		catDist = Vector3.Distance(catTransform.position, playerTransform.position);
		//testing the distance between cat and player
		if(Vector3.Distance(catTransform.position, playerTransform.position) < 5){
			// if distance < 5 move cat towards target
			catTransform.rotation = Quaternion.Slerp(catTransform.rotation, Quaternion.LookRotation(target.position - catTransform.position), rotationSpeed * Time.deltaTime);
			catTransform.position += catTransform.forward * moveSpeed * Time.deltaTime;
		}
		//if player further than 10m from the cat, then make the cat meow
		if(Vector3.Distance(catTransform.position, playerTransform.position) < 10) {
			audio.Play();
		}

		//Debug.Log(catDist);
		//Debug.DrawLine(target.position, catTransform.position, Color.red);
	}
}
