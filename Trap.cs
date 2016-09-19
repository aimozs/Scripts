using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class Trap : MonoBehaviour {

	public enum trapType {electricity, fire, steam}

	public AudioClip trapSound;
	public float damage = 2f;
	public GameObject lockedItem;

	private AudioSource _audioSource;

	void Start(){
		_audioSource = GetComponent<AudioSource>();
	}

	public void ApplyDamage(){
		BaseCharacter playerChar = GameSettings.Instance.player.GetComponent<BaseCharacter>();
		playerChar.AdjustVitae(-damage / (playerChar.staminaBonus + Globals.Instance.playerData.stamina));
		if(trapSound != null)
			_audioSource.PlayOneShot(trapSound);
	}
}
