using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]
public class Water : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<BoxCollider>().isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			Debug.Log ("player is swimming");
			col.GetComponent<BaseCharacter>().Swimming(true);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			Debug.Log ("player is NOT swimming");
			col.GetComponent<BaseCharacter>().Swimming(false);
		}
	}
}
