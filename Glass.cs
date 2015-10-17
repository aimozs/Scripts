using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]
public class Glass : MonoBehaviour
{

	public GameObject brokenGlass;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnInteract()
	{
		if(brokenGlass != null)
		{
			Instantiate(brokenGlass, transform.position, transform.rotation);
			Destroy(gameObject);
		}

	}
}
