using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
	public GameObject target;
	public float attackTimer;
	public float coolDown;
	
	// Use this for initialization
	void Start ()
	{
		attackTimer = 0;
		coolDown = 2.0f;
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(attackTimer > 0)
			attackTimer -= Time.deltaTime;
		
		if(attackTimer < 0)
			attackTimer = 0;
		

		if(attackTimer ==0)
		{
			Attack();
			attackTimer =  coolDown;

		}

	}
	
	private void Attack()
	{
		//define enemy's target by finding the Player
		target = GameObject.Find("First Person Controller");
		//give the distance between the player(target) and the enemy
		float distance = Vector3.Distance (target.transform.position, transform.position);

		//define the angle between the forward direction of the enemy and the transform of the player
		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot (dir, transform.forward);
			
		//Debug.Log(distance);
		//test if the enemy is close enough to the player
		if(distance < 2.5f)
		{
			//test is the player is in front of the enemy or not
			if(direction > 0.4f)
			{
				//apply the damage to the playerHealth
				PlayerHealth ph = (PlayerHealth)target.GetComponent("PlayerHealth");
				ph.AdjustCurHealth(-10);
			}
		}
	}
}
