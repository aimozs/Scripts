using UnityEngine;
using System.Collections;

public class CustomPlayerFX : MonoBehaviour {

	public ParticleSystem clouds;
	public ParticleSystem snow;
	public ParticleSystem rain;
	public ParticleSystem leafs;
	public ParticleSystem dust;
	public ParticleSystem fireflies;

	private static CustomPlayerFX instance;
	public static CustomPlayerFX Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<CustomPlayerFX>();
			}
			return instance;
		}
	}


	#region 0.Basics
	// Use this for initialization
//	void Start () {}
	
	// Update is called once per frame
//	void Update () {}

	#endregion

	#region 1.Statics

	public static void StartSnow(bool on){
		StartClouds(on);
		if(Instance.snow != null){
			if(on){
				Instance.dust.Play();
				Instance.snow.Play();
			} else {
				Instance.dust.Stop();
				Instance.snow.Stop();
			}
		}
	}

	public static void StartRain(bool on){
		StartClouds(on);
		if(Instance.rain != null){
			if(on){
				Instance.rain.Play();
				Instance.leafs.Play();
			} else {
				Instance.rain.Stop();
				Instance.leafs.Stop();
			}
		}
	}

	public static void StartFireflies(bool on){
		if(Instance.fireflies != null){
			if(on){
				Instance.fireflies.Play();
			} else {
				Instance.fireflies.Stop();
			}
		}
	}

	public static void StartClouds(bool on){
		if(Instance.clouds != null){
			if(on){
				Instance.clouds.Play();
			} else {
				Instance.clouds.Stop();
			}
		}
	}

	#endregion
}
