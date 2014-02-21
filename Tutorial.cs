using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
	public bool tutoMove = false;
	private bool tutoFlash = false;
	private bool tutoForw = false;
	private bool tutoBack = false;
	private bool tutoLeft = false;
	private bool tutoRight = false;
	private bool tutoInteract = false;
	private bool tutoJump = false;
	public string tuto;

	// Use this for initialization
	void Start ()
	{


	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyUp(KeyCode.F))
		{
			tutoFlash = true;
		}

		if (Input.GetKeyUp(KeyCode.W))
		{
			tutoForw = true;
		}

		if (Input.GetKeyUp(KeyCode.A))
		{
			tutoLeft = true;
		}

		if (Input.GetKeyUp(KeyCode.S))
		{
			tutoBack = true;
		}

		if (Input.GetKeyUp(KeyCode.D))
		{
			tutoRight = true;
		}

		if (Input.GetKeyUp(KeyCode.E))
		{
			tutoInteract = true;
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			tutoJump = true;
		}

		if (tutoForw && tutoBack && tutoLeft && tutoRight == true)
		{
			tutoMove = true;
		}

		if (tutoMove == false)
		{
			tuto = "To move around use W,A,S,D..";
		}

			else if (tutoFlash == false)
			{
				tuto = "To turn the flashlight on and off use F,";
			}
			else if(tutoJump == false)
			{
				tuto = "Pressing the space bar will make you jump,";
			}

			else if(tutoInteract == false)
			{
				tuto = "To interact with elements use E, choose the clan you want and 'interact' with its name!";
			}

		else
		{
			tuto = "";
		}
	}



	void OnGUI()
	{
		if (tuto != "")
		{
		GUI.Box(new Rect(10, 50, Screen.width-20, 30), tuto);
		}
		
	}
}
