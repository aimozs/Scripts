using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Steamer : MonoBehaviour, IDamaging {

	[Range(0f,1f)]
	public float damage;
	public AudioClip clip;

	void Start () {
		GetComponent<Collider>().isTrigger = true;
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Player")){
			ApplyDamage(other.gameObject.GetComponent<BaseCharacter>());
		}
	}
	
	public void ApplyDamage(BaseCharacter baseChar){
		if(baseChar != null){
			baseChar.AdjustVitae(-damage, clip);
		}
	}
}
