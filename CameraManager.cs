using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public Transform camera1stPerson;
	public Transform camera3rdPerson;

	private bool first = true;
//	private float speed = 3f;
	private Transform camTransform;
	private BaseCharacter _playerBC;

	private static CameraManager instance;
	public static CameraManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<CameraManager>();
			}
			return instance;
		}
	}

	void Start(){
		camTransform = camera1stPerson;
		_playerBC = GameSettings.Instance.player.GetComponent<BaseCharacter>();
	}

	void Update(){
		transform.position = camTransform.position;
////		transform.position = Vector3.Lerp(transform.position, camTransform.position, Time.deltaTime * speed);
	}

	public void SwitchPersonCamera(){
		first = !first;
		SetFirstPersonCamera(first);
	}

	public void SetFirstPersonCamera(bool on){
		if(_playerBC == null)
			_playerBC = GameSettings.Instance.player.GetComponent<BaseCharacter>();
		
		if(on){
			camTransform = camera1stPerson;
			_playerBC.ActivateCharModel(false);
		} else {
			camTransform = camera3rdPerson;
			_playerBC.ActivateCharModel(true);
		}
	}

	public void Swim(bool swim){
		if(swim){
			RenderSettings.fog = true;
			RenderSettings.fogMode = FogMode.ExponentialSquared;
			RenderSettings.fogDensity = .1f;
			RenderSettings.fogColor = new Color(.15f, .25f, .45f, 1f);
		} else {
			RenderSettings.fog = false;
		}
	}

	public void ToggleCrouch(bool on){
		if(on){
			camera1stPerson.localPosition = new Vector3(0f, 1f, 0f);
		} else {
			camera1stPerson.localPosition = new Vector3(0f, 1.7f, 0f);
		}
	}
}
