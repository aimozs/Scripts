using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
	//two transforms that define the distance between enemies and player
	private Transform playerTransform;
	private Transform enemyTransform;
	//distance betweem enemies and player
	public float enemyDist;
	//depending on the distance, tell the OnGUI to display enemy's healthbar
	public bool displayEnemyHealth = false;
	//define how big is the healthBar
	public float enemyHealthBarLength;
	public float maxHealthBarLength;

	public int maxHealth = 100;
	public int curHealth = 100;
	

	void Start ()
	{
		maxHealthBarLength = Screen.width / 3;

		//defining enemy's position
		enemyTransform = (GameObject.FindGameObjectWithTag("Enemy")).transform;

		//defining player's position
		playerTransform = (GameObject.FindGameObjectWithTag("Player")).transform;
	}
	

	void Update ()
	{
		AdjustCurHealth(0);

		//define the distance between player and enemy, if closer than 50m, display it
		enemyDist = Vector3.Distance(enemyTransform.position, playerTransform.position);
		if(enemyDist < 50)
		{
			displayEnemyHealth = true;
		}
		else
		{
			displayEnemyHealth = false;
		}
	}

	void OnGUI()
	{
		// if distance < 50 display enemy's health
		if(displayEnemyHealth == true)
		{
			//GUI.Box(new Rect(Screen.width*2/3 - 10, 10, maxHealthBarLength, 20), "" + curHealth);
			GUI.Box(new Rect(Screen.width*2/3 - 10, 10, enemyHealthBarLength, 20), "" + curHealth);
		}
	}

	
	public void AdjustCurHealth(int adj)
	{
		curHealth += adj;
		
		if(curHealth < 0)
			curHealth = 0;
		
		if(curHealth > maxHealth)
			curHealth = maxHealth;
		
		if(maxHealth < 1)
			maxHealth = 1;
		
		enemyHealthBarLength = maxHealthBarLength * (curHealth / (float)maxHealth);
	}
}
