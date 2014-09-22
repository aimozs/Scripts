using UnityEngine;

using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	private int maxHealth = 100;
	private int curHealth = 100;

	private float maxHealthBarLength;
	private float healthBarLength;

	private float barWidthPos = 20;
	private float barHeightPos = 30;
	private float barThickness = 20;
	
	public GUIStyle Health;
	public GUIStyle Vitae;

	void Awake()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		maxHealthBarLength = (Screen.width/3);
		AdjustCurHealth(0);
	}
	
	void OnGUI()
	{


		GUI.Box(new Rect(barWidthPos, barHeightPos, healthBarLength, barThickness), "", Health);
		GUI.Box(new Rect(barWidthPos, barHeightPos, maxHealthBarLength, barThickness), "Health");



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
		
		healthBarLength = maxHealthBarLength * (curHealth / (float)maxHealth);
	}
}
