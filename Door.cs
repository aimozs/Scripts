using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (AudioSource))]
[RequireComponent (typeof (Interactable))]
public class Door : MonoBehaviour, ILockable, IInteractable {
	public enum MoveDirection {left, right}

	public MoveDirection moveDirection;
	public bool isOpen;
	public AudioClip soundEffect;

	public bool isLocked = false;
	public GameObject key;
	public bool deleteKeyOnUnlock = true;
	public int doorLevel = 5;

	public Trap trap;

	private AudioSource _audioSource;
	private Animator anim;
	private GameObject player;

	void Start () {
		anim = gameObject.GetComponent<Animator>();

		if(trap == null)
			trap = GetComponentInChildren<Trap>();

		if(_audioSource == null)
			_audioSource = GetComponent<AudioSource>();
		
		Init();
	}

	void Init(){
		Open(isOpen);
	}

	public void OnInteract() {
		if(player == null)
			player = GameObject.FindGameObjectWithTag("Player");

		if(isLocked) {
			if(key != null) {
				if(player.GetComponent<BaseCharacter>().inventory.Find(obj=>obj.name==key.name)) {
					Unlock();
					UIManager.Notify("You have used the key to open this door");
				} else {
					if(doorLevel < Globals.Instance.playerData.strength + player.GetComponent<BaseCharacter>().strengthBonus){
						Unlock();
						UIManager.Notify("You used your unnatural strength to force the door");
					} else {
						UIManager.Notify("This lock seems stronger than usual..");
					}

					if(trap != null){
						trap.ApplyDamage();
					}
				}
			} else {
				UIManager.Notify("This door is locked");
			}

		} else {
			Open(!isOpen);
		}
	}

	public GameObject GetGameObject(){
		return gameObject;
	}

	public void Open(bool open){
		switch(moveDirection){
		case MoveDirection.left:
			anim.SetBool("OpenLeft", open);
			break;
		default:
			anim.SetBool("OpenRight", open);
			break;
		}

		if(soundEffect != null && _audioSource != null)
			_audioSource.PlayOneShot(soundEffect);

		isOpen = open;
	}

	public void Lock(){
		
	}

	public void Unlock(){
		isLocked = false;

		if(player == null)
			player = GameObject.FindGameObjectWithTag("Player");

		//when unlock

		if(trap != null)
			trap.gameObject.SetActive(false);
		
		if(key != null && player != null) {
			if(player.GetComponent<BaseCharacter>().inventory.Contains(key) && deleteKeyOnUnlock) {
				player.GetComponent<BaseCharacter>().inventory.Remove(key);
			}
		}
	}
}