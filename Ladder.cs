using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]
public class Ladder : MonoBehaviour
{
	private GameObject player;
	private float height;
	// Use this for initialization
	void Start ()
	{
		gameObject.GetComponentInChildren<BoxCollider>().isTrigger = true;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if(player == null)
		//	player = GameObject.FindWithTag("Player");
		if(player != null)
		{
			if(Input.GetKey(KeyCode.W))
			{
				Debug.Log ("Player " + player.name + " is climbing (pos:)" + height);
				height = height + .05f;
				Vector3 up = new Vector3(player.transform.position.x, height, player.transform.position.z);
					Debug.Log(up);
				player.transform.position = up;
			}
			else
				player.transform.position = new Vector3(player.transform.position.x, height, player.transform.position.z);
		}


	}


	void OnTriggerEnter(Collider collider)
	{
		//Detect player
		if(collider.transform.root.gameObject.CompareTag("Player"))
		{
			player = collider.transform.root.gameObject;
			Debug.Log ("player detected: " + player.name);
			height = player.transform.position.y;
			Debug.Log (height);
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if(collider.transform.root.gameObject.CompareTag("Player"))
		{
			player = collider.transform.root.gameObject;
			Debug.Log ("player exiting : " + player.name);
			player = null;
		}
	}
}
