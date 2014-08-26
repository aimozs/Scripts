using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	private int maxHealth = 100;
	private int curHealth = 100;

	private float maxHealthBarLength;
	private float healthBarLength;

	private float barWidthPos;
	private float barHeightPos;
	private float barThickness;
	
	public GUIStyle Health;
	public GUIStyle Vitae;

	// Update is called once per frame
	void Update ()
	{
		barWidthPos = Screen.width*2/100;
		barHeightPos = Screen.height*1/100;
		barThickness = 20;

		maxHealthBarLength = (Screen.width/3);
		AdjustCurHealth(0);
	}
	
	void OnGUI()
	{
		//GUI.skin = Ventrue;

		GUI.Box(new Rect(barWidthPos, barHeightPos, healthBarLength, barThickness), "", Health);
		GUI.Box(new Rect(barWidthPos, barHeightPos, maxHealthBarLength, barThickness), "Health");
	
		GUI.Box(new Rect(barWidthPos, barHeightPos*2+barThickness, maxHealthBarLength, barThickness), "", Vitae);
		GUI.Box(new Rect(barWidthPos, barHeightPos*2+barThickness, healthBarLength, barThickness), "Vitae");
	
		GUI.Box(new Rect(maxHealthBarLength + barWidthPos*2, barHeightPos, (barThickness+barHeightPos)*2.5f, (barThickness+barHeightPos)*2.5f), "Discipline", Vitae);

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
