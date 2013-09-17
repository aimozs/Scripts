/*using UnityEngine;
using System.Collections;

public class LoadLvl : MonoBehaviour {
	
	public string level;
	public GameObject player;
	// Use this for initialization
	void Start () {
	//discover at each scene load the FPC and assign it to player
	player = GameObject.Find("Player Character");
	// Make this game object and all its transform children survive when loading a new scene.
	DontDestroyOnLoad (player);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


function OnInteract() {
	Application.LoadLevel(level);
	//need to find how to know which object we interact with so we can look for the same object in the new scene to spawn at the right place
}*/