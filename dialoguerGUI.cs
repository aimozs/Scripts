using UnityEngine;
using System.Collections;

public class dialoguerGUI : MonoBehaviour
{
	private bool _showing;
	private string _text;
	private string[] _choices;
	//define the element we need to deactivate while chating
	private GameObject player;
	private GameObject camera;
	private MouseLook mouseH;
	private MouseLook mouseV;
	private FPSInputController fpsinput;
	//define the variable for the UI
	private int dPosx;
	private int dPosy;
	private int dSizex;
	private int dSizey;
	//provide the field on the scriptComponent to associate with the dialogue number
	public int dial;
	
	// public int textBox;
	// public int centeredBox;

	void Awake()
	{
		Dialoguer.Initialize();
	}
	
	// Use this for initialization
	void Start ()
	{
		//setup for the dialogue
		Dialoguer.events.onStarted += onStarted;
		Dialoguer.events.onEnded += onEnded;
		Dialoguer.events.onTextPhase += onTextPhase;
		//define the different element to unable the player to move or the mouselook while dialoguing
		player = GameObject.FindGameObjectWithTag("Player");
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		fpsinput = player.GetComponent<FPSInputController>();
		mouseH = player.GetComponent<MouseLook>();
		mouseV = camera.GetComponent<MouseLook>();
		// textBox = 600;
		// centeredBox = 800/2 - textBox/2;
		//define the position and size of the dialogue box
		dPosx = Screen.width*10/100;
		dPosy = Screen.height/2;
		dSizex = Screen.width*80/100;
		dSizey = Screen.height/2;
	
	}

	void OnInteract()
	{
		Dialoguer.StartDialogue(dial);
		fpsinput.enabled = false;
		mouseH.enabled = false;
		mouseV.enabled = false;
	}

	void OnGUI()
	{
		if(!_showing)
			return;

		GUI.Box (new Rect(dPosx, dPosy, dSizex, dSizey), _text);

		if(_choices == null)
		{
			if(GUI.Button(new Rect(dPosx, dPosy + 200, dSizex, 30), "Continue Dialogue"))
			{
				Dialoguer.ContinueDialogue();
			}
		}
		else
		{
			for(int i =0; i<_choices.Length ;i++)
			{
				if(GUI.Button(new Rect(dPosx, dPosy + 200 + (40*i), dSizex, 30), _choices[i]))
				{
					Dialoguer.ContinueDialogue(i);
				}
			}
		}

	}

	private void onStarted()
	{
		_showing = true;
	}

	private void onEnded()
	{
		_showing = false;
		fpsinput.enabled = true;
		mouseH.enabled = true;
		mouseV.enabled = true;
	}

	private void onTextPhase(DialoguerTextData data)
	{
		_text = data.text;
		_choices = data.choices;
	}
}
