using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour {

	public CanvasGroup bloodPowerCanvas;
	public CanvasGroup dialACode;

	[Header("Phone View")]
	public CanvasGroup phoneCanvas;
	public CanvasGroup notification;
	public CanvasGroup containerCanvas;

	private const float NOTIFICATION_DELAY = 5f;

	private static CanvasManager instance;
	public static CanvasManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<CanvasManager>();
			}
			return instance;
		}
	}

	void Start(){
		DisplayDialACode(false);
		SetVisible(bloodPowerCanvas, true);
		SetVisible(notification, false);
		SetVisible(containerCanvas, false);
		SetVisible(phoneCanvas, false);
	}

	public void DisplayDialACode(bool on = true) {
		if(dialACode != null)
			SetVisible(dialACode, on);
	}

//	public void TogglePhoneCanvas(){
//		bool on = !phoneCanvas.interactable;
//		SetVisible(phoneCanvas, on);
//		if(!on){
//			UIManager.Instance.CloseInventory();
////			SetVisible(containerCanvas, false);
//		}
////		ToggleCanvas(phoneCanvas, true);
//	}

	public void DisplayPhoneCanvas(bool on = true){
		
//		if(!on)
//			UIManager.Instance.CloseInventory();
		SetVisible(phoneCanvas, on);
	}

	void ToggleCanvas(CanvasGroup canvas, Dialogue dialogue){
		bool on = !canvas.interactable;
		SetVisible(canvas, on);
//		CustomInputManager.EnableControls(on);
	}

	public void ToggleCanvas(CanvasGroup canvas) {
		bool on = !canvas.interactable;
		SetVisible(canvas, on);
	}
	
	public void SetVisible(CanvasGroup canvas, bool on) {
		canvas.alpha = on ? 1f : 0f;
		canvas.interactable = canvas.blocksRaycasts = on;

	}

	public void EnableBloodCanvas(bool on){
		bloodPowerCanvas.alpha = on ? 1f : 0f;
	}

	public void ShowNotification(string message){
		SetVisible(notification, true);
		notification.GetComponentInChildren<Text>().color = Color.white;
		notification.GetComponentInChildren<Text>().text = message;
		StartCoroutine(SetVisibleDelayed(notification, false, NOTIFICATION_DELAY));
	}

	public void ShowNotification(string message, Globals.Conviction conviction, int step = 0){
//		Debug.Log("Notifying " + message);
		SetVisible(notification, true);
		Color color = GameModel.GetConvictionColor(conviction);
		notification.GetComponentInChildren<Text>().color = color;
		notification.GetComponentInChildren<Text>().text = message;
		StartCoroutine(SetVisibleDelayed(notification, false, NOTIFICATION_DELAY));
	}

	IEnumerator SetVisibleDelayed(CanvasGroup canvas, bool on, float sec){
		yield return new WaitForSeconds(sec);
		SetVisible(canvas, on);
	}

	public void DisplayContainer(bool on = true){
		SetVisible(containerCanvas, on);
	}

	void ClearContainerItems(){
		ItemUI[] containerItems = UIManager.ContainerPanel.GetComponentsInChildren<ItemUI>();
		foreach (ItemUI child in containerItems) {
			GameObject.Destroy(child.gameObject);
		}
	}
		
}
