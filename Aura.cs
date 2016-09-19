using UnityEngine;
using System.Collections;

public class Aura : MonoBehaviour {

	// Use this for initialization
//	void Start () {}

	// Update is called once per frame
//	void Update () {}

	void OnEnable(){
		Power.OnSwitchAura += SwitchAura;
	}

	void OnDisable(){
		Power.OnSwitchAura -= SwitchAura;
	}

	void SwitchAura() {
		GetComponent<ParticleSystem>().Play();
//		GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
	}
	

}
