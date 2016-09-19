using UnityEngine;
using System.Collections;

public class AttributeManager : MonoBehaviour {

	public enum AttributeName {none, strength, dexterity, stamina, intelligence, wits, resolve, apparence, manipulation, composure};

	private static AttributeManager instance;
	public static AttributeManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<AttributeManager>();
			}
			return instance;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
