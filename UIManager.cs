using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public Image weaponUI;
	public Image disciplineUI;
	public Slider healthSlider;
	public Slider vitaeSlider;
	public Sprite basicSprite;

	private GameObject _player;
	// Use this for initialization

	private static UIManager instance;
	public static UIManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<UIManager>();
			}
			return instance;
		}
	}

	void Awake () {
		_player = GameObject.FindGameObjectWithTag("Player");
	}

	public void ToggleControls() {
		EnableControls(!_player.GetComponentInChildren<CharacterMotor>().canControl);
	}

	public void EnableControls(bool on) {
		Debug.Log ("enable controls" + on);
		_player.GetComponent<CharacterMotor>().canControl = on;
		_player.GetComponent<MouseLook>().enabled = on;
		_player.GetComponentInChildren<Camera>().GetComponentInChildren<MouseLook>().enabled = on;
	}

	public void UseVitae(float quantity){
		Debug.Log ("adjusting vistae");
		vitaeSlider.value -= quantity/100;
	}

	public void DisplayDialogue(Dialogue dialogue) {
//		EnableControls(false);
		CanvasManager.Instance.DisplayDialogue(dialogue);
	}

	public void UpdateUI() {
		weaponUI.sprite = _player.GetComponent<BaseCharacter>().weapons[_player.GetComponent<BaseCharacter>().currentWeapon].GetComponent<Weapon>().iconWeapon;
		if(_player.GetComponent<BaseCharacter>().disciplines[_player.GetComponent<BaseCharacter>().currentDiscipline].iconDiscipline != null){
			disciplineUI.GetComponentInChildren<Text>().text = null;
			disciplineUI.sprite = _player.GetComponent<BaseCharacter>().disciplines[_player.GetComponent<BaseCharacter>().currentDiscipline].iconDiscipline;
		} else {
			disciplineUI.sprite = basicSprite;
			disciplineUI.GetComponentInChildren<Text>().text = _player.GetComponent<BaseCharacter>().disciplines[_player.GetComponent<BaseCharacter>().currentDiscipline].name;
		}
	}
}
