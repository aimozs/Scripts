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
	public float healthBarLength;

	public int maxHealth = 100;
	public int curHealth = 100;
	
	// Use this for initialization
	void Start ()
	{
		healthBarLength = Screen.width / 2;

		//defining enemy's position
		enemyTransform = (GameObject.FindGameObjectWithTag("Enemy")).transform;

		//defining player's position
		playerTransform = (GameObject.FindGameObjectWithTag("Player")).transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		AdjustCurHealth(0);

		enemyDist = Vector3.Distance(enemyTransform.position, playerTransform.position);


		displayEnemyHealth = false;
		if(enemyDist < 50)
		{
			displayEnemyHealth = true;
		}
	}

	void OnGUI()
	{
		// if distance < 50 display enemy's health
		if(displayEnemyHealth == true)
		{
			GUI.Box(new Rect(10, 40, healthBarLength, 20), curHealth + "/" + maxHealth);
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
		
		healthBarLength = (Screen.width / 2) * (curHealth / (float)maxHealth);
	}
}
