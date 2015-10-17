using UnityEngine;
using System.Collections;

public class Start : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Awake ()
	{
		if(GameObject.FindWithTag("Player") == null)
			Instantiate(player, gameObject.transform.position, Quaternion.identity);
	}
	
}
