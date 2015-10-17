using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class Door : MonoBehaviour
{
	public enum MoveType {sliding, rotating}

	public MoveType moveType;
	public bool isLocked = false;
	public GameObject key;

	private Animator anim;
	private GameObject player;

	void Start ()
	{
		anim = gameObject.GetComponent<Animator>();
	}

	void OnInteract()
	{
		if(player == null)
			player = GameObject.FindGameObjectWithTag("Player");

		if(isLocked)
		{
			Debug.Log("door locked");
			if(key!=null)
			{
				Debug.Log("looking for " + key.name + " in list of " + player.GetComponent<BaseCharacter>().inventory.Count);
			}
			else
			{
				Debug.Log("this door is locked");
			}

			if(player.GetComponent<BaseCharacter>().inventory.Find(obj=>obj.name==key.name))
			{
				isLocked = false;
				player.GetComponent<BaseCharacter>().inventory.Remove(key);
				Debug.Log("You have used the key to open this door");
			}
			else
			{
				Debug.Log("You need to find the key first!");
			}
		}
		else
		{
			switch(moveType)
			{
			case MoveType.sliding:
				anim.SetTrigger("slide");
				break;
			default:
				anim.SetTrigger("rotateRight");
				break;
			}

		}
	}
}