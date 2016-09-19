using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class TimedPower : MonoBehaviour {

	private bool _counting = false;
	private float _totalDuration;
	private float _timeLeft;

	private GameObject _power;
	private Slider _slider;

	void Start(){
		_slider = GetComponent<Slider>();
	}

	void Update(){
		if(_counting){
			_timeLeft -= Time.deltaTime;
			_slider.value = _timeLeft / _totalDuration;
			if ( _timeLeft < 0 ) {
				UIManager.RemoveFromActivePowers(_power);
				Destroy(gameObject);
			}
		}
	}

	public void StartTimer(GameObject newPower, float time){
		_power = newPower;
		_totalDuration = _timeLeft = time;
		GetComponentInChildren<Image>().sprite = newPower.GetComponent<Power>().iconPower;
		_counting = true;
	}

	public void ResetTimer(float newTime){
		_timeLeft = newTime;
	}


}
