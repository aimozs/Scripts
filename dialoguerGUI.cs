using UnityEngine;
using System.Collections;

public class dialoguerGUI : MonoBehaviour
{
	private bool _showing;
	private string _text;
	private string[] _choices;

	public int textBox;
	public int centeredBox;
	public int dial;

	void Awake()
	{
		Dialoguer.Initialize();
	}
	
	// Use this for initialization
	void Start ()
	{
		Dialoguer.events.onStarted += onStarted;
		Dialoguer.events.onEnded += onEnded;
		Dialoguer.events.onTextPhase += onTextPhase;
		textBox = 600;
		centeredBox = 800/2 - textBox/2;
	
	}

	void OnInteract()
	{
		Dialoguer.StartDialogue(dial);
	}

	void OnGUI()
	{
		if(!_showing)
			return;

		GUI.Box (new Rect(centeredBox,100,textBox,150), _text);

		if(_choices == null)
		{
			if(GUI.Button(new Rect(centeredBox, 220, textBox, 30), "Continue Dialogue"))
			{
				Dialoguer.ContinueDialogue();
			}
		}
		else
		{
			for(int i =0; i<_choices.Length ;i++)
			{
				if(GUI.Button(new Rect(centeredBox, 220 + (40*i), textBox, 30), _choices[i]))
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
	}

	private void onTextPhase(DialoguerTextData data)
	{
		_text = data.text;
		_choices = data.choices;
	}
}
