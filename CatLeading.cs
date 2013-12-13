using UnityEngine;
using System.Collections;

public class CatLeading : MonoBehaviour
{
	//two transforms that define the distance between enemies and player
	private Transform playerTransform;
	private Transform catTransform;
	//distance betweem cat and player
	public float catDist;
	//define where the cat is going
	public Transform target;
	//if catDist < 5 define how fast the cat is going
	public int moveSpeed;
	public int rotationSpeed;
	//define if the cat is meowing
	public bool catMeowing;




	// Use this for initialization when the script is enabled (awake() is called when the script is loaded even if not enabled)
	void Start ()
	{
		moveSpeed = 6;
		rotationSpeed = 20;
		//defining cat's target's position
		target = (GameObject.FindGameObjectWithTag("WayPoint")).transform;

		//defining cat's position
		catTransform = (GameObject.FindGameObjectWithTag("Siren")).transform;

		//defining player's position
		playerTransform = (GameObject.FindGameObjectWithTag("Player")).transform;

	
	}
	
	// Update is called once per frame
	void Update ()
	{
		catDist = Vector3.Distance(catTransform.position, playerTransform.position);

		//testing the distance between cat and player
		if(Vector3.Distance(catTransform.position, playerTransform.position) < 8)
		{
			// if distance < 5 move cat towards target
			catTransform.rotation = Quaternion.Slerp(catTransform.rotation, Quaternion.LookRotation(target.position - catTransform.position), rotationSpeed * Time.deltaTime);
			catTransform.position += catTransform.forward * moveSpeed * Time.deltaTime;
		}

		//each frame put catmeowing at false and check if the sound is playing, if playing but it back to true
		catMeowing = false;
		if(GameObject.Find("catSiamese").audio.isPlaying)
		{
			catMeowing = true;
		}

		//if player further than 10m from the cat & the sounds is not already playing, then make the cat meow
		if((Vector3.Distance(catTransform.position, playerTransform.position) > 10) && !catMeowing)
		{
			audio.Play();
		}

		//Debug.Log(catDist);
		//Debug.DrawLine(target.position, catTransform.position, Color.red);
	}
}
