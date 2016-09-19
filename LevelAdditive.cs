using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent (typeof (SphereCollider))]
public class LevelAdditive : MonoBehaviour {

	public string level;
	// Use this for initialization
	void Start () {
		GetComponent<Collider>().isTrigger = true;
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
//			StartCoroutine(LoadAsync());
			SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
//			SceneManager.SetActiveScene(SceneManager.GetSceneByName(level));
		}
	}

	void OnTriggerExit(Collider other){
		if(other.CompareTag("Player")){
			SceneManager.SetActiveScene(SceneManager.GetSceneByName("Montreal"));
			StartCoroutine(UnloadScene());
		}
	}

	IEnumerator LoadAsync(){
		AsyncOperation loading = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
		loading.allowSceneActivation = false;
		yield return loading;
		loading.allowSceneActivation = true;

	}

	IEnumerator UnloadScene(){
		yield return new WaitForEndOfFrame();
		SceneManager.UnloadScene(level);

	}
}
