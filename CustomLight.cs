using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class CustomLight : MonoBehaviour {

	public enum LightMode { none, candle, flickering, fadeWave }
	public LightMode mode;
	public RuntimeAnimatorController controller;

	private Animator _anim;

	// Use this for initialization
	void Start () {
		_anim = GetComponent<Animator>();

		if(_anim != null){
			if(_anim.runtimeAnimatorController == null)
				_anim.runtimeAnimatorController = controller;
			if(mode != LightMode.none)
				_anim.SetTrigger(mode.ToString());
		}
	}
	
	// Update is called once per frame
//	void Update () {}
}
