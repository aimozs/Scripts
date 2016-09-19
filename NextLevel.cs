using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (Interactable))]

public class NextLevel : MonoBehaviour, ILockable, IInteractable {

	public bool isTrigger = false;
	public bool isLocked = false;
	public int weigth = 1;
	public GameObject key;

	public bool loadOnEnter = true;
	public string level;
	public string spawn;

	public bool loadOnScenarioStep = true;
	public Globals.Conviction conviction = Globals.Conviction.sloth;
	public int convictionStep = 0;

	private static bool _isLoading = false;

//	private delegate void OnSceneLoaded(Scene scene, LoadSceneMode mode);

	void Start(){
		GetComponent<Collider>().isTrigger = isTrigger;
	}

	void OnEnable(){
		SceneManager.sceneLoaded += Init;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= Init;
	}

	void Init(Scene scene, LoadSceneMode mode){
		_isLoading = false;
	}

	public IEnumerator Load() {
		if(!_isLoading){
			_isLoading = true;
			GameSettings.StoreNextLevelData = this;
			yield return new WaitForSeconds(.5f);

			if(level != null) {
				GameManager.Instance.LoadScene(level, spawn);
			} else {
				Debug.Log("we are missing one of those information, level is: " + level + ", and spawn is: " + spawn);
			}
		}
	}

	//load on trigger enter
	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.CompareTag("Player") && loadOnEnter && loadOnScenarioStep) {
			if(Globals.TestConvictionProgress(conviction, convictionStep)){
				if(collider.GetComponent<BaseCharacter>().kind == BaseCharacter.Kind.human){
					collider.GetComponent<BaseCharacter>().PlayEmbraceAnimation();
				} else {
					StartCoroutine(Load());
				}
			} else {
				UIManager.Notify("This door is locked");
			}
		}
	}

	void OnTriggerExit(Collider collider) {
		if(collider.gameObject.CompareTag("Player") && !loadOnEnter) {
			StartCoroutine(Load());
		}
	}

	/// <summary>
	/// IInteractable
	/// </summary>
	public void OnInteract() {
		if(isLocked){
			if(key != null) {
				if(GameSettings.TestInventory(key)) {
					Unlock();
					UIManager.Notify("You have used the key to open this door");
				} else {
					UIManager.Notify("You need to find the key first!");
				}
			} else {
				UIManager.Notify("This door is locked");
			}
		} else {
//			if(Globals.TestConvictionProgress(conviction, convictionStep)){
				StartCoroutine(Load());
//			} else {
//				UIManager.Notify("This door is locked", conviction, convictionStep);
//			}
		}
	}

	public GameObject GetGameObject(){
		return gameObject;
	}


	/// <summary>
	/// ILockable
	/// </summary>
	public void Lock(){
		isLocked = true;
		weigth = 99;
		Debug.Log(gameObject.name + " set to " + weigth);
	}

	public void Unlock(){
		isLocked = false;
		InventoryManager.PlayerInventory.Remove(key);
	}
}