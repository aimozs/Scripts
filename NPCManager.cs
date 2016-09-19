using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour {

	public bool debugNPC;
	public List<NPC> NPCresidents = new List<NPC>();
	public List<NPC> NPCguests = new List<NPC>();
	public List<NPC> NPCalive = new List<NPC>();
	public RuntimeAnimatorController basicController;

	private bool generateNPC = false;

	private static NPCManager instance;
	public static NPCManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<NPCManager>();
			}
			return instance;
		}
	}

//	public delegate void ManageNPC();
//	public static event ManageNPC manageNPCs;


	void OnEnable(){
		SceneManager.sceneLoaded += SetGenerateNPC;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= SetGenerateNPC;
	}

	void SetGenerateNPC(Scene scene, LoadSceneMode loadMode){
		if(StartGame.Instance.residents.Count > 0)
			NPCresidents = StartGame.Instance.residents;

		if(StartGame.Instance.guests.Count > 0)
			NPCguests = StartGame.Instance.guests;
		
//		generateNPC = false;
//
//		if(scene.name == "Montreal" || scene.name == "Resources")
//			generateNPC = true;
//
//		if(generateNPC){
			InvokeRepeating("GenerateNPC", 10f, 10f);
//			StartCoroutine(GenerateNPC());
//		} else {
//			CancelInvoke();
//		}

//		StartCoroutine(ManageNPCs());
	}

//	IEnumerator ManageNPCs() {
//		
//		if(manageNPCs != null)
//			manageNPCs();
//		
//		yield return new WaitForSeconds(12f);
//		StartCoroutine(ManageNPCs());
//	}

	void GenerateNPC(){

		if(NPCresidents.Count != 0){

			int unit = UnityEngine.Random.Range(0, NPCresidents.Count-1);
			if(NPCresidents[unit] != null){
				Instantiate(NPCresidents[unit], GameSettings.GetRandomTarget().transform.position, Quaternion.identity);

				NPCalive.Add(NPCresidents[unit]);
			}
			NPCresidents.RemoveAt(unit);
		} else {
			if(NPCguests.Count != 0 && NPCalive.Count < StartGame.Instance.capacity){
				int unit = UnityEngine.Random.Range(0, NPCguests.Count-1);
				if(NPCguests[unit] != null){
					Instantiate(NPCguests[unit], GameSettings.GetRandomTarget().transform.position, Quaternion.identity);

					NPCalive.Add(NPCguests[unit]);
				}
				NPCguests.RemoveAt(unit);
			}
		}
	}


	public void KillNPCs(){
		StopAllCoroutines();

		foreach(NPC npc in NPCalive){
			NPCresidents.Add(npc);
		}
		NPCalive.Clear();
	}

	public void RemoveNPC(NPC npc){
		NPCresidents.Add(npc);
		NPCalive.Remove(npc);
		Destroy(npc);
	}
}
