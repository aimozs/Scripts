using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public GameObject target;
	public float attackTimer;
	public float coolDown;

	//private bool attackSuccess;
	
	// Use this for initialization
	void Start () {
		attackTimer = 0;
		coolDown = 2.0f;
		//attackSuccess = false;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(attackTimer > 0)
		{
			attackTimer -= Time.deltaTime;
		}
		if(attackTimer < 0)
		{
			attackTimer = 0;
			//attackSuccess = false;
		}
			
		//Attack when pressing G with a cooldown of 2sec
		if(Input.GetKeyUp(KeyCode.G))
		{
			if(attackTimer == 0)
			{
				Debug.Log("attacked");
				Attack();
				attackTimer = coolDown;
			}
		}


	
	}

	/*void OnGUI() //supposed to display bang! when attack succeed
	{
		if(attackSuccess == true && attackTimer > 0)
		{
			GUI.Box(new Rect(Screen.width/2, 150, 100, 30), "Attacked Successfully!");
		}
	}//*/
	
	private void Attack()
	{
		float distance = Vector3.Distance (target.transform.position, transform.position);
		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot (dir, transform.forward);
			
		//Debug.Log(direction);
		
		if(distance < 2.5f)
		{
			if(direction > 0.4f)
			{
				EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
				eh.AdjustCurHealth(-10);
				//attackSuccess = true;
			}
		}
	}
}
