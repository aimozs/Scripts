using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasManager : MonoBehaviour {

	// Use this for initialization
	public CanvasGroup dialogueCanvas;
	public CanvasGroup inGameCanvas;
	public CanvasGroup optionCanvas;

	private static CanvasManager instance;
	public static CanvasManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType<CanvasManager>();
			}
			return instance;
		}
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DisplayDialogue(Dialogue dialogue)
	{
		ToggleCanvas(dialogueCanvas, dialogue);
//		dialogueCanvas.transform.FindChild("Question").GetComponent<Text>().text = dialogue.chat;
	}

	void ToggleCanvas(CanvasGroup canvas, Dialogue dialogue)
	{
		bool on = !canvas.interactable;
		SetVisible(canvas, on);
	}

	void ToggleCanvas(CanvasGroup canvas)
	{
		bool on = !canvas.interactable;
		SetVisible(canvas, on);
	}
	
	void SetVisible(CanvasGroup canvas, bool on)
	{
		canvas.alpha = on ? 1f : 0f;
		canvas.interactable = canvas.blocksRaycasts = on ? true : false;
		UIManager.Instance.ToggleControls();
	}
}
